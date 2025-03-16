using AutoMapper;
using IO.Ably;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect;
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
    }

    public class InspectingResultService : IInspectingResultService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public InspectingResultService(IMapper mapper)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAllResults(string? evaluatedResult)
        {
            try
            {
                var results = await _unitOfWork.InspectingResultRepository.GetInspectingResults(evaluatedResult);
                var rs = _mapper.Map<List<InspectingResultModel>>(results);

                if (results.Count > 0)
                {
                    return new BusinessResult(200, "List of inspecting results: ", rs);
                }
                else
                {
                    return new BusinessResult(404, "Not found any inspecting results");
                }
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
                    return new BusinessResult(200, "List of inspecting results: ", rs);
                }
                else
                {
                    return new BusinessResult(404, "Not found any inspecting results");
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
                var result = _mapper.Map<InspectingResult>(model);
                result.Id = id;
                var plant = await _unitOfWork.InspectingResultRepository.GetPlantByInspectingForm(id);
                if (plant == null)
                {
                    return new BusinessResult(404, "Not found any plant");
                }

                string res = await ClassifyResult(model, plant.Id);
                result.EvaluatedResult = res;

                return new BusinessResult(200, "Create inspecting result success !", result);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        private readonly Dictionary<string, Dictionary<string, float>> _contaminantLimits = new()
        {
            { "Rau họ thập tự", new Dictionary<string, float> { { "Cadmi", 0.05f } } },
            { "Hành", new Dictionary<string, float> { { "Cadmi", 0.05f } } },
            { "Rau ăn lá", new Dictionary<string, float> { { "Cadmi", 0.2f }, { "Plumbum", 0.3f } } },
            { "Rau ăn quả", new Dictionary<string, float> { { "Cadmi", 0.05f }, { "Plumbum", 0.1f } } },
            { "Rau ăn củ", new Dictionary<string, float> { { "Cadmi", 0.1f }, { "Plumbum", 0.1f } } },
            { "Nấm", new Dictionary<string, float> { { "Cadmi", 0.2f } } },
            { "Rau củ quả", new Dictionary<string, float> { { "Hydrargyrum", 0.02f } } },
            { "Rau khô", new Dictionary<string, float> { { "Arsen", 1.0f } } }
        };

        private readonly Dictionary<string, float> _globalLimits = new()
        {
            { "SulfurDioxide", 10.0f },
            { "Nitrat", 9.0f },
            { "NaNO3_KNO3", 15.0f },
            { "Glyphosate_Glufosinate", 0.01f },
            { "MethylBromide", 0.01f },
            { "HydrogenPhosphide", 0.01f },
            { "Dithiocarbamate", 0.01f },
            { "Chlorate", 0.01f },
            { "Perchlorate", 0.01f }
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

                var limits = _contaminantLimits[plant.Type];
                int violationCount = 0;
                bool hasSevereViolation = false;

                void CheckLimit(string contaminant, float value)
                {
                    if (!limits.ContainsKey(contaminant)) return;
                    float limit = limits[contaminant];

                    if (value > 1.3 * limit)
                    {
                        hasSevereViolation = true;
                        violationCount++;
                    }
                    else if (value > limit)
                    {
                        violationCount++;
                    }
                }

                CheckLimit("Cadmi", model.Cadmi);
                CheckLimit("Plumbum", model.Plumbum);
                CheckLimit("Hydrargyrum", model.Hydrargyrum);
                CheckLimit("Arsen", model.Arsen);
                CheckLimit("SulfurDioxide", model.SulfurDioxide);
                CheckLimit("Nitrat", model.Nitrat);
                CheckLimit("NaNO3_KNO3", model.NaNO3_KNO3);
                CheckLimit("Glyphosate_Glufosinate", model.Glyphosate_Glufosinate);
                CheckLimit("MethylBromide", model.MethylBromide);
                CheckLimit("HydrogenPhosphide", model.HydrogenPhosphide);
                CheckLimit("Dithiocarbamate", model.Dithiocarbamate);
                CheckLimit("Chlorate", model.Chlorate);
                CheckLimit("Perchlorate", model.Perchlorate);

                if (hasSevereViolation || violationCount > 3)
                    return "Grade 1";

                if (violationCount > 0)
                    return "Grade 2";

                return "Grade 3";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
