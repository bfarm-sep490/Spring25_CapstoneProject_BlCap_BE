using AutoMapper;
using Microsoft.AspNetCore.Http;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Dashboards;
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
        Task<IBusinessResult> CreateCaringTask(CreateCaringPlan model);
        Task<IBusinessResult> UpdateDetailCaringTask(int id, UpdateCaringTask model);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
        Task<IBusinessResult> DeleteCaringTask(int id);
        Task<IBusinessResult> TaskReport(int id, CaringTaskReport model);
        Task<IBusinessResult> DashboardCaringTasks();
        Task<IBusinessResult> DashboardCaringTasksByPlanId(int id);
        Task<IBusinessResult> GetInfomationOfFertilizerTasksByPlanId(int id);
        Task<IBusinessResult> GetInfomationOfPesticideTasksByPlanId(int id);
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
            var obj = await _unitOfWork.CaringTaskRepository.GetAllCaringTasks(taskId: id);
            if (obj.Count <= 0) return new BusinessResult(404, "Not found caring tasks !", null);
            var result = _mapper.Map<List<CaringTaskModel>>(obj);
            return new BusinessResult(200,"Get caring task by id", result);
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
                caringTask.CompleteDate = DateTime.Now;
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

        public async Task<IBusinessResult> DashboardCaringTasks()
        {
            var obj = await _unitOfWork.CaringTaskRepository.GetDashboardCaringTasks();
            var data = new List<AdminData>();
            if (obj != null && obj.Count >= 1)
            {
                for (var i = DateTime.Now.Date; i.Date >= obj.Min(x => x.Date); i = i.AddDays(-1))
                {
                    if (!obj.Any(x => x.Date == i.Date))
                    {
                        data.Add(new AdminData { Date = i.Date, Value = 0 });
                    }
                }
                foreach (var task in obj)
                {
                    data.Add(new AdminData { Date = task.Date, Value = task.Value });
                }
                data = data.OrderBy(c => c.Date).ToList();

            }
            return new BusinessResult(200, "Dashboard Caring Tasks", data);
        }

        public async Task<IBusinessResult> DashboardCaringTasksByPlanId(int id)
        {
            var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);
            if (plan == null) return new BusinessResult(200, "Dashboard Caring Tasks by plan id", null);
            var obj = await _unitOfWork.CaringTaskRepository.GetDashboardCaringTasksByPlanId(id);
            var data = new List<AdminData>();
            if (obj != null && obj.Count >= 1)
            {
                for (var i = DateTime.Now.Date; i.Date >= obj.Min(x => x.Date); i = i.AddDays(-1))
                {
                    if (!obj.Any(x => x.Date == i.Date))
                    {
                        data.Add(new AdminData { Date = i.Date, Value = 0 });
                    }
                }
                foreach (var task in obj)
                {
                    data.Add(new AdminData { Date = task.Date, Value = task.Value });
                }
                data = data.OrderBy(c => c.Date).ToList();

            }
            return new BusinessResult(200, "Dashboard Caring Tasks by plan id", data);
        }

        public async Task<IBusinessResult> GetInfomationOfFertilizerTasksByPlanId(int id)
        {
            var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);
            if (plan == null) return new BusinessResult(200, "Dashboard Caring Tasks by plan id", null);
            var result = new List<NurturingItem>();
            var list = await _unitOfWork.CaringFertilizerRepository.GetFertilizersByPlanId(id);
            foreach (var item in list) { 
                var obj = new NurturingItem();
                obj.Id = item.Id;
                obj.Name = item.Name;
                obj.EstimateQuantity = await _unitOfWork.CaringFertilizerRepository.EstimateFertilizerInPlan(id,item.Id);
                obj.UsedQuantity = await _unitOfWork.CaringFertilizerRepository.UsedFertilizerInPlan(id,item.Id);
                obj.Unit = "Kg";
                result.Add(obj);
            }
            return new BusinessResult(200, "Get Infomation Of Fertilizers Tasks By PlanId", result);
        } 
        public async Task<IBusinessResult> GetInfomationOfPesticideTasksByPlanId(int id)
        {
            var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);
            if (plan == null) return new BusinessResult(200, "Dashboard Caring Tasks by plan id", null);
            var result = new List<NurturingItem>();
            var list = await _unitOfWork.CaringFertilizerRepository.GetPesticidesByPlanId(id);
            foreach (var item in list) { 
                var obj = new NurturingItem();
                obj.Id = item.Id;
                obj.Name = item.Name;
                obj.EstimateQuantity = await _unitOfWork.CaringPesticideRepository.EstimatePesticideInPlan(id,item.Id);
                obj.UsedQuantity = await _unitOfWork.CaringPesticideRepository.UsedPesticideInPlan(id,item.Id);
                obj.Unit = "Kg";
                result.Add(obj);
            }
            return new BusinessResult(200, "Get Infomation Of Pesticide Tasks By PlanId", result);
        }

    }
}
