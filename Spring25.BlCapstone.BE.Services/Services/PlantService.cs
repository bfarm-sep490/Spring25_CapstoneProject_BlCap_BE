using AutoMapper;
using IO.Ably;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Redis;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plan;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plant;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IPlantService
    {
        Task<IBusinessResult> GetAll(bool? isAvailable);
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Create(PlantModel model);
        Task<IBusinessResult> Update(int id,PlantModel model);
        Task<IBusinessResult> Delete(int id);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
    }

    public class PlantService : IPlantService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RedisManagement _redisManagerment;
        public PlantService(UnitOfWork unitOfWork,IMapper mapper, RedisManagement redis)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _redisManagerment = redis;
        }

        public async Task<IBusinessResult> Create(PlantModel model)
        {
           CheckModel(model);
           var obj = _mapper.Map<Plant>(model);
           var result = await _unitOfWork.PlantRepository.CreateAsync(obj);
           return new BusinessResult(200,"Create successfully",result);
        }

        public async Task<IBusinessResult> Delete(int id)
        {
            var obj = await _unitOfWork.PlantRepository.GetByIdAsync(id);
            if (obj == null) return new BusinessResult(400, "Not found this Plant", null);
            var result = await _unitOfWork.PlantRepository.RemoveAsync(obj);
            return new BusinessResult(200, "Remove successfully", result);
        }

        public async Task<IBusinessResult> GetAll()
        {
      
           var key = "ListPlants";
           string productListJson = _redisManagerment.GetData(key);
           var result = new List<PlantModel>();
           if (productListJson == null || productListJson == "[]")
            {
                var list = await _unitOfWork.PlantRepository.GetAllAsync();
                result = _mapper.Map<List<PlantModel>>(list);
                productListJson = JsonConvert.SerializeObject(result);
                _redisManagerment.SetData(key, productListJson);
            }
            else
            {
                result = JsonConvert.DeserializeObject<List<PlantModel>>(productListJson);
            }

            return new BusinessResult(200, "Get all plants", result);
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            var obj = await _unitOfWork.PlantRepository.GetByIdAsync(id);
            var result = _mapper.Map<PlantModel>(obj);
            return new BusinessResult(200, "Get Plant by Id", result);
        }

        public async Task<IBusinessResult> Update(int id, PlantModel model)
        {
           CheckModel(model);
            var obj = await _unitOfWork.PlantRepository.GetByIdAsync(id);
            if (obj == null) return new BusinessResult(400, "Not found this Plant", null);
            _mapper.Map(model,obj);
            obj.Id = id;
            var result = await _unitOfWork.PlantRepository.UpdateAsync(obj);
            return new BusinessResult(200,"Update successfully",obj);

        }
        private void CheckModel(PlantModel model)
        {
            if (model.MinMoisture >= model.MaxMoisture) { throw new Exception("MinMoisture must be < MaxMoistrure"); }
            if (model.MinPesticide >= model.MaxPesticide) { throw new Exception("MinPesticide must be < MaxPesticide"); }
            if (model.MinBrixPoint >= model.MaxBrixPoint) { throw new Exception("MinBrixPoint must be < MaxBrixPoint"); }
            if (model.MinHumid >= model.MaxHumid) { throw new Exception("MinHumid must be < MaxHumicPoint"); }
            if (model.MinFertilizer >= model.MaxFertilizer) { throw new Exception("MinFertilizer must be < MaxFertilizer"); }
            if (model.MinTemp >= model.MaxTemp) { throw new Exception("MinTemp must be < MaxTemp"); }
        }

        public async Task<IBusinessResult> UploadImage(List<IFormFile> file)
        {
            try
            {
                var image = await CloudinaryHelper.UploadMultipleImages(file);
                var url = image.Select(x => x.Url).ToList();

                return new BusinessResult
                {
                    Status = 200,
                    Message = "Upload success !",
                    Data = url
                };
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
