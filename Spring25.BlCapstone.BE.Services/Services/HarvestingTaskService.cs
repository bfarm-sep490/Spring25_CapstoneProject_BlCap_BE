using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Havest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IHarvestingTaskService
    {
        Task<IBusinessResult> GetHarvestingTasks();
        Task<IBusinessResult> GetHarvestingTaskById(int id);
        Task<IBusinessResult> GetHarvestingTaskDetailById(int id);
        Task<IBusinessResult> CreateHarvestingTask(HarvestingTaskModel model);
        Task<IBusinessResult> UpdateHarvestingTask(int id, HarvestingTaskModel model);
        Task<IBusinessResult> DeleteHarvestingTask(int id);


    }
    public class HarvestingTaskService : IHarvestingTaskService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public HarvestingTaskService(IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<IBusinessResult> CreateHarvestingTask(HarvestingTaskModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IBusinessResult> DeleteHarvestingTask(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IBusinessResult> GetHarvestingTaskById(int id)
        {
            var obj = await _unitOfWork.HarvestingTaskRepository.GetByIdAsync(id);
            if (obj == null) return new BusinessResult(400, "Not found this id");
            var result = _mapper.Map<HarvestingTaskModel>(obj);
            return new BusinessResult(200, "Get harvesting task by id");
        }

        public async Task<IBusinessResult> GetHarvestingTaskDetailById(int id)
        {
            var obj = await _unitOfWork.HarvestingTaskRepository.GetByIdAsync(id);
            if (obj == null) return new BusinessResult(400, "Not found this id");
            var result = _mapper.Map<HarvestingTaskModel>(obj);
            return new BusinessResult(200, "Get detail harvesting task by id");
        }

        public async Task<IBusinessResult> GetHarvestingTasks()
        {
            var obj = await _unitOfWork.HarvestingTaskRepository.GetAllAsync();
            var result = _mapper.Map<List<HarvestingTaskModel>>(obj);
            return new BusinessResult(200, "Get harvesting tasks");
        }

        public Task<IBusinessResult> UpdateHarvestingTask(int id, HarvestingTaskModel model)
        {
            throw new NotImplementedException();
        }
    }
}
