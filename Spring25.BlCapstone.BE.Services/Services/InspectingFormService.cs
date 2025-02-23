using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IInspectingFormService
    {
        Task<IBusinessResult> GetAllInspectingForm();
        Task<IBusinessResult> GetInspectingFormById(int id);
        Task<IBusinessResult> GetDetailInspectingFormById(int id);
        Task<IBusinessResult> CreateInspectingForm(InspectingFormModel result);
        Task<IBusinessResult> UpdateInspectingForm(int id,InspectingFormModel result);
        Task<IBusinessResult> DeleteInspectingFormById(int id);
    }
    public class InspectingFormService:IInspectingFormService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        public InspectingFormService(IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<IBusinessResult> CreateInspectingForm(InspectingFormModel result)
        {
            throw new NotImplementedException();
        }

        public Task<IBusinessResult> DeleteInspectingFormById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IBusinessResult> GetAllInspectingForm()
        {
            var list = await _unitOfWork.InspectingTaskRepository.GetAllAsync();
            var result = _mapper.Map<List<InspectingFormModel>>(list);
            return new BusinessResult(200,"Get all Inspecting forms",result);
        }

        public async Task<IBusinessResult> GetDetailInspectingFormById(int id)
        {
            var obj = await _unitOfWork.InspectingTaskRepository.GetDetailInspectingFormById(id);
            if (obj == null) return new BusinessResult(400, "Not Found this object");
            var result = _mapper.Map<InspectingFormModel>(obj);
            return new BusinessResult(200, "Get detail Inspecting form by id", result);
        }

        public async Task<IBusinessResult> GetInspectingFormById(int id)
        {
            var obj = await _unitOfWork.InspectingTaskRepository.GetByIdAsync(id);
            if (obj == null) return new BusinessResult(400, "Not Found this object");
            var result = _mapper.Map<InspectingFormModel>(obj);
            return new BusinessResult(200, "Get Inspecting form by id", result);
        }

        public Task<IBusinessResult> UpdateInspectingForm(int id, InspectingFormModel result)
        {
            throw new NotImplementedException();
        }
    }
}
