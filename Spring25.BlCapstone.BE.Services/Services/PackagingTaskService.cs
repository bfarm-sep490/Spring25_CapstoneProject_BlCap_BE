using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IPackagingTaskService
    {
        Task<IBusinessResult> GetPackagingTasks(int? planId);
    }
    public class PackagingTaskService : IPackagingTaskService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public PackagingTaskService(IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetPackagingTasks(int? planId)
        {
            try
            {
                if (planId != null)
                {
                    var plan = await _unitOfWork.PlanRepository.GetByIdAsync(planId.Value);

                    if (plan == null)
                    {
                        return new BusinessResult { Status = 404, Message = "Not found any Plan", Data = null };
                    }
                }

                var packs = await _unitOfWork.PackagingTaskRepository.GetPackagingTask(planId);
                var rs = _mapper.Map<List<PackagingTaskModel>>(packs);

                return new BusinessResult { Status = 200, Message = "List of packaging tasks:", Data = rs };
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }
    }
}
