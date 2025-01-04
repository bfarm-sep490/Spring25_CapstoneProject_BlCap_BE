using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IPesticideService
    {
    Task<IBusinessResult> GetAll();
    Task<IBusinessResult> GetByFarmOwnerId(int id);
    Task<IBusinessResult> Update(int id, PesticideModel model);
    Task<IBusinessResult> Create(PesticideModel model);
    Task<IBusinessResult> Delete(int id);
    }
    public class PesticideService:IPesticideService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PesticideService(IMapper mapper)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
        }

        public async Task<IBusinessResult> Create(PesticideModel model)
        {
            var orner = await _unitOfWork.FarmOwnerRepository.GetByIdAsync(model.FarmOrnerId);
            if (orner == null) { return new BusinessResult(2, "Do not have this Farm Owner"); }
            var obj = _mapper.Map<Pesticide>(model);
            obj.CreatedAt = DateTime.Now;
            obj.Status = "true";
            var result = await _unitOfWork.PesticideRepository.CreateAsync(obj);
            return new BusinessResult(1, "Create Pesticide successfully", result);
        }

        public async Task<IBusinessResult> Delete(int id)
        {
            var obj = await _unitOfWork.PesticideRepository.GetByIdAsync(id);
            if (obj == null) { return new BusinessResult(2, "Do not have this Pesticide"); }
            var result = await _unitOfWork.PesticideRepository.RemoveAsync(obj);
            if (result) { return new BusinessResult(1, "Remove Pesticide successfully", result); }
            return new BusinessResult(2, "Remove Pesticide fail!", result);
        }

        public async Task<IBusinessResult> GetAll()
        {
            var list = await _unitOfWork.PesticideRepository.GetAllAsync();
            var result = _mapper.Map<List<PesticideModel>>(list);
            return new BusinessResult(1, "List Pesticides", result);
        }

        public async Task<IBusinessResult> GetByFarmOwnerId(int id)
        {
            var orner = await _unitOfWork.FarmOwnerRepository.GetByIdAsync(id);
            if (orner == null) { return new BusinessResult(2, "Do not have this Farm Owner"); }
            var list = await _unitOfWork.PesticideRepository.GetFertilizersByFarmOwnerId(id);
            var result = _mapper.Map<List<Pesticide>>(list);
            return new BusinessResult(1, "List Pesticides by farm ownerid", result);
        }

        public async Task<IBusinessResult> Update(int id, PesticideModel model)
        {
            var obj = await _unitOfWork.PesticideRepository.GetByIdAsync(id);
            if (obj == null) { return new BusinessResult(2, "Not found this Pesticide"); }
            _mapper.Map(model, obj);
            obj.Id = id;
            var result = await _unitOfWork.PesticideRepository.UpdateAsync(obj);
            if (result != 0) { return new BusinessResult(1, "Update Pesticide successfully", obj); }
            else return new BusinessResult(2, "Update Pesticide Fail");
        }
    }
}
