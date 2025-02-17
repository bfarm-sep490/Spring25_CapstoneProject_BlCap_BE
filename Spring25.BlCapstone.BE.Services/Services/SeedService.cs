using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface ISeedService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Create(SeedModel model);
        Task<IBusinessResult> Update(int id,SeedModel model);
        Task<IBusinessResult> Delete(int id);
        
    }
    public class SeedService:ISeedService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SeedService(UnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IBusinessResult> Create(SeedModel model)
        {
           CheckModel(model);
           var obj = _mapper.Map<Plant>(model);
           var result = await _unitOfWork.SeedRepository.CreateAsync(obj);
           return new BusinessResult(200,"Create successfully",result);
        }

        public async Task<IBusinessResult> Delete(int id)
        {
            var obj = await _unitOfWork.SeedRepository.GetByIdAsync(id);
            if (obj == null) return new BusinessResult(400, "Not found this Seed", null);
            var result = await _unitOfWork.SeedRepository.RemoveAsync(obj);
            return new BusinessResult(200, "Remove successfully", result);
        }

        public async Task<IBusinessResult> GetAll()
        {
           var list = await _unitOfWork.SeedRepository.GetAllAsync();
           var result = _mapper.Map<List<SeedModel>>(list);
           return new BusinessResult(200, "Get all seeds", result);
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            var obj = await _unitOfWork.SeedRepository.GetByIdAsync(id);
            var result = _mapper.Map<SeedModel>(obj);
            return new BusinessResult(200, "Get Seed by Id", result);
        }

        public async Task<IBusinessResult> Update(int id, SeedModel model)
        {
           CheckModel(model);
            var obj = await _unitOfWork.SeedRepository.GetByIdAsync(id);
            if (obj == null) return new BusinessResult(400, "Not found this Seed", null);
            _mapper.Map(model,obj);
            obj.Id = id;
            var result = await _unitOfWork.SeedRepository.UpdateAsync(obj);
            return new BusinessResult(200,"Update successfully",obj);

        }
        private void CheckModel(SeedModel model)
        {
            if (model.MinMoisture >= model.MaxMoisture) { throw new Exception("MinMoisture must be < MaxMoistrure"); }
            if (model.MinPesticide >= model.MaxPesticide) { throw new Exception("MinPesticide must be < MaxPesticide"); }
            if (model.MinBrixPoint >= model.MaxBrixPoint) { throw new Exception("MinBrixPoint must be < MaxBrixPoint"); }
            if (model.MinHumid >= model.MaxHumid) { throw new Exception("MinHumid must be < MaxHumicPoint"); }
            if (model.MinFertilizer >= model.MaxFertilizer) { throw new Exception("MinFertilizer must be < MaxFertilizer"); }
            if (model.MinTemp >= model.MaxTemp) { throw new Exception("MinTemp must be < MaxTemp"); }
        }
    }
}
