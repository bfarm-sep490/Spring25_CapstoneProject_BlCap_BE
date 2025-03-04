using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Problem;
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
    }
}
