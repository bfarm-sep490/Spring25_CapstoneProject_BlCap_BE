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
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
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

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var problems = await _unitOfWork.ProblemRepository.GetProblems();

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
    }
}
