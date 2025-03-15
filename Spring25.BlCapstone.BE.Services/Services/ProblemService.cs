using AutoMapper;
using Microsoft.AspNetCore.Http;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Problem;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IProblemService
    {
        Task<IBusinessResult> GetAll(int? planId);
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> UpdateResult(int id, UpdateResult model);
        Task<IBusinessResult> Create(CreateProblem model);
        Task<IBusinessResult> Update(int id, UpdateProblem model);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
    }

    public class ProblemService : IProblemService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProblemService(IMapper mapper)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAll(int? planId)
        {
            try
            {
                var problems = await _unitOfWork.ProblemRepository.GetProblems(planId);

                var res = _mapper.Map<List<ProblemModel>>(problems);
                if (res.Any())
                {
                    return new BusinessResult
                    {
                        Status = 200,
                        Message = "List of Problems",
                        Data = res
                    };
                }

                return new BusinessResult(404, "Not found any Problems", null);
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

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var pro = await _unitOfWork.ProblemRepository.GetProblem(id);

                var res = _mapper.Map<ProblemModel>(pro);
                if (pro != null)
                {
                    return new BusinessResult(200, "Problem ne", res);
                }

                return new BusinessResult(404, "Not found any Problems", null);
            }
            catch(Exception ex)
            {
                return new BusinessResult
                {
                    Status = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<IBusinessResult> UpdateResult(int id, UpdateResult model)
        {
            try
            {
                var problem = await _unitOfWork.ProblemRepository.GetByIdAsync(id);
                if (problem == null)
                {
                    return new BusinessResult { Status = 404, Message = "Not found any problems", Data = null };
                }

                problem.Status = model.Status;
                problem.ResultContent = model.ResultContent;

                var rs = await _unitOfWork.ProblemRepository.UpdateAsync(problem);
                if (rs > 0)
                {
                    return new BusinessResult { Status = 200, Message = "Update result successfull", Data = problem };
                }
                else
                {
                    return new BusinessResult { Status = 500, Message = "Update failed !", Data = null };
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }

        public async Task<IBusinessResult> Create(CreateProblem model)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(model.PlanId);
                if (plan == null)
                {
                    return new BusinessResult(404, "Not found any plan");
                }

                var problem = _mapper.Map<Problem>(model);
                problem.Status = "Pending";
                var rs = await _unitOfWork.ProblemRepository.CreateAsync(problem);

                if (model.ImageUrl != null)
                {
                    foreach (var image in model.ImageUrl)
                    {
                        var img = new ProblemImage
                        {
                            ProblemId = rs.Id,
                            Url = image
                        };
                        await _unitOfWork.ProblemImageRepository.CreateAsync(img);
                    }
                }

                return new BusinessResult(200, "Create successfully!", rs);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message, null);
            }
        }

        public async Task<IBusinessResult> UploadImage(List<IFormFile> file)
        {
            try
            {
                var image = await CloudinaryHelper.UploadMultipleImages(file);
                var url = image.Select(x => x.Url).ToList();

                return new BusinessResult(200, "Upload success !", url);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> Update(int id, UpdateProblem model)
        {
            try
            {
                var prob = await _unitOfWork.ProblemRepository.GetByIdAsync(id);
                if (prob == null)
                {
                    return new BusinessResult(404, "Not found any problems");
                }

                _mapper.Map(model, prob);
                var rs = await _unitOfWork.ProblemRepository.UpdateAsync(prob);
                
                var images = await _unitOfWork.ProblemImageRepository.GetImages(id);
                if (images.Count > 0)
                {
                    foreach (var image in images)
                    {
                        await _unitOfWork.ProblemImageRepository.RemoveAsync(image);
                    }
                }

                if (model.ImageUrl != null)
                {
                    foreach (var image in model.ImageUrl)
                    {
                        var img = new ProblemImage
                        {
                            ProblemId = id,
                            Url = image
                        };
                        await _unitOfWork.ProblemImageRepository.CreateAsync(img);
                    }
                }

                return new BusinessResult(200, "Update successfully", rs);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
