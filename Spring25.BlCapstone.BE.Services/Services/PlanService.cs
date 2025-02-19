using AutoMapper;
using IO.Ably;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IPlanService
    {
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> GetAll();
    }

    public class PlanService : IPlanService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PlanService(IMapper mapper)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetPlan(id);

                var res = _mapper.Map<PlanModel>(plan);
                if (res != null)
                {
                    return new BusinessResult(200, "Plan ne", res);
                }

                return new BusinessResult(404, "Not found any Plans", null);
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

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetAllAsync();

                var rs = _mapper.Map<List<PlanForList>>(plan);
                if (rs != null)
                {
                    return new BusinessResult(200, "List of Plans", rs);
                }

                return new BusinessResult(404, "Not found any Plans", null);
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
