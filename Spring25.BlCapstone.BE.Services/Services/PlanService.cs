using AutoMapper;
using IO.Ably;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Dashboards;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Dashboard;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Item;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plan;
using Spring25.BlCapstone.BE.Services.BusinessModels.Problem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IPlanService
    {
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> GetAll(int? expertId, string? status);
        Task<IBusinessResult> GetGeneralPlan(int id);
        Task<IBusinessResult> GetAllProblems(int planId);
        Task<IBusinessResult> GetAllFarmers(int planId);
        Task<IBusinessResult> GetAllItems(int planId);
        Task<IBusinessResult> AssignTasks(int id, AssigningPlan model);
        Task<IBusinessResult> ApprovePlan(int id);
        Task<IBusinessResult> Create(CreatePlan model);
        Task<IBusinessResult> UpdatePlan(int id, UpdatePlan model);
        Task<IBusinessResult> UpdateStatus(int id, string status, string? reportBy);
        Task<IBusinessResult> DeletePlan(int id);
        Task<IBusinessResult> GetStatusTasksDashboardByPlanId(int id);
        Task<IBusinessResult> GetAllPlanFarmerAssigned(int id);
        Task<IBusinessResult> GetInfomationOfFertilizerTasksByPlanId(int id);
        Task<IBusinessResult> GetInfomationOfPesticideTasksByPlanId(int id);
        Task<IBusinessResult> RemoveFarmerFromPlan(int planId, int farmerId);
        Task<IBusinessResult> AddFarmerToPlan(int planId, int farmerId);
        Task<IBusinessResult> GetCountTasksByPlanId(int id);
    }

    public class PlanService : IPlanService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PlanService(IMapper mapper)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetPlan(id);

                var res = _mapper.Map<PlanModel>(plan);
                if (res != null)
                {
                    return new BusinessResult(200, "Plan ne", res);
                }

                return new BusinessResult(404, "Not found any Plans", null);
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

        public async Task<IBusinessResult> GetAll(int? expertId, string? status)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetAllPlans(expertId, status);

                var rs = _mapper.Map<List<PlanForList>>(plan);
                if (rs != null)
                {
                    return new BusinessResult(200, "List of Plans", rs);
                }

                return new BusinessResult(404, "Not found any Plans", null);
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

        public async Task<IBusinessResult> GetGeneralPlan(int id)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetPlan(id);

                var res = _mapper.Map<PlanGeneral>(plan);
                if (res != null)
                {
                    return new BusinessResult(200, "Plan ne", res);
                }

                return new BusinessResult(404, "Not found any Plans", null);
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }

        public async Task<IBusinessResult> GetAllProblems(int planId)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(planId);
                if (plan == null)
                {
                    return new BusinessResult { Status = 404, Message = "Plan not existed !", Data = null };
                }

                var probs = await _unitOfWork.ProblemRepository.GetProblemByPlanId(planId);

                var res = _mapper.Map<List<ProblemPlan>>(probs);
                if (probs.Count > 0)
                {
                    return new BusinessResult { Status = 200, Message = "Problems ne!", Data = res };
                }

                return new BusinessResult { Status = 404, Message = "Not found any Problems in plan", Data = null };
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }

        public async Task<IBusinessResult> GetAllFarmers(int planId)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(planId);
                if (plan == null)
                {
                    return new BusinessResult { Status = 404, Message = "Plan not existed !", Data = null };
                }

                var farmers = await _unitOfWork.FarmerRepository.GetFarmersByPlanId(planId);

                var res = _mapper.Map<List<FarmerPlan>>(farmers);
                if (farmers.Count > 0)
                {
                    return new BusinessResult { Status = 200, Message = "Farmers ne!", Data = res };
                }

                return new BusinessResult { Status = 404, Message = "Not found any Farmers in plan", Data = null };
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }

        public async Task<IBusinessResult> GetAllItems(int planId)
        {
            try
            {
                var plan = await _unitOfWork.PlantRepository.GetByIdAsync(planId);
                if (plan == null)
                {
                    return new BusinessResult { Status = 404, Message = "Plan not existed !", Data = null };
                }

                var ci = await _unitOfWork.CaringItemRepository.GetCaringItemByPlanId(planId);
                var hi = await _unitOfWork.HarvestingItemRepository.GetHarvestingItemByPlanId(planId);
                var pi = await _unitOfWork.PackagingItemRepository.GetPackagingItemByPlanId(planId);

                var caringItemPlans = ci.GroupBy(i => new { i.Id, i.Unit })
                                        .Select(group => new CaringItemPlan
                                        {
                                            Id = group.Key.Id,
                                            Unit = group.Key.Unit,
                                            EstimatedQuantity = group.Where(i => i.CaringTask.Status.ToLower() != "cancel").Sum(i => i.Quantity),
                                            InUseQuantity = group.Where(i => i.Item.Status.ToLower() == "in-use").Sum(i => i.Quantity)
                                        }).ToList();

                var harvestingItemPlans = hi.GroupBy(i => new { i.Id, i.Unit })
                                            .Select(group => new HarvestingItemPlan
                                            {
                                                Id = group.Key.Id,
                                                Unit = group.Key.Unit,
                                                EstimatedQuantity = group.Where(i => i.HarvestingTask.Status.ToLower() != "cancel").Sum(i => i.Quantity),
                                                InUseQuantity = group.Where(i => i.Item.Status.ToLower() == "in-use").Sum(i => i.Quantity)
                                            }).ToList();

                var packagingItemPlans = pi.GroupBy(i => new { i.Id, i.Unit })
                                           .Select(group => new PackagingItemPlan
                                           {
                                               Id = group.Key.Id,
                                               Unit = group.Key.Unit,
                                               EstimatedQuantity = group.Where(i => i.PackagingTask.Status.ToLower() != "cancel").Sum(i => i.Quantity),
                                               InUseQuantity = group.Where(i => i.Item.Status.ToLower() == "in-use").Sum(i => i.Quantity)
                                           }).ToList();

                var rs = new ItemPlan
                {
                    CaringItemPlans = caringItemPlans,
                    HarvestingItemPlans = harvestingItemPlans,
                    PackagingItemPlans = packagingItemPlans,
                };

                return new BusinessResult { Status = 200, Message = "Item in Plan", Data = rs };
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }

        public async Task<IBusinessResult> AssignTasks(int id, AssigningPlan model)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);
                if (plan == null)
                {
                    return new BusinessResult { Status = 404, Message = "Not found any Plans", Data = null };
                }

                _mapper.Map(model, plan);
                await _unitOfWork.PlanRepository.UpdateAsync(plan);

                var farmersCaringTask = await _unitOfWork.FarmerCaringTaskRepository.GetFarmerCaringTasksByPlanId(id);
                foreach (var farmer in farmersCaringTask)
                {
                    await _unitOfWork.FarmerCaringTaskRepository.RemoveAsync(farmer);
                }

                var farmersHarvestingTask = await _unitOfWork.FarmerHarvestingTaskRepository.GetFarmerHarvestingTasksByPlanId(id);
                foreach (var farmer in farmersHarvestingTask)
                {
                    await _unitOfWork.FarmerHarvestingTaskRepository.RemoveAsync(farmer);
                }

                var farmerPackagingTask = await _unitOfWork.FarmerPackagingTaskRepository.GetFarmerPackagingTasksByPlanId(id);
                foreach (var farmer in farmerPackagingTask)
                {
                    await _unitOfWork.FarmerPackagingTaskRepository.RemoveAsync(farmer);
                }

                if (model.AssignCaringTasks != null)
                {
                    foreach (var task in model.AssignCaringTasks)
                    {
                        var caring = await _unitOfWork.CaringTaskRepository.GetByIdAsync(task.Id);
                        caring.Status = task.Status;

                        await _unitOfWork.CaringTaskRepository.UpdateAsync(caring);

                        await _unitOfWork.FarmerCaringTaskRepository.CreateAsync(new FarmerCaringTask
                        {
                            FarmerId = task.FarmerId,
                            TaskId = task.Id,
                            Description = task.Description,
                            Status = "Active",
                            ExpiredDate = task.ExpiredDate,
                        });

                        var farmerPlan = await _unitOfWork.FarmerPermissionRepository.GetFarmerPermission(caring.PlanId, task.FarmerId);
                        if (farmerPlan != null)
                        {
                            await _unitOfWork.FarmerPermissionRepository.CreateAsync(new FarmerPermission
                            {
                                FarmerId = task.FarmerId,
                                PlanId = caring.PlanId,
                                CreatedAt = DateTime.Now,
                                Status = "Active"
                            });
                        }
                    }
                }

                if (model.AssignHarvestingTasks != null)
                {
                    foreach (var task in model.AssignHarvestingTasks)
                    {
                        var harvesting = await _unitOfWork.HarvestingTaskRepository.GetByIdAsync(task.Id);
                        harvesting.Status = task.Status;

                        await _unitOfWork.HarvestingTaskRepository.UpdateAsync(harvesting);

                        await _unitOfWork.FarmerHarvestingTaskRepository.CreateAsync(new FarmerHarvestingTask
                        {
                            FarmerId = task.FarmerId,
                            TaskId = task.Id,
                            Description = task.Description,
                            Status = "Active",
                            ExpiredDate = task.ExpiredDate,
                        });

                        var farmerPlan = await _unitOfWork.FarmerPermissionRepository.GetFarmerPermission(harvesting.PlanId, task.FarmerId);
                        if (farmerPlan != null)
                        {
                            await _unitOfWork.FarmerPermissionRepository.CreateAsync(new FarmerPermission
                            {
                                FarmerId = task.FarmerId,
                                PlanId = harvesting.PlanId,
                                CreatedAt = DateTime.Now,
                                Status = "Active"
                            });
                        }
                    }
                }
                
                if (model.AssignInspectingTasks != null)
                {
                    foreach (var task in model.AssignInspectingTasks)
                    {
                        var inspecting = await _unitOfWork.InspectingFormRepository.GetByIdAsync(task.Id);
                        inspecting.InspectorId = task.InspectorId;
                        inspecting.Status = task.Status;

                        await _unitOfWork.InspectingFormRepository.UpdateAsync(inspecting);
                    }
                }

                if (model.AssignPackagingTasks != null)
                {
                    foreach (var task in model.AssignPackagingTasks)
                    {
                        var packaging = await _unitOfWork.PackagingTaskRepository.GetByIdAsync(task.Id);
                        packaging.Status = task.Status;

                        await _unitOfWork.PackagingTaskRepository.UpdateAsync(packaging);

                        await _unitOfWork.FarmerPackagingTaskRepository.CreateAsync(new FarmerPackagingTask
                        {
                            FarmerId = task.FarmerId,
                            TaskId = task.Id,
                            Description = task.Description,
                            Status = "Active",
                            ExpiredDate = task.ExpiredDate,
                        });

                        var farmerPlan = await _unitOfWork.FarmerPermissionRepository.GetFarmerPermission(packaging.PlanId, task.FarmerId);
                        if (farmerPlan != null)
                        {
                            await _unitOfWork.FarmerPermissionRepository.CreateAsync(new FarmerPermission
                            {
                                FarmerId = task.FarmerId,
                                PlanId = packaging.PlanId,
                                CreatedAt = DateTime.Now,
                                Status = "Active"
                            });
                        }
                    }
                }

                return new BusinessResult { Status = 200, Message = "Assign successfull!" };
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }

        public async Task<IBusinessResult> ApprovePlan(int id)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);

                if (plan == null)
                {
                    return new BusinessResult { Status = 404, Message = "Not found any plan!", Data = null };
                }

                if (!plan.YieldId.HasValue)
                {
                    return new BusinessResult(404, "Plan does not have any yield. Can not approve!");
                }

                if (string.IsNullOrEmpty(plan.PlanName))
                {
                    return new BusinessResult(404, "Plan does not have a name. Can not approve!");
                }

                if (string.IsNullOrEmpty(plan.Description))
                {
                    return new BusinessResult(404, "Plan does not have a description. Can not approve!");
                }

                if (!plan.StartDate.HasValue)
                {
                    return new BusinessResult(404, "Plan does not have a start date. Can not approve!");
                }

                if (!plan.EndDate.HasValue)
                {
                    return new BusinessResult(404, "Plan does not have an end date. Can not approve!");
                }
                
                if (!plan.CompleteDate.HasValue)
                {
                    return new BusinessResult(404, "Plan does not have an end date. Can not approve!");
                }

                if (!plan.EstimatedProduct.HasValue)
                {
                    return new BusinessResult(404, "Plan does not have an estimated product. Can not approve!");
                }

                if (string.IsNullOrEmpty(plan.EstimatedUnit))
                {
                    return new BusinessResult(404, "Plan does not have an estimated unit. Can not approve!");
                }

                if (!plan.SeedQuantity.HasValue)
                {
                    return new BusinessResult(404, "Plan does not have a seed quantity. Can not approve!");
                }

                plan.Status = "Pending";
                plan.IsApproved = true;
                _unitOfWork.PlanRepository.PrepareUpdate(plan);

                var caringTasks = await _unitOfWork.CaringTaskRepository.GetAllCaringTasks(id);
                if (caringTasks.Count > 0)
                {
                    foreach (var task in caringTasks)
                    {
                        task.Status = "Pending";
                        await _unitOfWork.CaringTaskRepository.UpdateAsync(task);
                    }
                }

                var inspectingForms = await _unitOfWork.InspectingFormRepository.GetInspectingForms(id);
                if (inspectingForms.Count > 0)
                {
                    foreach (var form in inspectingForms)
                    {
                        form.Status = "Pending";
                        await _unitOfWork.InspectingFormRepository.UpdateAsync(form);
                    }
                }

                var packagingTasks = await _unitOfWork.PackagingTaskRepository.GetPackagingTasks(id);
                if (packagingTasks.Count > 0)
                {
                    foreach(var task in packagingTasks)
                    {
                        task.Status = "Pending";
                        await _unitOfWork.PackagingTaskRepository.UpdateAsync(task);
                    }
                }

                var harvestingTasks = await _unitOfWork.HarvestingTaskRepository.GetHarvestingTasks(id);
                if (harvestingTasks.Count > 0)
                {
                    foreach(var task in harvestingTasks)
                    {
                        task.Status = "Pending";
                        await _unitOfWork.HarvestingTaskRepository.UpdateAsync(task);
                    }
                }

                await _unitOfWork.PlanRepository.SaveAsync();
                return new BusinessResult { Status = 200, Message = "Approve success", Data = null };
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }

        public async Task<IBusinessResult> Create(CreatePlan model)
        {
            try
            {
                var plant = await _unitOfWork.PlantRepository.GetByIdAsync(model.PlantId);
                if (plant == null)
                {
                    return new BusinessResult(404, "Not found any plant!");
                }

                if (model.YieldId != null)
                {
                    var yield = await _unitOfWork.YieldRepository.GetByIdAsync(model.YieldId.Value);
                    if (yield == null)
                    {
                        return new BusinessResult(404, "Not found any yield!");
                    }
                }

                if (model.OrderIds != null)
                {
                    foreach (var id in model.OrderIds)
                    {
                        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
                        if (order == null)
                        {
                            return new BusinessResult(404, $"Not found order {id} !");
                        }

                        if (order.PlanId.HasValue)
                        {
                            return new BusinessResult(400, "This order already has plan !");
                        }
                    }
                }

                var expert = await _unitOfWork.ExpertRepository.GetExpert(model.ExpertId);
                if (expert == null)
                {
                    return new BusinessResult(404, "Not found any expert!");
                }

                var plan = _mapper.Map<Plan>(model);
                plan.Status = "Draft";
                plan.CreatedAt = DateTime.Now;
                plan.CreatedBy = expert.Account.Name;
                plan.IsApproved = false;
                var rs = await _unitOfWork.PlanRepository.CreateAsync(plan);

                if (model.OrderIds != null)
                {
                    foreach (var id in model.OrderIds)
                    {
                        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);

                        order.PlanId = plan.Id;
                        _unitOfWork.OrderRepository.PrepareUpdate(order);
                    }

                    await _unitOfWork.OrderRepository.SaveAsync();
                }

                if (rs != null)
                {
                    var result = _mapper.Map<PlanModel>(rs);
                    return new BusinessResult(200, "Create plan successfully!", result);
                }
                else
                {
                    return new BusinessResult(500, "Create plan failed !");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }

        public async Task<IBusinessResult> UpdateStatus(int id, string status, string? reportBy)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);
                if (plan == null)
                {
                    return new BusinessResult(404, "Not found any plans !");
                }

                plan.Status = status;
                plan.UpdatedAt = DateTime.Now;
                plan.UpdatedBy = reportBy;
                var rs = await _unitOfWork.PlanRepository.UpdateAsync(plan);

                if(rs != null)
                {
                    return new BusinessResult(200, "Update status successfully");
                }
                else
                {
                    return new BusinessResult(500, "Update failed!");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }

        public async Task<IBusinessResult> DeletePlan(int id)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);
                if (plan == null)
                {
                    return new BusinessResult(404, "Not found any plan !");
                }

                if (!string.Equals(plan.Status, "draft", StringComparison.OrdinalIgnoreCase))
                {
                    return new BusinessResult(400, "You just can delete plan have draft status !");
                }

                var inspecPlan = await _unitOfWork.InspectingFormRepository.GetInspectingForms(planId: id);
                foreach (var form in inspecPlan)
                {
                    await _unitOfWork.InspectingFormRepository.RemoveAsync(form);
                }

                var harItems = await _unitOfWork.HarvestingItemRepository.GetHarvestingItemByPlanId(id);
                foreach (var harItem in harItems)
                {
                    await _unitOfWork.HarvestingItemRepository.RemoveAsync(harItem);
                }

                var harvestPlan = await _unitOfWork.HarvestingTaskRepository.GetHarvestingTasks(planId: id);
                foreach (var task in harvestPlan)
                {
                    await _unitOfWork.HarvestingTaskRepository.RemoveAsync(task);
                }

                var careFer = await _unitOfWork.CaringFertilizerRepository.GetCareFertilizersByPlanId(id);
                foreach (var fer in careFer)
                {
                    await _unitOfWork.CaringFertilizerRepository.RemoveAsync(fer);
                }

                var carePes = await _unitOfWork.CaringPesticideRepository.GetCarePesticidesByPlanId(id);
                foreach (var pes in carePes)
                {
                    await _unitOfWork.CaringPesticideRepository.RemoveAsync(pes);
                }

                var careItems = await _unitOfWork.CaringItemRepository.GetCaringItemByPlanId(id);
                foreach (var item in careItems)
                {
                    await _unitOfWork.CaringItemRepository.RemoveAsync(item);
                }

                var packItems = await _unitOfWork.PackagingItemRepository.GetPackagingItemByPlanId(planId: id);
                foreach (var item in packItems)
                {
                    await _unitOfWork.PackagingItemRepository.RemoveAsync(item);
                }

                var packagePlan = await _unitOfWork.PackagingTaskRepository.GetPackagingTasks(planId: id);
                foreach (var package in packagePlan)
                {
                    await _unitOfWork.PackagingTaskRepository.RemoveAsync(package);
                }

                var carePlan = await _unitOfWork.CaringTaskRepository.GetAllCaringTasks(planId: id);
                foreach (var task in carePlan)
                {
                    await _unitOfWork.CaringTaskRepository.RemoveAsync(task);
                }

                var rs = await _unitOfWork.PlanRepository.RemoveAsync(plan);
                if (rs)
                {
                    return new BusinessResult(200, "Delete plan successfull");
                }
                else
                {
                    return new BusinessResult(500, "Delete failed !");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }

        public async Task<IBusinessResult> UpdatePlan(int id, UpdatePlan model)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);
                if (plan == null)
                {
                    return new BusinessResult(404, "Not found any plans !");
                }

                plan.YieldId = model.YieldId;
                plan.PlantId = model.PlantId;
                plan.PlanName = model.PlanName;
                plan.Description = model.Description;
                plan.EstimatedProduct = model.EstimatedProduct;
                plan.EstimatedUnit = model.EstimatedUnit;
                plan.ExpertId = model.ExpertId;
                plan.StartDate = model.StartDate;
                plan.EndDate = model.EndDate;
                plan.UpdatedAt = DateTime.Now;
                plan.UpdatedBy = model.UpdatedBy;
                plan.SeedQuantity = model.SeedQuantity;

                var rs = await _unitOfWork.PlanRepository.UpdateAsync(plan);

                if (rs != null)
                {
                    return new BusinessResult(200, "Update status successfully");
                }
                else
                {
                    return new BusinessResult(500, "Update failed!");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }
        public async Task<IBusinessResult> GetStatusTasksDashboardByPlanId(int id)
        {
            var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);
            if (plan == null) return new BusinessResult(200, "Dashboard Caring Tasks by plan id", null);
            var result = new TasksDashboardModel();
            result.Id = plan.Id;
            result.CaringTask = await _unitOfWork.CaringTaskRepository.GetTasksStatusDashboardByPlanId(id);
            result.HarvestingTask = await _unitOfWork.HarvestingTaskRepository.GetHarvestingTasksStatusDashboardByPlanId(id);
            result.PackagingTask = await _unitOfWork.PackagingTaskRepository.GetPackagingTasksStatusDashboardByPlanId(id);
            return new BusinessResult(200, "Get Tasks Dashboard", result);
        }

        public async Task<IBusinessResult> GetAllPlanFarmerAssigned(int id)
        {
            try
            {
                var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(id);
                if (farmer == null)
                {
                    return new BusinessResult(404, "Not found any farmers !");
                }

                var plans = await _unitOfWork.PlanRepository.GetPlanFarmerAssign(id);
                var rs = _mapper.Map<List<PlanListFarmerAssignTo>>(plans);

                if (rs.Count <= 0)
                {
                    return new BusinessResult(404, "Not found any plans !");
                }
                else
                {
                    return new BusinessResult(200, "Plans that farmer assigned in : ", rs);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetInfomationOfFertilizerTasksByPlanId(int id)
        {
            var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);
            if (plan == null) return new BusinessResult(404, "Plan not found !", null);

            var result = new List<NurturingItem>();
            var list = await _unitOfWork.CaringFertilizerRepository.GetFertilizersByPlanId(id);
            foreach (var item in list)
            {
                var obj = new NurturingItem();
                obj.Id = item.Id;
                obj.Name = item.Name;
                obj.EstimateQuantity = await _unitOfWork.CaringFertilizerRepository.EstimateFertilizerInPlan(id, item.Id);
                obj.UsedQuantity = await _unitOfWork.CaringFertilizerRepository.UsedFertilizerInPlan(id, item.Id);
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
            var list = await _unitOfWork.CaringPesticideRepository.GetPesticidesByPlanId(id);
            foreach (var item in list)
            {
                var obj = new NurturingItem();
                obj.Id = item.Id;
                obj.Name = item.Name;
                obj.EstimateQuantity = await _unitOfWork.CaringPesticideRepository.EstimatePesticideInPlan(id, item.Id);
                obj.UsedQuantity = await _unitOfWork.CaringPesticideRepository.UsedPesticideInPlan(id, item.Id);
                obj.Unit = "l";
                result.Add(obj);
            }
            return new BusinessResult(200, "Get Infomation Of Pesticide Tasks By PlanId", result);
        }

        public async Task<IBusinessResult> RemoveFarmerFromPlan(int planId, int farmerId)
        {
            try
            {
                var plan = await _unitOfWork.PlantRepository.GetByIdAsync(planId);
                if (plan == null)
                {
                    return new BusinessResult(404, "Not found any plan !");
                }

                var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(farmerId);
                if (farmer == null)
                {
                    return new BusinessResult(404, "Not found any farmer !");
                }

                var isInCaringPlan = await _unitOfWork.FarmerCaringTaskRepository.CheckFarmerAssignInPlan(planId, farmerId);
                if (isInCaringPlan)
                {
                    return new BusinessResult(400, "Can not remove farmer because he/she have been assign in a Caring Task !");
                }

                var isInHarvestingPlan = await _unitOfWork.FarmerHarvestingTaskRepository.CheckFarmerAssignInPlan(planId, farmerId);
                if (isInHarvestingPlan)
                {
                    return new BusinessResult(400, "Can not remove farmer because he/she have been assign in a Harvesting Task !");
                }

                var isInPackagingPlan = await _unitOfWork.FarmerPackagingTaskRepository.CheckFarmerAssignInPlan(planId, farmerId);
                if (isInPackagingPlan)
                {
                    return new BusinessResult(400, "Can not remove farmer because he/she have been assign in a Packaging Task !");
                }

                var farmerPermission = await _unitOfWork.FarmerPermissionRepository.GetFarmerPermission(planId, farmerId);
                var rs = await _unitOfWork.FarmerPermissionRepository.RemoveAsync(farmerPermission);

                if (rs)
                {
                    return new BusinessResult(200, "Remove farmer from plan successfully");
                }

                return new BusinessResult(500, "Remove failed !");
            }
            catch(Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> AddFarmerToPlan(int planId, int farmerId)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(planId);
                if (plan == null)
                {
                    return new BusinessResult(404, "Not found any plan !");
                }

                var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(farmerId);
                if (farmer == null)
                {
                    return new BusinessResult(404, "Not found any farmers");
                }

                await _unitOfWork.FarmerPermissionRepository.CreateAsync(new FarmerPermission
                {
                    CreatedAt = DateTime.Now,
                    FarmerId = farmerId,
                    PlanId = planId,
                    Status = "Active"
                });


                return new BusinessResult(200, "Add Farmer to Plan successfully !");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetCountTasksByPlanId(int id)
        {
            var result = new StatusDashboard();
            result.PackagingTasks = await _unitOfWork.PackagingTaskRepository.GetStatusTaskPackagingByPlanId(id);
            result.CaringTasks = await _unitOfWork.CaringTaskRepository.GetStatusTaskCaringByPlanId(id);
            result.HarvestingTasks = await _unitOfWork.HarvestingTaskRepository.GetStatusTaskHarvestingByPlanId(id);
            return new BusinessResult(200, "Count Status Tasks by Plan Id", result);

        }
    }
}
