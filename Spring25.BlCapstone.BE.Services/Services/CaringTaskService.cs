using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.BlockChain;
using Spring25.BlCapstone.BE.Repositories.Dashboards;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Care;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface ICaringTaskService
    {
        Task<IBusinessResult> GetAllCaringTask(int? planId, int? farmerId, int? problemId, List<string>? status);
        Task<IBusinessResult> GetCaringTaskById(int id);
        Task<IBusinessResult> CreateCaringTask(CreateCaringPlan model);
        Task<IBusinessResult> UpdateDetailCaringTask(int id, UpdateCaringTask model);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
        Task<IBusinessResult> DeleteCaringTask(int id);
        Task<IBusinessResult> TaskReport(int id, CaringTaskReport model);
        Task<IBusinessResult> DashboardCaringTasks();
        Task<IBusinessResult> DashboardCaringTasksByPlanId(int id);
        Task<IBusinessResult> GetHistoryFarmers(int id);
        Task<IBusinessResult> GetTypeCaringTasksStatusByPlanId(int id);
        Task<IBusinessResult> ReplaceFarmer(int id, int farmerId, string? reasons);
    }
    public class CaringTaskService : ICaringTaskService
    {
        private readonly UnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly IVechainInteraction _vechainInteraction;
        public CaringTaskService(UnitOfWork unitOfWork, IMapper mapper, IVechainInteraction vechainInteraction)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _vechainInteraction = vechainInteraction;
        }

        public async Task<IBusinessResult> GetAllCaringTask(int? planId, int? farmerId, int? problemId, List<string>? status)
        {
            var list = await _unitOfWork.CaringTaskRepository.GetAllCaringTasks(planId, farmerId, problemId: problemId, status: status);
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

                model.Status = model.Status != null ? model.Status : task.Status;
                _mapper.Map(model, task);
                task.UpdatedAt = DateTime.Now;
                var rs = await _unitOfWork.CaringTaskRepository.UpdateAsync(task);

                if (rs > 0)
                {
                    return new BusinessResult(200, "Update successfull", task);
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
                task.Status = model.Status != null ? model.Status : "Draft";
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
                var caringTask = await _unitOfWork.CaringTaskRepository.GetCaringTaskById(id);
                if (caringTask == null)
                {
                    return new BusinessResult(404, "Not found any Caring Tasks");
                }

                if (model.Status.ToLower().Trim().Equals("complete"))
                {
                    var farmer = await _unitOfWork.FarmerPerformanceRepository.GetFarmerByTaskId(caringTaskId: id);
                    farmer.CompletedTasks += 1;
                    farmer.PerformanceScore = Math.Round((((farmer.CompletedTasks * 1.0) / ((farmer.CompletedTasks * 1.0) + (farmer.IncompleteTasks * 1.0))) * 100), 2);

                    _unitOfWork.FarmerPerformanceRepository.PrepareUpdate(farmer);

                    var blTransaction = await _unitOfWork.PlanTransactionRepository.GetPlanTransactionByTaskId(caringTaskId: id);
                    var task = new DataTask
                    {
                        Description = caringTask.Description,
                        Farmer = caringTask.FarmerCaringTasks.Select(ct => new VeChainFarmer
                        {
                            Id = ct.FarmerId,
                            Name = ct.Farmer.Account.Name,
                        }).FirstOrDefault(),
                        Fertilizers = caringTask.CaringFertilizers.Select(ct => new VeChainItem
                        {
                            Id = ct.Id,
                            Name = ct.Fertilizer.Name,
                            Quantity = ct.Quantity,
                            Unit = ct.Unit,
                        }).ToList(),
                        Pesticides = caringTask.CaringPesticides.Select(ct => new VeChainItem
                        {
                            Id = ct.Id,
                            Name = ct.Pesticide.Name,
                            Quantity = ct.Quantity,
                            Unit = ct.Unit,
                        }).ToList(),
                        Items = caringTask.CaringItems.Select(ct => new VeChainItem
                        {
                            Id = ct.Id,
                            Name = ct.Item != null ? ct.Item.Name : "",
                            Quantity = ct.Quantity,
                            Unit = ct.Unit,
                        }).ToList(),
                        Timestamp = (new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()).ToString()
                    };

                    var result = await _vechainInteraction.CreateNewVechainTask(blTransaction.UrlAddress, new CreateVechainTask
                    {
                        TaskId = id,
                        TaskType = caringTask.TaskType,
                        Status = "Complete",
                        Data = JsonConvert.SerializeObject(task)
                    });
                } 
                else if (model.Status.ToLower().Trim().Equals("incomplete"))
                {
                    var farmer = await _unitOfWork.FarmerPerformanceRepository.GetFarmerByTaskId(caringTaskId: id);
                    farmer.IncompleteTasks += 1;
                    farmer.PerformanceScore = Math.Round((((farmer.CompletedTasks * 1.0) / ((farmer.CompletedTasks * 1.0) + (farmer.IncompleteTasks * 1.0))) * 100), 2);

                    _unitOfWork.FarmerPerformanceRepository.PrepareUpdate(farmer);
                }

                _mapper.Map(model, caringTask);
                caringTask.UpdatedAt = DateTime.Now;
                caringTask.CompleteDate = DateTime.Now;
                await _unitOfWork.CaringTaskRepository.UpdateAsync(caringTask);
                await _unitOfWork.FarmerPerformanceRepository.SaveAsync();

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

                var rs = _mapper.Map<CaringTaskModel>(caringTask);

                return new BusinessResult { Status = 200, Message = "Update successfull", Data = rs };
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

        public async Task<IBusinessResult> GetHistoryFarmers(int id)
        {
            try
            {
                var task = await _unitOfWork.CaringTaskRepository.GetByIdAsync(id);
                if (task == null)
                {
                    return new BusinessResult(404, "Not found any Caring Task");
                }

                var history = await _unitOfWork.FarmerCaringTaskRepository.GetFarmerCaringTasks(id);
                if (history.Count <= 0)
                {
                    return new BusinessResult(404, "There are not any farmers in task !");
                }

                var res = _mapper.Map<List<HistoryFarmersTask>>(history);
                return new BusinessResult(200, "List of history: ", res);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetTypeCaringTasksStatusByPlanId(int id)
        {
            var tasks = await _unitOfWork.CaringTaskRepository.GetTypeTasksStatus(id);
            var result = new TypeTasksStatus();

            foreach (var task in tasks)
            {
                var caringType = new CaringType
                {
                    OnGoingQuantity = task.OnGoingQuantity,
                    PendingQuantity = task.PendingQuantity,
                    InCompleteQuantity = task.InCompleteQuantity,
                    CancelQuantity = task.CancelQuantity,
                    CompleteQuantity = task.CompleteQuantity
                };
                switch (task.Type.ToLower())
                {
                    case "watering":
                        result.Watering = caringType;
                        break;
                    case "nuturing":
                        result.Nuturing = caringType;
                        break;
                    case "planting":
                        result.Planting = caringType;
                        break;
                    case "fertilizer":
                        result.Fertilizer = caringType;
                        break;
                    case "pesticide":
                        result.Pesticide = caringType;
                        break;
                    case "weeding":
                        result.Weeding = caringType;
                        break;
                    case "pruning":
                        result.Pruning = caringType;
                        break;
                }
            }

            return new BusinessResult(200,"Count Type Status by Plan Id",result);
        }

        public async Task<IBusinessResult> ReplaceFarmer(int id, int farmerId, string? reasons)
        {
            try
            {
                var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(farmerId);
                if (farmer == null)
                {
                    return new BusinessResult(404, "Not found any farmer !");
                }

                var caringTask = await _unitOfWork.CaringTaskRepository.GetByIdAsync(id);
                if (caringTask == null)
                {
                    return new BusinessResult(404, "Caring Task not found !");
                }

                var farmerCare = await _unitOfWork.FarmerCaringTaskRepository.GetFarmerCaringTasks(id);
                if (farmerCare.Any(fc => fc.FarmerId == farmerId))
                {
                    return new BusinessResult(400, "This farmer is already assigned to the caring task.");
                }

                farmerCare.ForEach(fc =>
                {
                    if (fc.Status.ToLower().Trim().Equals("active"))
                    {
                        fc.Status = "Inactive";
                        fc.ExpiredDate = DateTime.Now;
                        fc.Description = string.IsNullOrEmpty(reasons) ? null : reasons;
                    }
                    _unitOfWork.FarmerCaringTaskRepository.PrepareUpdate(fc);
                });
                await _unitOfWork.FarmerCaringTaskRepository.SaveAsync();

                await _unitOfWork.FarmerCaringTaskRepository.CreateAsync(new FarmerCaringTask
                {
                    TaskId = id,
                    FarmerId = farmerId,
                    Status = "Active"
                });

                return new BusinessResult(200, "Add Farmer to Caring Task successfully !");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
