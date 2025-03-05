using AutoMapper;
using Microsoft.AspNetCore.Http;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
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
        Task<IBusinessResult> GetAllCaringTask(int? planId, int? farmerId);
        Task<IBusinessResult> GetCaringTaskById(int id);
        Task<IBusinessResult> GetDetailCaringTaskById(int id);
        Task<IBusinessResult> CreateCaringTask(CreateCaringPlan model);
        Task<IBusinessResult> UpdateDetailCaringTask(int id, UpdateCaringTask model);
        Task<IBusinessResult> UpdateCaringFertilizerModel(CareFertilizerModel result);
        Task<IBusinessResult> UpdateCaringPesticideModel(CarePesticideModel result);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
        Task<IBusinessResult> DeleteCaringTask(int id);
        Task<IBusinessResult> TaskReport(int id, CaringTaskReport model);
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

        public async Task<IBusinessResult> GetAllCaringTask(int? planId, int? farmerId)
        {
            var list = await _unitOfWork.CaringTaskRepository.GetAllCaringTasks(planId, farmerId);
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

        public async Task<IBusinessResult> UpdateDetailCaringTask(int id, UpdateCaringTask model)
        {
            try
            {
                var task = await _unitOfWork.CaringTaskRepository.GetByIdAsync(id);
                if (task == null)
                {
                    return new BusinessResult(404, "Not found any caring task");
                }

                var items = await _unitOfWork.CaringItemRepository.GetCaringItemsByTaskId(id);
                foreach (var item in items)
                {
                    await _unitOfWork.CaringItemRepository.RemoveAsync(item);
                }

                var pes = await _unitOfWork.CaringPesticideRepository.GetCaringPesticidesByTaskId(id);
                foreach (var p in pes)
                {
                    await _unitOfWork.CaringPesticideRepository.RemoveAsync(p);
                }

                var fer = await _unitOfWork.CaringFertilizerRepository.GetCaringFertilizersByTaskId(id);
                foreach (var f in fer)
                {
                    await _unitOfWork.CaringFertilizerRepository.RemoveAsync(f);
                }

                if (model.Fertilizers != null)
                {
                    foreach (var f in model.Fertilizers)
                    {
                        await _unitOfWork.CaringFertilizerRepository.CreateAsync(new CaringFertilizer
                        {
                            TaskId = task.Id,
                            FertilizerId = f.FertilizerId,
                            Quantity = f.Quantity,
                            Unit = f.Unit,
                        });
                    }
                }

                if (model.Pesticides != null)
                {
                    foreach (var p in model.Pesticides)
                    {
                        await _unitOfWork.CaringPesticideRepository.CreateAsync(new CaringPesticide
                        {
                            TaskId = task.Id,
                            PesticideId = p.PesticideId,
                            Quantity = p.Quantity,
                            Unit = p.Unit,
                        });
                    }
                }

                if (model.Items != null)
                {
                    foreach (var i in model.Items)
                    {
                        await _unitOfWork.CaringItemRepository.CreateAsync(new CaringItem
                        {
                            TaskId = task.Id,
                            ItemId = i.ItemId,
                            Quantity = i.Quantity,
                            Unit = i.Unit,
                        });
                    }
                }

                _mapper.Map(model, task);
                task.UpdatedAt = DateTime.Now;
                var rs = await _unitOfWork.CaringTaskRepository.UpdateAsync(task);

                if (rs != null)
                {
                    return new BusinessResult(200, "Update successfull", rs);
                }
                else
                {
                    return new BusinessResult(500, "Update failed!");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
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

        public async Task<IBusinessResult> CreateCaringTask(CreateCaringPlan model)
        {
            try
            {
                var task = _mapper.Map<CaringTask>(model);
                task.IsCompleted = false;
                task.IsAvailable = true;
                task.Priority = 0;
                task.Status = "Draft";
                task.CreatedAt = DateTime.Now;

                var rs = await _unitOfWork.CaringTaskRepository.CreateAsync(task);
                if (model.Fertilizers != null)
                {
                    foreach (var f in model.Fertilizers)
                    {
                        await _unitOfWork.CaringFertilizerRepository.CreateAsync(new CaringFertilizer
                        {
                            TaskId = task.Id,
                            FertilizerId = f.FertilizerId,
                            Quantity = f.Quantity,
                            Unit = f.Unit,
                        });
                    }
                }

                if (model.Pesticides != null)
                {
                    foreach (var p in model.Pesticides)
                    {
                        await _unitOfWork.CaringPesticideRepository.CreateAsync(new CaringPesticide
                        {
                            TaskId = task.Id,
                            PesticideId = p.PesticideId,
                            Quantity = p.Quantity,
                            Unit = p.Unit,
                        });
                    }
                }

                if (model.Items != null)
                {
                    foreach (var i in model.Items)
                    {
                        await _unitOfWork.CaringItemRepository.CreateAsync(new CaringItem
                        {
                            TaskId = task.Id,
                            ItemId = i.ItemId,
                            Quantity = i.Quantity,
                            Unit = i.Unit,
                        });
                    }
                }

                if (rs != null)
                {
                    return new BusinessResult(200, "Create task successfull", rs);
                }
                else
                {
                    return new BusinessResult(500, "Create failed!");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> DeleteCaringTask(int id)
        {
            try
            {
                var caringTask = await _unitOfWork.CaringTaskRepository.GetByIdAsync(id);
                if (caringTask == null)
                {
                    return new BusinessResult(404, "Not found any Caring Tasks");   
                }

                var caringFers = await _unitOfWork.CaringFertilizerRepository.GetCaringFertilizersByTaskId(id);
                foreach (var fer in caringFers)
                {
                    await _unitOfWork.CaringFertilizerRepository.RemoveAsync(fer);
                }

                var caringPes = await _unitOfWork.CaringPesticideRepository.GetCaringPesticidesByTaskId(id);
                foreach (var pes in caringPes)
                {
                    await _unitOfWork.CaringPesticideRepository.RemoveAsync(pes);
                }

                var caringItems = await _unitOfWork.CaringItemRepository.GetCaringItemsByTaskId(id);
                foreach (var item in caringItems)
                {
                    await _unitOfWork.CaringItemRepository.RemoveAsync(item);
                }

                var rs = await _unitOfWork.CaringTaskRepository.RemoveAsync(caringTask);
                if (rs)
                {
                    return new BusinessResult(200, "Remove caring task successfully !");
                }
                else
                {
                    return new BusinessResult(500, "Remove failed!");
                }

            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> TaskReport(int id, CaringTaskReport model)
        {
            try
            {
                var caringTask = await _unitOfWork.CaringTaskRepository.GetByIdAsync(id);
                if (caringTask == null)
                {
                    return new BusinessResult(404, "Not found any Caring Tasks");
                }

                _mapper.Map(model, caringTask);
                caringTask.UpdatedAt = DateTime.Now;
                await _unitOfWork.CaringTaskRepository.UpdateAsync(caringTask);

                var images = await _unitOfWork.CaringImageRepository.GetCaringImagesByTaskId(id);
                if (!images.Any() || images.Count > 0)
                {
                    foreach (var image in images)
                    {
                        await _unitOfWork.CaringImageRepository.RemoveAsync(image);
                    }
                }

                if (model.Images != null && model.Images.Any())
                {
                    foreach (var image in model.Images)
                    {
                        await _unitOfWork.CaringImageRepository.CreateAsync(new Repositories.Models.CaringImage
                        {
                            TaskId = id,
                            Url = image
                        });
                    }
                }

                return new BusinessResult { Status = 200, Message = "Update successfull", Data = caringTask };
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
