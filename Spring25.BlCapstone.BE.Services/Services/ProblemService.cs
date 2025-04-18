﻿using AutoMapper;
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
        Task<IBusinessResult> GetAll(int? planId, int? farmerId, string? name, string? status, int? pageNumber, int? pageSize);
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Create(CreateProblem model);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
        Task<IBusinessResult> ReportProblem(int id, ReportProblem model);
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

        public async Task<IBusinessResult> GetAll(int? planId, int? farmerId, string? name, string? status, int? pageNumber, int? pageSize)
        {
            try
            {
                var problems = await _unitOfWork.ProblemRepository.GetProblems(planId, farmerId, name, status, pageNumber, pageSize);
                if (!problems.Any())
                {
                    return new BusinessResult(404, "Not found any Problems", null);
                }

                var res = _mapper.Map<List<ProblemModel>>(problems);
                return new BusinessResult(200, "List of problems: ", res);
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

        public async Task<IBusinessResult> Create(CreateProblem model)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(model.PlanId);
                if (plan == null)
                {
                    return new BusinessResult(404, "Not found any plan");
                }

                var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(model.FarmerId);
                if (farmer == null)
                {
                    return new BusinessResult(404, "Not found any farmer");
                }

                var problem = _mapper.Map<Problem>(model);
                problem.Status = "Pending";
                problem.CreatedDate = DateTime.Now;
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
                var result = _mapper.Map<ProblemModel>(rs);

                return new BusinessResult(200, "Create successfully!", result);
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

        public async Task<IBusinessResult> ReportProblem(int id, ReportProblem model)
        {
            try
            {
                var problem = await _unitOfWork.ProblemRepository.GetByIdAsync(id);
                if (problem == null)
                {
                    return new BusinessResult(404, "Not found any problems");
                }

                if (!model.Status.ToLower().Trim().Equals("cancel") && !model.Status.ToLower().Trim().Equals("resolve"))
                {
                    return new BusinessResult(400, $"Invalid status for problem: {model.Status}");
                }

                problem.Status = model.Status;
                problem.ResultContent = model.ResultContent;
                await _unitOfWork.ProblemRepository.UpdateAsync(problem);
                var rs = _mapper.Map<ProblemModel>(problem);

                var tasks = await _unitOfWork.CaringTaskRepository.GetAllCaringTasksByProblemId(id);

                if (model.Status.ToLower().Trim().Equals("cancel"))
                {
                    foreach (var task in tasks)
                    {
                        task.Status = "Cancel";
                        _unitOfWork.CaringTaskRepository.PrepareUpdate(task);
                    }
                } 
                else
                {
                    foreach (var task in tasks)
                    {
                        task.Status = "Ongoing";
                        _unitOfWork.CaringTaskRepository.PrepareUpdate(task);
                    }
                }

                await _unitOfWork.CaringTaskRepository.SaveAsync();
                return new BusinessResult(200, "Report successfully", rs);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
