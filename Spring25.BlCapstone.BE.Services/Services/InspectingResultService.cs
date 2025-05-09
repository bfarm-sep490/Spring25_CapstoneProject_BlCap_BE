﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.BlockChain;
using Spring25.BlCapstone.BE.Repositories.Helper;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect;
using Spring25.BlCapstone.BE.Services.Untils;
using Spring25.BlCapstone.BE.Services.Utils;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IInspectingResultService
    {
        Task<IBusinessResult> GetAllResults(string? evaluatedResult);
        Task<IBusinessResult> GetResultById(int id);
        Task<IBusinessResult> ReportForm(int id, CreateInspectingResult model);
        Task<IBusinessResult> UploadDocument(List<IFormFile> file);
    }

    public class InspectingResultService : IInspectingResultService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IVechainInteraction _vechainInteraction;

        public InspectingResultService(IMapper mapper, IVechainInteraction vechainInteraction)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
            _vechainInteraction = vechainInteraction;
        }

        public async Task<IBusinessResult> GetAllResults(string? evaluatedResult)
        {
            try
            {
                var results = await _unitOfWork.InspectingResultRepository.GetInspectingResults(evaluatedResult);
                var rs = _mapper.Map<List<InspectingResultModel>>(results);

                return new BusinessResult(200, "List of inspecting results: ", rs);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetResultById(int id)
        {
            try
            {
                var results = await _unitOfWork.InspectingResultRepository.GetInspectingResults(resultId: id);
                var rs = _mapper.Map<List<InspectingResultModel>>(results);

                if (results.Count > 0)
                {
                    return new BusinessResult(200, "Inspecting result: ", rs);
                }
                else
                {
                    return new BusinessResult(400, "Not found any inspecting results");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> ReportForm(int id, CreateInspectingResult model)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var form = await _unitOfWork.InspectingResultRepository.GetByIdAsync(id);
                if (form != null)
                {
                    return new BusinessResult(400, "This form already has result !");
                }

                var result = _mapper.Map<InspectingResult>(model);
                result.Id = id;
                var plant = await _unitOfWork.InspectingResultRepository.GetPlantByInspectingForm(id);
                if (plant == null)
                {
                    return new BusinessResult(400, "Not found any plant");
                }

                string res = await ClassifyResult(model, plant.Id);
                result.EvaluatedResult = res;
                _unitOfWork.InspectingResultRepository.PrepareCreate(result);
                if (model.Images != null && model.Images.Any())
                {
                    foreach (var image in model.Images)
                    {
                        _unitOfWork.InspectingImageRepository.PrepareCreate(new InspectingImage
                        {
                            ResultId = id,
                            Url = image
                        });
                    }
                }

                var insForm = await _unitOfWork.InspectingFormRepository.GetInspectingFormById(id);
                insForm.Status = "Complete";
                insForm.CompleteDate = DateTimeHelper.NowVietnamTime();
                _unitOfWork.InspectingFormRepository.PrepareUpdate(insForm);

                var re = _mapper.Map<InspectingResultModel>(result);

                var blTransaction = await _unitOfWork.PlanTransactionRepository.GetPlanTransactionByTaskId(inspectingFormId: id);
                var task = new DataInspect
                {
                    Arsen = model.Arsen,
                    Plumbum = model.Plumbum,
                    Cadmi = model.Cadmi,
                    Hydrargyrum = model.Hydrargyrum,
                    Salmonella = model.Salmonella,
                    Coliforms = model.Coliforms,
                    Ecoli = model.Ecoli,
                    Glyphosate_Glufosinate = model.Glyphosate_Glufosinate,
                    SulfurDioxide = model.SulfurDioxide,
                    MethylBromide = model.MethylBromide,
                    HydrogenPhosphide = model.HydrogenPhosphide,
                    Dithiocarbamate = model.Dithiocarbamate,
                    Nitrat = model.Nitrat,
                    NaNO3_KNO3 = model.NaNO3_KNO3,
                    Chlorate = model.Chlorate,
                    Perchlorate = model.Perchlorate,
                    ResultContent = model.ResultContent == null ? "" : model.ResultContent,
                    Timestamp = (new DateTimeOffset(DateTimeHelper.NowVietnamTime()).ToUnixTimeSeconds()).ToString(),
                    Inspector = new VeChainFarmer
                    {
                        Id = insForm.InspectorId == null ? 0 : insForm.InspectorId.Value,
                        Name = insForm.Inspector.Account.Name
                    }
                };

                var resultBlock = await _vechainInteraction.CreateNewVechainInspect(blTransaction.UrlAddress, new CreateVechainInspect
                {
                    InspectionId = id,
                    InspectionType = res,
                    Data = JsonConvert.SerializeObject(task)
                });

                await _unitOfWork.InspectingResultRepository.SaveAsync();
                await _unitOfWork.InspectingImageRepository.SaveAsync();
                await _unitOfWork.InspectingFormRepository.SaveAsync();

                var inspectorChanel = $"inspector-{insForm.InspectorId}";
                var message = "Chúng tôi đã nhận được kết quả kiểm định từ quý đơn vị. Xin chân thành cảm ơn sự phối hợp và hỗ trợ trong quá trình kiểm định. Rất mong sẽ tiếp tục đồng hành cùng quý đơn vị trong các kế hoạch tiếp theo.";
                var title = $"Đã nhận kết quả kiểm định – Cảm ơn sự hợp tác của quý đơn vị - {plant.PlantName}";
                await AblyHelper.SendMessageWithChanel(title, message, inspectorChanel);
                await _unitOfWork.NotificationInspectorRepository.CreateAsync(new NotificationInspector
                {
                    InspectorId = insForm.InspectorId.Value,
                    Message = message,
                    Title = title,
                    IsRead = false,
                    CreatedDate = DateTimeHelper.NowVietnamTime(),
                });

                var ownerChanel = "owner";
                var mo = "Bên kiểm định đã gửi kết quả điểm định về cây trồng trong kế hoạch. Hãy truy cập kế hoạch để xem chi tiết hơn về kết quả kiểm định!";
                var to = $"Đã có kết quả kiểm định từ kế hoạch - {plant.PlantName}";
                await AblyHelper.SendMessageWithChanel(to, mo, ownerChanel);
                await _unitOfWork.NotificationOwnerRepository.CreateAsync(new NotificationOwner
                {
                    OwnerId = 1,
                    Message = mo,
                    Title = to,
                    IsRead = false,
                    CreatedDate = DateTimeHelper.NowVietnamTime(),
                });

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
                return new BusinessResult(200, "Create inspecting result success !", re);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new BusinessResult(500, ex.Message);
            }
        }

        private readonly Dictionary<string, Dictionary<string, float>> _contaminantLimits = new()
        {
            { "Rau họ thập tự", new Dictionary<string, float> { { "Cadmi", 0.05f }, { "Plumbum", 0.3f } } },
            { "Hành", new Dictionary<string, float> { { "Cadmi", 0.05f }, { "Plumbum", 0.1f } } },
            { "Rau ăn lá", new Dictionary<string, float> { { "Cadmi", 0.2f }, { "Plumbum", 0.3f } } },
            { "Rau ăn quả", new Dictionary<string, float> { { "Cadmi", 0.05f }, { "Plumbum", 0.1f } } },
            { "Rau ăn củ", new Dictionary<string, float> { { "Cadmi", 0.1f }, { "Plumbum", 0.1f } } },
            { "Rau ăn thân", new Dictionary<string, float> { { "Cadmi", 0.1f } } },
            { "Nấm", new Dictionary<string, float> { { "Cadmi", 0.2f }, { "Plumbum", 0.3f } } },
            { "Rau củ quả", new Dictionary<string, float> { { "Hydrargyrum", 0.02f } } },
            { "Rau khô", new Dictionary<string, float> { { "Arsen", 1.0f } } },
            { "Rau họ đậu", new Dictionary<string, float> { { "Cadmi", 0.1f }, { "Plumbum", 0.2f } } }
        };

        private readonly Dictionary<string, float> _globalLimits = new()
        {
            { "SulfurDioxide", 0f },
            { "Nitrat", 5000f },
            { "Glyphosate_Glufosinate", 0.1f },
            { "MethylBromide", 0.01f },
            { "HydrogenPhosphide", 0f },
            { "Dithiocarbamate", 0.01f },
            { "Chlorate", 0.01f },
            { "Perchlorate", 0.05f },
            { "Salmonella", 0f },
            { "Coliforms", 10f }
        };

        private async Task<string> ClassifyResult(CreateInspectingResult model, int plantId)
        {
            try
            {
                var plant = await _unitOfWork.PlantRepository.GetByIdAsync(plantId);
                if (plant == null)
                {
                    throw new Exception("Not found any plant !");
                }

                if (!_contaminantLimits.ContainsKey(plant.Type))
                {
                    return "Không có dữ liệu cho loại cây này";
                }

                var limits = _contaminantLimits.ContainsKey(plant.Type) ? _contaminantLimits[plant.Type] : new Dictionary<string, float>();
                int minorViolations = 0;
                int majorViolations = 0;

                if (_globalLimits.ContainsKey("Salmonella") && model.Salmonella > _globalLimits["Salmonella"])
                {
                    return "Grade 3";
                }
                
                if (_globalLimits.ContainsKey("SulfurDioxide") && model.Salmonella > _globalLimits["SulfurDioxide"])
                {
                    return "Grade 3";
                }
                
                if (_globalLimits.ContainsKey("HydrogenPhosphide") && model.Salmonella > _globalLimits["HydrogenPhosphide"])
                {
                    return "Grade 3";
                }

                void CheckLimit(string contaminant, float value)
                {
                    if (!limits.ContainsKey(contaminant)) return;
                    float limit = limits[contaminant];

                    if (value >= limit)
                        majorViolations++;
                    else if (value < limit && (value > (limit / 1.3)))
                        minorViolations++;
                }

                void CheckLimitGlobal(string contaminant, float value)
                {
                    if (!_globalLimits.ContainsKey(contaminant)) return;
                    float limit = _globalLimits[contaminant];

                    if (value >= limit)
                        majorViolations++;
                    else if (value < limit && (value > (limit / 1.3)))
                        minorViolations++;
                }

                void CheckEColi(float value)
                {
                    float lowerLimit = (float)Math.Pow(10, 2);
                    float upperLimit = (float)Math.Pow(10, 3);

                    if (value > upperLimit)
                        majorViolations++;
                    else if (value >= lowerLimit && value <= upperLimit)
                    {
                        minorViolations++;
                    }
                }

                CheckEColi(model.Ecoli);

                CheckLimit("Cadmi", model.Cadmi);
                CheckLimit("Plumbum", model.Plumbum);
                CheckLimit("Hydrargyrum", model.Hydrargyrum);
                CheckLimit("Arsen", model.Arsen);
                CheckLimit("Coliforms", model.Coliforms);

                CheckLimitGlobal("Nitrat", model.Nitrat);
                CheckLimitGlobal("NaNO3_KNO3", model.NaNO3_KNO3);
                CheckLimitGlobal("Glyphosate_Glufosinate", model.Glyphosate_Glufosinate);
                CheckLimitGlobal("MethylBromide", model.MethylBromide);
                CheckLimitGlobal("Dithiocarbamate", model.Dithiocarbamate);
                CheckLimitGlobal("Chlorate", model.Chlorate);
                CheckLimitGlobal("Perchlorate", model.Perchlorate);

                if (majorViolations > 0 || minorViolations > 3)
                    return "Grade 3";

                if (minorViolations > 0 && minorViolations <= 3)
                    return "Grade 2";

                return "Grade 1";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IBusinessResult> UploadDocument(List<IFormFile> file)
        {
            try
            {
                var image = await CloudinaryHelper.UploadMultipleDocuments(file);
                var url = image.Select(x => x.Url).ToList();

                return new BusinessResult
                {
                    Status = 200,
                    Message = "Upload success !",
                    Data = url
                };
            }
            catch (Exception ex)
            {
                return new BusinessResult
                {
                    Status = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}
