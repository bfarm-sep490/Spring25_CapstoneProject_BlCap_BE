using AutoMapper;
using Microsoft.AspNetCore.Http;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Dashboards;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Harvest;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Havest;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IHarvestingTaskService
    {
        Task<IBusinessResult> GetHarvestingTasks(int? planId, int? farmerId);
        Task<IBusinessResult> GetHarvestingTaskById(int id);
        Task<IBusinessResult> GetHarvestingTaskDetailById(int id);
        Task<IBusinessResult> CreateHarvestingTask(CreateHarvestingPlan model);
        Task<IBusinessResult> ReportHarvestingTask(int id, HarvestingTaskReport model);
        Task<IBusinessResult> UpdateTask(int id, UpdateHarvestingTask model);
        Task<IBusinessResult> DeleteHarvestingTask(int id);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
        Task<IBusinessResult> DashboardHarvest();
        Task<IBusinessResult> DeleteTask(int id);
        Task<IBusinessResult> DashboardHarvestByPlanId(int id);
        Task<IBusinessResult> GetHavestedTasksDashboardByPlanId(int id);
    }
    public class HarvestingTaskService : IHarvestingTaskService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public HarvestingTaskService(IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IBusinessResult> CreateHarvestingTask(CreateHarvestingPlan model)
        {
            try
            {
                var task = _mapper.Map<HarvestingTask>(model);
                task.IsAvailable = true;
                task.Status = "Draft";
                task.Priority = 0;
                task.CreatedAt = DateTime.Now;

                var rs = await _unitOfWork.HarvestingTaskRepository.CreateAsync(task);
                if (model.Items != null)
                {
                    foreach (var i in model.Items)
                    {
                        await _unitOfWork.HarvestingItemRepository.CreateAsync(new HarvestingItem
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

        public async Task<IBusinessResult> DashboardHarvest()
        {
            var obj = await _unitOfWork.HarvestingTaskRepository.GetDashboardHarvestingTasks();
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
                foreach(var task in obj)
                {
                    data.Add(new AdminData { Date = task.Date, Value = task.Value });
                }
                data = data.OrderBy(c => c.Date).ToList();

            }
            return new BusinessResult(200, "Dashboard Harvesting Tasks", data);
        }
        public Task<IBusinessResult> DeleteHarvestingTask(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IBusinessResult> GetHarvestingTaskById(int id)
        {
            var obj = await _unitOfWork.HarvestingTaskRepository.GetByIdAsync(id);
            if (obj == null) return new BusinessResult(400, "Not found this id");
            var result = _mapper.Map<HarvestingTaskModel>(obj);
            return new BusinessResult(200, "Get harvesting task by id", result);
        }

        public async Task<IBusinessResult> GetHarvestingTaskDetailById(int id)
        {
            var obj = await _unitOfWork.HarvestingTaskRepository.GetHarvestingTaskById(id);
            if (obj == null) return new BusinessResult(400, "Not found this id");
            var result = _mapper.Map<HarvestingTaskModel>(obj);
            return new BusinessResult(200, "Get detail harvesting task by id", result);
        }

        public async Task<IBusinessResult> GetHarvestingTasks(int? planId, int? farmerId)
        {
            var obj = await _unitOfWork.HarvestingTaskRepository.GetHarvestingTasks(planId, farmerId);
            var result = _mapper.Map<List<HarvestingTaskModel>>(obj);
            return new BusinessResult(200, "Get harvesting tasks", result);
        }

        public async Task<IBusinessResult> ReportHarvestingTask(int id, HarvestingTaskReport model)
        {
            try
            {
                var harvestingTask = await _unitOfWork.HarvestingTaskRepository.GetByIdAsync(id);
                if (harvestingTask == null)
                {
                    return new BusinessResult { Status = 404, Message = "Not found any Harvesting Task", Data = null };
                }

                _mapper.Map(model, harvestingTask);
                harvestingTask.UpdatedAt = DateTime.Now;
                await _unitOfWork.HarvestingTaskRepository.UpdateAsync(harvestingTask);

                var images = await _unitOfWork.HarvestingImageRepository.GetHarvestingImagesByTaskId(id);
                if (!images.Any() || images.Count > 0)
                {
                    foreach (var image in images)
                    {
                        await _unitOfWork.HarvestingImageRepository.RemoveAsync(image);
                    }
                }

                if (model.Images != null && model.Images.Any())
                {
                    foreach (var image in model.Images)
                    {
                        await _unitOfWork.HarvestingImageRepository.CreateAsync(new Repositories.Models.HarvestingImage
                        {
                            TaskId = id,
                            Url = image
                        });
                    }
                }

                return new BusinessResult { Status = 200, Message = "Update successfull", Data = harvestingTask };
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message };
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

        public async Task<IBusinessResult> UpdateTask(int id, UpdateHarvestingTask model)
        {
            try
            {
                var task = await _unitOfWork.HarvestingTaskRepository.GetByIdAsync(id);
                if (task == null)
                {
                    return new BusinessResult(404, "Not found any harvesting tasks");
                }

                _mapper.Map(model, task);
                task.UpdatedAt = DateTime.Now;
                var rs = await _unitOfWork.HarvestingTaskRepository.UpdateAsync(task);

                var items = await _unitOfWork.HarvestingItemRepository.GetHarvestingItemsByTaskId(id);
                foreach ( var item in items )
                {
                    await _unitOfWork.HarvestingItemRepository.RemoveAsync(item);
                }

                if (model.Items != null)
                {
                    foreach (var i in model.Items)
                    {
                        await _unitOfWork.HarvestingItemRepository.CreateAsync(new HarvestingItem
                        {
                            TaskId = task.Id,
                            ItemId = i.ItemId,
                            Quantity = i.Quantity,
                            Unit = i.Unit,
                        });
                    }
                }

                if (rs > 0)
                {
                    return new BusinessResult(200, "Update successfull", task);
                } else
                {
                    return new BusinessResult(500, "Update failed !");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> DeleteTask(int id)
        {
            try
            {
                var harvestingTask = await _unitOfWork.HarvestingTaskRepository.GetByIdAsync(id);
                if (harvestingTask == null)
                {
                    return new BusinessResult(404, "Not found any harvesting task!");
                }

                var harvestingItems = await _unitOfWork.HarvestingItemRepository.GetHarvestingItemsByTaskId(id);
                foreach (var item in harvestingItems)
                {
                    await _unitOfWork.HarvestingItemRepository.RemoveAsync(item);
                }

                var rs = await _unitOfWork.HarvestingTaskRepository.RemoveAsync(harvestingTask);
                if (rs)
                {
                    return new BusinessResult(200, "Remove successfully!");
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

        public async Task<IBusinessResult> DashboardHarvestByPlanId(int id)
        {
            var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);
            if (plan == null) return new BusinessResult(200, "Dashboard Caring Tasks by plan id", null);
            var obj = await _unitOfWork.HarvestingTaskRepository.GetDashboardHarvestingTasksByPlanId(id);
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
            return new BusinessResult(200, "Dashboard Harvesting Tasks by Plan Id", data);
        }

        public async Task<IBusinessResult> GetHavestedTasksDashboardByPlanId(int id)
        {
            var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);
            if (plan == null) return new BusinessResult(200, "Dashboard Caring Tasks by plan id", null);
            var result = await _unitOfWork.HarvestingTaskRepository.GetHavestedTaskDashboardByPlanId(plan.Id);
            return new BusinessResult(200,"Get Havested Task by Plan Id",result);
        }
    }
}
