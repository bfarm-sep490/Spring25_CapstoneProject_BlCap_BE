using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Redis;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Pesticide;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IPesticideService
    {
        Task<IBusinessResult> GetAll(string? status);
        Task<IBusinessResult> Update(int id, PesticideModel model);
        Task<IBusinessResult> Create(PesticideModel model);
        Task<IBusinessResult> Delete(int id);
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
    }
    public class PesticideService : IPesticideService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RedisManagement _redisManagement;
        private readonly string key = "ListPesticides";
        public PesticideService(IMapper mapper,RedisManagement redisManagement)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
            _redisManagement = redisManagement;
        }

        public async Task<IBusinessResult> Create(PesticideModel model)
        {
            var obj = _mapper.Map<Pesticide>(model);
            obj.Status = "Available";
            var pesticide = await _unitOfWork.PesticideRepository.CreateAsync(obj);
            var result = _mapper.Map<PesticideModel>(pesticide);
            await this.ResetPesticidesRedis();
            return new BusinessResult(200, "Create Pesticide successfully", result);
        }

        public async Task<IBusinessResult> Delete(int id)
        {
            var obj = await _unitOfWork.PesticideRepository.GetByIdAsync(id);
            if (obj == null) { return new BusinessResult(404, "Do not have this Pesticide"); }
            var result = await _unitOfWork.PesticideRepository.RemoveAsync(obj);
            if (result) 
            {
                await this.ResetPesticidesRedis();
                return new BusinessResult(200, "Remove Pesticide successfully", result); 
            }
            return new BusinessResult(500, "Remove Pesticide fail!", result);
        }
        public async Task<IBusinessResult> GetAll(string? status)
        {
           var result = new List<PesticideModel>();

            try
            {
                if (!_redisManagement.IsConnected) throw new Exception();
                string productListJson = _redisManagement.GetData(key);
                if (productListJson == null || productListJson == "[]")
                {
                    var list = await _unitOfWork.PesticideRepository.GetAllAsync();
                    result = _mapper.Map<List<PesticideModel>>(list);
                    _redisManagement.SetData(key, JsonConvert.SerializeObject(result));
                }
                else
                {
                    result = JsonConvert.DeserializeObject<List<PesticideModel>>(productListJson);
                }
            }
            catch (Exception ex)
            {
                var list = await _unitOfWork.FertilizerRepository.GetAllAsync();
                result = _mapper.Map<List<PesticideModel>>(list);
            }
            if (!string.IsNullOrEmpty(status))
            {
                    result = result.Where(c => c.Status.ToLower().Trim() == status.ToLower().Trim()).ToList();
            }
            return new BusinessResult(200, "List Pesticides", result);
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            var obj = new PesticideModel();
            try
            {
                if (_redisManagement.IsConnected == false) throw new Exception(); 
                var listJson = _redisManagement.GetData(key);
                if (!string.IsNullOrEmpty(listJson))
                {
                    var list = JsonConvert.DeserializeObject<List<PesticideModel>>(listJson);
                    obj = list.FirstOrDefault(x => x.Id == id);
                    if (obj != null) return new BusinessResult(200, "Pesticide (From Cache)", obj);
                }          
                throw new Exception();
            }
            catch (Exception ex)
            {
                var pesticide = await _unitOfWork.PesticideRepository.GetByIdAsync(id);
                if (pesticide == null) return new BusinessResult(400, "Not Found this Pesticide");
                obj = _mapper.Map<PesticideModel>(pesticide);
            }

            return new BusinessResult(200, "Pesticide", obj);
        }

        public async Task<IBusinessResult> Update(int id, PesticideModel model)
        {
            var obj = await _unitOfWork.PesticideRepository.GetByIdAsync(id);
            if (obj == null) { return new BusinessResult(404, "Not found this Pesticide"); }
            _mapper.Map(model, obj);
            obj.Id = id;
            var result = await _unitOfWork.PesticideRepository.UpdateAsync(obj);
            if (result != 0) 
            { 
                model.Id = id;
                await this.ResetPesticidesRedis();
                return new BusinessResult(200, "Update Pesticide successfully", model); 
        }
            else return new BusinessResult(500, "Update Pesticide Fail");
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
        private async Task ResetPesticidesRedis()
        {
            try
            {
                if (_redisManagement.IsConnected == false) throw new Exception("Redis is fail");
                _redisManagement.DeleteData(key);
                var pesticides = await _unitOfWork.PesticideRepository.GetAllAsync();
                var list = _mapper.Map<List<Pesticide>>(pesticides);
                _redisManagement.SetData(key, JsonConvert.SerializeObject(list));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
