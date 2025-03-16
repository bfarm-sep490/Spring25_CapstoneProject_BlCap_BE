using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Redis;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Fertilizer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Pesticide;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plant;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IFertilizerService
    {
        Task<IBusinessResult> GetAll(string? status);
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Update(int id, FertilizerModel model);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> Create(FertilizerModel model);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
    }
    public class FertilizerService : IFertilizerService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RedisManagement _redisManagement;
        private readonly string key = "ListFertilizers";
        public FertilizerService(IMapper mapper,RedisManagement redisManagement)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
            _redisManagement = redisManagement;
        }

        public async Task<IBusinessResult> Create(FertilizerModel model)
        {
            var obj = _mapper.Map<Fertilizer>(model);
            obj.Status = "Available";
            var fertilizer = await _unitOfWork.FertilizerRepository.CreateAsync(obj);
            var result = _mapper.Map<FertilizerModel>(fertilizer);
            return new BusinessResult(200, "Create Fertilizer successfully", result);
        }
        public async Task<IBusinessResult> DeleteById(int id)
        {
            var obj = await _unitOfWork.FertilizerRepository.GetByIdAsync(id);
            if (obj == null) { return new BusinessResult(404, "Do not have this Fertilizer"); }
            var result = await _unitOfWork.FertilizerRepository.RemoveAsync(obj);
            if (result)
            {
                await this.ResetFertilizersRedis();
                return new BusinessResult(1, "Remove Fertilizer successfully", result);
            }
            return new BusinessResult(200, "Remove Fertilizer fail!", result);
        }

        public async Task<IBusinessResult> GetAll(string? status)
        {
            List<FertilizerModel> result;
            try
            {
                if (_redisManagement.IsConnected)
                {
                    var listJson = _redisManagement.GetData(key);
                    if (!string.IsNullOrEmpty(listJson))
                    {
                        result = JsonConvert.DeserializeObject<List<FertilizerModel>>(listJson);
                        if (!string.IsNullOrEmpty(status))
                        {
                            result = result.Where(f => f.Status.ToLower().Trim() == status.ToLower().Trim()).ToList();
                        }
                        return new BusinessResult(200, "List Fertilizer (From Cache)", result);
                    }
                }
                var list = await _unitOfWork.FertilizerRepository.GetAllAsync();
                result = _mapper.Map<List<FertilizerModel>>(list);

                if (!string.IsNullOrEmpty(status))
                {
                    result = result.Where(f => f.Status.ToLower().Trim() == status.ToLower().Trim()).ToList();
                }

                if (_redisManagement.IsConnected)
                {
                    _redisManagement.SetData(key, JsonConvert.SerializeObject(result));
                }
            }
            catch (Exception ex)
            {
                var list = await _unitOfWork.FertilizerRepository.GetAllAsync();
                result = _mapper.Map<List<FertilizerModel>>(list);

                if (!string.IsNullOrEmpty(status))
                {
                    result = result.Where(f => f.Status.ToLower().Trim() == status.ToLower().Trim()).ToList();
                }
            }
            return new BusinessResult(200, "List Fertilizer", result);
        }
        public async Task<IBusinessResult> GetById(int id)
        {
            var obj = new FertilizerModel();
            try
            {
                if (_redisManagement.IsConnected == false) throw new Exception();
                var listJson = _redisManagement.GetData(key);
                if (!string.IsNullOrEmpty(listJson))
                {
                    var list = JsonConvert.DeserializeObject<List<FertilizerModel>>(listJson);
                    obj = list.FirstOrDefault(x => x.Id == id);
                    if (obj != null) return new BusinessResult(200, "Fertilizer (From Cache)", obj);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                var fertilizer = await _unitOfWork.FertilizerRepository.GetByIdAsync(id);
                if (fertilizer == null) return new BusinessResult(400, "Not Found this Fertilizer");
                obj = _mapper.Map<FertilizerModel>(fertilizer);
            }
            return new BusinessResult(200, "Fertilizer", obj);
        }
        public async Task<IBusinessResult> Update(int id, FertilizerModel model)
        {
            var obj = await _unitOfWork.FertilizerRepository.GetByIdAsync(id);
            if (obj == null) { return new BusinessResult(404, "Not found this Fertilizer"); }
            _mapper.Map(model, obj);
            obj.Id = id;
            var result = await _unitOfWork.FertilizerRepository.UpdateAsync(obj);
            if (result != 0) {
                model.Id = id;
                await  this.ResetFertilizersRedis();
                return new BusinessResult(200, "Update Fertilizer successfully", model);              
            }
            else return new BusinessResult(500, "Update Fertilizer Fail");
        }

        public async Task<IBusinessResult> UploadImage(List<IFormFile> file)
        {
            try
            {
                var image = await CloudinaryHelper.UploadMultipleImages(file);
                var url = image.Select(x => x.Url).ToList();
                return new BusinessResult(200, "Upload success !", url);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        private async Task ResetFertilizersRedis()
        {
            try
            {
                if (_redisManagement.IsConnected == false) throw new Exception("Redis is fail");
                _redisManagement.DeleteData(key);
                var fertilizers = await _unitOfWork.FertilizerRepository.GetAllAsync();
                var list = _mapper.Map<List<FertilizerModel>>(fertilizers);
                _redisManagement.SetData(key, JsonConvert.SerializeObject(list));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
