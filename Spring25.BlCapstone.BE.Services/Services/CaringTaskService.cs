using AutoMapper;
using Microsoft.AspNetCore.Http;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Care;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface ICaringTaskService
    {
        Task<IBusinessResult> GetAllCaringTask(int? planId);
        Task<IBusinessResult> GetCaringTaskById(int id);
        Task<IBusinessResult> GetDetailCaringTaskById(int id);
        Task<IBusinessResult> UpdateDetailCaringTask(CaringTaskModel result);
        Task<IBusinessResult> UpdateCaringFertilizerModel(CareFertilizerModel result);
        Task<IBusinessResult> UpdateCaringPesticideModel(CarePesticideModel result);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
    }
    public class CaringTaskService : ICaringTaskService
    {
        private readonly UnitOfWork _unitOfWork;
        private IMapper _mapper;
        public CaringTaskService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAllCaringTask(int? planId)
        {
            var list = await _unitOfWork.CaringTaskRepository.GetAllCaringTasks(planId);
            var result = _mapper.Map<List<CaringTaskModel>>(list);
            return new BusinessResult(200, "List caring task", result);
        }

        public async Task<IBusinessResult> GetCaringTaskById(int id)
        {
            var obj = await _unitOfWork.CaringTaskRepository.GetByIdAsync(id);
            if (obj == null) return new BusinessResult(404, "Not found this object", null);
            var result =_mapper.Map<CaringTaskModel>(obj);
            return new BusinessResult(200,"Get caring task by id",result);
        }

        public async Task<IBusinessResult> GetDetailCaringTaskById(int id)
        {
            var obj = await _unitOfWork.CaringTaskRepository.GetDetail(id);
            if (obj == null) return new BusinessResult(404, "Not found this object", null);
            var result = _mapper.Map<CaringTaskModel>(obj);
            return new BusinessResult(200, "Get detail caring task by id", result);
        }

        public Task<IBusinessResult> UpdateCaringFertilizerModel(CareFertilizerModel result)
        {
            throw new NotImplementedException();
        }

        public Task<IBusinessResult> UpdateCaringPesticideModel(CarePesticideModel result)
        {
            throw new NotImplementedException();
        }

        public Task<IBusinessResult> UpdateDetailCaringTask(CaringTaskModel result)
        {
            throw new NotImplementedException();
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
