using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Redis;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plant;
using Spring25.BlCapstone.BE.Services.BusinessModels.Yield;
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
        Task<IBusinessResult> GetAll(string? status, string? name);
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Create(PlantModel model);
        Task<IBusinessResult> Update(int id,PlantModel model);
        Task<IBusinessResult> Delete(int id);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
        Task<IBusinessResult> GetSuggestYieldsbyPlantId(int id);
        Task<IBusinessResult> DeleteSuggestYields(int id, int yieldId);
        Task<IBusinessResult> CreateSuggestYields(int id, List<PlantYieldModel> model);
    }

    public class PlantService : IPlantService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RedisManagement _redisManagement;
        private readonly string key = "ListPlants";
        public PlantService(UnitOfWork unitOfWork,IMapper mapper, RedisManagement redis)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _redisManagement = redis;
        }

        public async Task<IBusinessResult> Create(PlantModel model)
        {   
           var obj = _mapper.Map<Plant>(model);
           obj.Status = "Available";
           var plant = await _unitOfWork.PlantRepository.CreateAsync(obj);
           await this.ResetPlantsRedis();
           var result = _mapper.Map<PlantModel>(plant);
           return new BusinessResult(200,"Create successfully",result);
        }

        public async Task<IBusinessResult> CreateSuggestYields(int id, List<PlantYieldModel> model)
        {
            foreach (var plantYield in model)
            {
                var suggest = await _unitOfWork.PlantYieldRepository.GetPlantYield(plantYield.YieldId, id);
                if (suggest != null)
                {
                    return new BusinessResult(400, $"This Yield has already been suggested in this plant, yield id: {plantYield.YieldId}");
                }
            }
            
            foreach (var plantYield in model)
            {
                _unitOfWork.PlantYieldRepository.PrepareCreate(new PlantYield
                {
                    PlantId = id,
                    YieldId = plantYield.YieldId,
                    MaximumQuantity = plantYield.MaximumQuantity
                });
            }
            await _unitOfWork.PlantYieldRepository.SaveAsync();

            return new BusinessResult(200, "Add successfully.");
        }

        public async Task<IBusinessResult> Delete(int id)
        {
            var obj = await _unitOfWork.PlantRepository.GetByIdAsync(id);
            if (obj == null) return new BusinessResult(400, "Not found this Plant", null);
            var result = await _unitOfWork.PlantRepository.RemoveAsync(obj);
            await this.ResetPlantsRedis();
            return new BusinessResult(200, "Remove successfully", result);
        }

        public async Task<IBusinessResult> DeleteSuggestYields(int id, int yieldId)
        {
            var obj = await _unitOfWork.PlantYieldRepository.GetPlantYield(yieldId, id);
            if (obj == null) { return new BusinessResult(404, "Not Found"); }
            await _unitOfWork.PlantYieldRepository.DeletePlantYield(obj);
            return new BusinessResult(200, "Removed successfully.");
        }

        public async Task<IBusinessResult> GetAll(string? status, string? name)
        {
            var result = new List<PlantModel>();
            try 
            {
                if (_redisManagement.IsConnected == false) throw new Exception(); 
                string productListJson = _redisManagement.GetData(key);          
                if (productListJson == null || productListJson == "[]")
                {
                    var list = await _unitOfWork.PlantRepository.GetAllAsync();
                    result = _mapper.Map<List<PlantModel>>(list);
                    _redisManagement.SetData(key, JsonConvert.SerializeObject(result));
                }
                else
                {
                    result = JsonConvert.DeserializeObject<List<PlantModel>>(productListJson);
                }
            }
            catch(Exception ex)  
            {
                var list = await _unitOfWork.PlantRepository.GetAllAsync();
                result = _mapper.Map<List<PlantModel>>(list);
            }

            if (status != null)
            {
                result = result.Where(x => x.Status.ToLower() == status.ToLower()).ToList();
            }

            if (name != null)
            {
                result = result.Where(x => x.PlantName.ToLower().Trim().Contains(name.ToLower().Trim())).ToList();
            }

            return new BusinessResult(200, "Get all plants", result);
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            var obj = new PlantModel();
            try
            {
                if (_redisManagement.IsConnected == false) throw new Exception();
                var listJson = _redisManagement.GetData(key);
                if (!string.IsNullOrEmpty(listJson))
                {
                    var list = JsonConvert.DeserializeObject<List<PlantModel>>(listJson);
                    obj = list.FirstOrDefault(x => x.Id == id);
                    if (obj != null) return new BusinessResult(200, "Plant (From Cache)", obj);
                }
                throw new Exception();
            }
            catch (Exception ex)
            {
                var plant = await _unitOfWork.PlantRepository.GetByIdAsync(id);
                if (plant == null) return new BusinessResult(400, "Not Found this Plant");
                obj = _mapper.Map<PlantModel>(plant);
            }
            return new BusinessResult(200, "Get Plant by Id", obj);
        }

        public async Task<IBusinessResult> GetSuggestYieldsbyPlantId(int id)
        {
            var obj = await _unitOfWork.PlantRepository.GetByIdAsync(id);
            if (obj == null)
            {
                return new BusinessResult(400, "Not Found this Plant");
            }

            var list = await _unitOfWork.PlantRepository.GetSuggestPlantById(id);
            var result = _mapper.Map<List<YieldSuggestionModel>>(list);
            return new BusinessResult(200, "Get suggest yields by plantid", result);
        }

        public async Task<IBusinessResult> Update(int id, PlantModel model)
        {
            var obj = await _unitOfWork.PlantRepository.GetByIdAsync(id);
            if (obj == null) return new BusinessResult(400, "Not found this Plant", null);
            _mapper.Map(model,obj);
            obj.Id = id;
            var result = await _unitOfWork.PlantRepository.UpdateAsync(obj);
            model.Id = id;
            await this.ResetPlantsRedis();
            return new BusinessResult(200,"Update successfully",model);
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
        private async Task ResetPlantsRedis()
        {
            try
            {
                if (_redisManagement.IsConnected == false) throw new Exception("Redis is fail");
                _redisManagement.DeleteData(key);
                var plants = await _unitOfWork.PlantRepository.GetAllAsync();
                var list = _mapper.Map<List<PlantModel>>(plants);
                _redisManagement.SetData(key, JsonConvert.SerializeObject(list));
            }
            catch(Exception ex)
            {
                
            }
        }
    }
}
