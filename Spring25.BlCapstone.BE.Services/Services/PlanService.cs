using AutoMapper;
using IO.Ably;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.BlockChain;
using Spring25.BlCapstone.BE.Repositories.Dashboards;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Template;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Dashboard;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Item;
using Spring25.BlCapstone.BE.Services.BusinessModels.Notification;
using Spring25.BlCapstone.BE.Services.BusinessModels.Order;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plan;
using Spring25.BlCapstone.BE.Services.BusinessModels.Problem;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Care;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Harvest;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Package;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IPlanService
    {
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> GetAll(int? expertId, string? status, int? orderId);
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
        Task<IBusinessResult> GetAllPlanFarmerAssigned(int id, bool? is_active_in_plan, List<string>? status);
        Task<IBusinessResult> GetInfomationOfFertilizerTasksByPlanId(int id);
        Task<IBusinessResult> GetInfomationOfPesticideTasksByPlanId(int id);
        Task<IBusinessResult> RemoveFarmerFromPlan(int planId, int farmerId);
        Task<IBusinessResult> AddFarmerToPlan(int planId, int farmerId);
        Task<IBusinessResult> GetCountTasksByPlanId(int id);
        Task<IBusinessResult> RemoveOrderFromPlan(int id, int orderId);
        Task<IBusinessResult> AddOrderToPlan(int id, int orderId);
        Task<IBusinessResult> GetBusyFarmerInPlanAssigned(int id, DateTime? start, DateTime? end);
        Task<IBusinessResult> GenarateTasksForFarmer(int id, List<int> farmerid);
        Task<IBusinessResult> ChangeCompleteStatus(int id);
        Task<IBusinessResult> ChangeCancelStatus(int id);
        Task<IBusinessResult> PublicPlan(int id);
        Task<IBusinessResult> GetSuggestTasksByPlanId(int planId, int suggestPlanId);
        Task<IBusinessResult> GetSuggestPlansByPlanId(int planId);
        Task<IBusinessResult> CreateBigPlan(CreatePlanTemplate model);
        Task<IBusinessResult> GetPlanOrderById(int id);
        Task<IBusinessResult> GetTemplatePlan(RequestTemplatePlan model);
        Task<IBusinessResult> NotificationforExperts(NotificationExpertsRequest model);
    }

    public class PlanService : IPlanService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly VechainInteraction _veChainInteraction;
        private readonly IVechainInteraction _vechainInteraction;
        public PlanService(IMapper mapper, IVechainInteraction vechainInteraction)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
            _veChainInteraction ??= new VechainInteraction();
            _vechainInteraction = vechainInteraction;
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

        public async Task<IBusinessResult> GetAll(int? expertId, string? status, int? orderId)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetAllPlans(expertId, status, orderId);

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

                if (model.Farmers.Any())
                {
                    var farmerInPlan = await _unitOfWork.FarmerPermissionRepository.GetFarmerPermissionsByPlanId(id);
                    var farmerids = farmerInPlan.Select(f => f.FarmerId).ToList();
                    var newFarmerIds = model.Farmers.Except(farmerids);

                    if (newFarmerIds.Any())
                    {
                        foreach (var fid in newFarmerIds)
                        {
                            var newPermission = new FarmerPermission
                            {
                                FarmerId = fid,
                                PlanId = id,
                                CreatedAt = DateTime.Now,
                                Status = "Active"
                            };

                            _unitOfWork.FarmerPermissionRepository.PrepareCreate(newPermission);
                        }

                        await _unitOfWork.FarmerPermissionRepository.SaveAsync();
                    }
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
                        if (farmerPlan == null)
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
                        if (farmerPlan == null)
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
                        if (farmerPlan == null)
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
                    return new BusinessResult(404, null, "Plan does not have any yield. Can not approve!");
                }

                if (string.IsNullOrEmpty(plan.PlanName))
                {
                    return new BusinessResult(404, null, "Plan does not have a name. Can not approve!");
                }

                if (string.IsNullOrEmpty(plan.Description))
                {
                    return new BusinessResult(404, null, "Plan does not have a description. Can not approve!");
                }

                if (!plan.StartDate.HasValue)
                {
                    return new BusinessResult(404, null, "Plan does not have a start date. Can not approve!");
                }

                if (!plan.EndDate.HasValue)
                {
                    return new BusinessResult(404, null, "Plan does not have an end date. Can not approve!");
                }

                if (!plan.EstimatedProduct.HasValue)
                {
                    return new BusinessResult(404, null, "Plan does not have an estimated product. Can not approve!");
                }

                if (!plan.SeedQuantity.HasValue)
                {
                    return new BusinessResult(404, null, "Plan does not have a seed quantity. Can not approve!");
                }

                if (!plan.Status.ToLower().Trim().Equals("pending"))
                {
                    return new BusinessResult(400, null, "Can not approve plan that not have status pending!");
                }

                var plant = await _unitOfWork.PlantRepository.GetByIdAsync(plan.PlantId);
                if (plant == null)
                {
                    return new BusinessResult(400, null, "Can not approve plan that not have seed");
                }

                if (plant.Quantity < plan.SeedQuantity)
                {
                    return new BusinessResult(400, null, "Seed quantity available in system is not enough for this plan. Please add more seed in system !");
                }

                plant.Quantity -= plan.SeedQuantity.Value;
                await _unitOfWork.PlantRepository.UpdateAsync(plant);

                plan.Status = "Ongoing";
                plan.IsApproved = true;
                _unitOfWork.PlanRepository.PrepareUpdate(plan);

                var caringTasks = await _unitOfWork.CaringTaskRepository.GetAllCaringTasks(id);
                if (caringTasks.Count > 0)
                {
                    foreach (var task in caringTasks)
                    {
                        task.Status = "Ongoing";
                        _unitOfWork.CaringTaskRepository.PrepareUpdate(task);
                    }
                }

                var inspectingForms = await _unitOfWork.InspectingFormRepository.GetInspectingForms(id);
                if (inspectingForms.Count > 0)
                {
                    foreach (var form in inspectingForms)
                    {
                        form.Status = "Ongoing";
                        _unitOfWork.InspectingFormRepository.PrepareUpdate(form);
                    }
                }

                var packagingTasks = await _unitOfWork.PackagingTaskRepository.GetPackagingTasks(id);
                if (packagingTasks.Count > 0)
                {
                    foreach (var task in packagingTasks)
                    {
                        task.Status = "Ongoing";
                        _unitOfWork.PackagingTaskRepository.PrepareUpdate(task);
                    }
                }

                var harvestingTasks = await _unitOfWork.HarvestingTaskRepository.GetHarvestingTasks(id);
                if (harvestingTasks.Count > 0)
                {
                    foreach (var task in harvestingTasks)
                    {
                        task.Status = "Ongoing";
                        _unitOfWork.HarvestingTaskRepository.PrepareUpdate(task);
                    }
                }

                if (plan.YieldId.HasValue)
                {
                    var yield = await _unitOfWork.YieldRepository.GetByIdAsync(plan.YieldId.Value);
                    yield.Status = "In-Use";
                    _unitOfWork.YieldRepository.PrepareUpdate(yield);
                }

                var result = await _vechainInteraction.CreateNewVechainPlan(new CreatedVeChainPlan
                {
                    PlanId = id,
                    PlantId = plan.PlantId,
                    YieldId = plan.YieldId.Value,
                    ExpertId = plan.ExpertId,
                    PlanName = plan.PlanName,
                    StartDate = (new DateTimeOffset(plan.StartDate.Value).ToUnixTimeSeconds()).ToString(),
                    EndDate = (new DateTimeOffset(plan.EndDate.Value).ToUnixTimeSeconds()).ToString(),
                    EstimatedProduct = plan.EstimatedProduct.Value,
                    Status = plan.Status,
                });

                await _unitOfWork.PlanTransactionRepository.CreateAsync(new PlanTransaction
                {
                    Id = id,
                    UrlAddress = result
                });

                await _unitOfWork.PlanRepository.SaveAsync();
                await _unitOfWork.CaringTaskRepository.SaveAsync();
                await _unitOfWork.InspectingFormRepository.SaveAsync();
                await _unitOfWork.PackagingTaskRepository.SaveAsync();
                await _unitOfWork.HarvestingTaskRepository.SaveAsync();
                await _unitOfWork.YieldRepository.SaveAsync();

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
                    else
                    {
                        if (!yield.Status.ToLower().Trim().Equals("available"))
                        {
                            return new BusinessResult(400, $"Can not use yield with status {yield.Status}");
                        }
                    }
                }

                if (model.Orders != null)
                {
                    foreach (var order in model.Orders)
                    {
                        var existed = await _unitOfWork.OrderRepository.GetOrderByOrderId(order.OrderId);
                        if (order == null)
                        {
                            return new BusinessResult(404, $"Not found order {order.OrderId} !");
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

                if (model.Orders != null)
                {
                    foreach (var o in model.Orders)
                    {
                        await _unitOfWork.OrderPlanRepository.CreateAsync(new OrderPlan
                        {
                            OrderId = o.OrderId,
                            PlanId = plan.Id,
                            Quantity = o.Quantity
                        });
                    }
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

                var orders = await _unitOfWork.OrderPlanRepository.GetOrderPlansByPlanId(id);
                foreach (var order in orders)
                {
                    _unitOfWork.OrderPlanRepository.PrepareRemove(order);
                }

                await _unitOfWork.OrderPlanRepository.SaveAsync();

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

        public async Task<IBusinessResult> GetAllPlanFarmerAssigned(int id, bool? is_active_in_plan, List<string>? status)
        {
            try
            {
                var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(id);
                if (farmer == null)
                {
                    return new BusinessResult(404, "Not found any farmers !");
                }

                var plans = await _unitOfWork.FarmerPermissionRepository.GetPlanFarmerAssign(id, is_active_in_plan, status);
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
                    return new BusinessResult(400, null, "Can not remove farmer because he/she have been assign in a Caring Task !");
                }

                var isInHarvestingPlan = await _unitOfWork.FarmerHarvestingTaskRepository.CheckFarmerAssignInPlan(planId, farmerId);
                if (isInHarvestingPlan)
                {
                    return new BusinessResult(400, null, "Can not remove farmer because he/she have been assign in a Harvesting Task !");
                }

                var isInPackagingPlan = await _unitOfWork.FarmerPackagingTaskRepository.CheckFarmerAssignInPlan(planId, farmerId);
                if (isInPackagingPlan)
                {
                    return new BusinessResult(400, null, "Can not remove farmer because he/she have been assign in a Packaging Task !");
                }

                var farmerPermission = await _unitOfWork.FarmerPermissionRepository.GetFarmerPermission(planId, farmerId);
                var rs = await _unitOfWork.FarmerPermissionRepository.RemoveAsync(farmerPermission);

                if (rs)
                {
                    return new BusinessResult(200, "Remove farmer from plan successfully");
                }

                return new BusinessResult(500, "Remove failed !");
            }
            catch (Exception ex)
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

        public async Task<IBusinessResult> AddOrderToPlan(int id, int orderId)
        {
            try
            {
                var plan = await _unitOfWork.PlantRepository.GetByIdAsync(id);
                if (plan == null)
                {
                    return new BusinessResult(404, "Not found any plan !");
                }

                var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
                if (order == null)
                {
                    return new BusinessResult(404, "Not found any order !");
                }

                //add orderplan co quantity nho check
                await _unitOfWork.OrderRepository.UpdateAsync(order);
                var rs = _mapper.Map<OrderModel>(order);
                return new BusinessResult(200, "Add plan to order successfull", rs);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> RemoveOrderFromPlan(int id, int orderId)
        {
            try
            {
                var plan = await _unitOfWork.PlantRepository.GetByIdAsync(id);
                if (plan == null)
                {
                    return new BusinessResult(404, "Not found any plan !");
                }

                var order = await _unitOfWork.OrderRepository.GetOrderByOrderId(orderId);
                if (order == null)
                {
                    return new BusinessResult(404, "Not found any order !");
                }

                if (!order.OrderPlans.Any())
                {
                    return new BusinessResult(400, "This order doesn't have any plans ?");
                }

                if (!order.OrderPlans.Any(c => c.PlanId == id))
                {
                    return new BusinessResult(400, "This order is not belong to this plan !");
                }

                var orderPlan = await _unitOfWork.OrderPlanRepository.GetOrderPlansByPlanId(id, orderId);
                if (orderPlan.Any() || orderPlan.Count > 0)
                {
                    orderPlan.ForEach(o => _unitOfWork.OrderPlanRepository.PrepareRemove(o));
                }
                await _unitOfWork.OrderPlanRepository.SaveAsync();

                await _unitOfWork.OrderRepository.UpdateAsync(order);
                var rs = _mapper.Map<OrderModel>(order);
                return new BusinessResult(200, "Remove order from plan successfull", rs);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetBusyFarmerInPlanAssigned(int id, DateTime? start, DateTime? end)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);
                if (plan == null)
                {
                    return new BusinessResult(404, "Not found any plan !");
                }

                var farmers = await _unitOfWork.FarmerRepository.GetBusyFarmersByPlanId(id, start, end);

                var result = farmers.Select(farmer => new FarmerBusySchedule
                {
                    Id = farmer.Id,
                    Name = farmer.Account.Name,
                    FarmerSchedule = (
                        farmer.FarmerCaringTasks
                              .Where(t => t.CaringTask.StartDate < end && t.CaringTask.EndDate > start)
                              .Select(t => new Schedule
                              {
                                  StartDate = t.CaringTask.StartDate,
                                  EndDate = t.CaringTask.EndDate
                              })
                        .Concat(
                        farmer.FarmerHarvestingTasks
                            .Where(t => t.HarvestingTask.StartDate < end && t.HarvestingTask.EndDate > start)
                            .Select(t => new Schedule
                            {
                                StartDate = t.HarvestingTask.StartDate,
                                EndDate = t.HarvestingTask.EndDate
                            })
                        )
                        .Concat(
                            farmer.FarmerPackagingTasks
                                .Where(t => t.PackagingTask.StartDate < end && t.PackagingTask.EndDate > start)
                                .Select(t => new Schedule
                                {
                                    StartDate = t.PackagingTask.StartDate,
                                    EndDate = t.PackagingTask.EndDate
                                })
                        )
                        ).ToList()
                }).ToList();

                if (result.Count <= 0)
                {
                    return new BusinessResult(404, "Not found any busy farmers !");
                }
                else
                {
                    return new BusinessResult(200, "Plans that farmer assigned in : ", result);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GenarateTasksForFarmer(int id, List<int> farmerIds)
        {
            var result = new GenerateTasksModel();
            var plan = await _unitOfWork.PlanRepository.GetPlan(id);
            var listFarmer = await _unitOfWork.FarmerRepository.GetFarmersByListId(farmerIds, id);

            if (plan == null) return new BusinessResult(400, "Không thể tìm thấy kế hoạch này !");

            if (listFarmer == null) return new BusinessResult(400, "Số nông dân truyền vào không khớp với số nông dân có trong kế hoạch !");

            result.PlanId = id;
            var caringTasks = plan.CaringTasks.Where(x => x.Status.ToLower() == "pending" || x.Status.ToLower() == "draft" || x.Status.ToLower().Trim().Equals("ongoing")).ToList();
            var harvestingTasks = plan.HarvestingTasks.Where(x => x.Status.ToLower() == "pending" || x.Status.ToLower() == "draft" || x.Status.ToLower().Trim().Equals("ongoing")).ToList();
            var packagingTasks = plan.PackagingTasks.Where(x => x.Status.ToLower() == "pending" || x.Status.ToLower() == "draft" || x.Status.ToLower().Trim().Equals("ongoing")).ToList();

            int farmerIndex = 0;
            foreach (var task in harvestingTasks)
            {
                var freeFarmers = await _unitOfWork.FarmerRepository.GetFreeFarmerByListId(farmerIds, task.StartDate, task.EndDate);
                if (freeFarmers == null || !freeFarmers.Any()) return new BusinessResult(400, $"Không có Nông Dân nào thực hiện được Việc Thu Hoạch: {task.Id}", $"Không có Nông Dân nào thực hiện được Việc Thu Hoạch: {task.Id}");
                var farmer = freeFarmers[farmerIndex % freeFarmers.Count];
                result.HarvestingTasks.Add(new HarvestingTaskGenerate
                {
                    HarvestingTaskId = task.Id,
                    FarmerId = farmer.Id,
                    Avatar = farmer.Avatar,
                    StartDate = task.StartDate,
                    EndDate = task.EndDate,
                    ExpiredDate = task.EndDate.AddDays(1)
                });
                farmerIndex++;
            }

            foreach (var task in caringTasks)
            {
                var freeFarmers = await _unitOfWork.FarmerRepository.GetFreeFarmerByListId(farmerIds, task.StartDate, task.EndDate);
                if (freeFarmers == null || !freeFarmers.Any()) return new BusinessResult(400, $"Không có Nông Dân nào thực hiện được Việc Chăm Sóc:{task.Id}", $"Không có Nông Dân nào thực hiện được Việc Chăm Sóc:{task.Id}");
                var farmer = freeFarmers[farmerIndex % freeFarmers.Count];
                result.CaringTasks.Add(new CaringTaskGenerate
                {
                    CaringTaskId = task.Id,
                    FarmerId = farmer.Id,
                    Avatar = farmer.Avatar,
                    StartDate = task.StartDate,
                    EndDate = task.EndDate,
                    ExpiredDate = task.EndDate.AddDays(1)
                });
                farmerIndex++;
            }

            foreach (var task in packagingTasks)
            {
                var freeFarmers = await _unitOfWork.FarmerRepository.GetFreeFarmerByListId(farmerIds, task.StartDate, task.EndDate);
                if (freeFarmers == null || !freeFarmers.Any()) return new BusinessResult(400, $"Không có Nông Dân nào thực hiện được Việc Đóng Gói:{task.Id}", $"Không có Nông Dân nào thực hiện được Việc Đóng Gói:{task.Id}");
                var farmer = freeFarmers[farmerIndex % freeFarmers.Count];
                result.PackagingTasks.Add(new PackagingTaskGenerate
                {
                    PackagingTaskId = task.Id,
                    FarmerId = farmer.Id,
                    Avatar = farmer.Avatar,
                    StartDate = task.StartDate,
                    EndDate = task.EndDate,
                    ExpiredDate = task.EndDate.AddDays(1)
                });
                farmerIndex++;
            }
            return new BusinessResult(200, "Get Generate Tasks", result);
        }

        public async Task<IBusinessResult> ChangeCompleteStatus(int id)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);
                if (plan == null)
                {
                    return new BusinessResult(404, "Not found any plans !");
                }

                if (!plan.Status.ToLower().Trim().Equals("ongoing"))
                {
                    return new BusinessResult(400, $"Can not complete plan with {plan.Status} status");
                }

                plan.Status = "Complete";
                _unitOfWork.PlanRepository.PrepareUpdate(plan);

                var farmer = await _unitOfWork.FarmerPermissionRepository.GetFarmerPermissionsByPlanId(id);
                farmer.ForEach(f =>
                {
                    f.Status = "Inactive";
                    _unitOfWork.FarmerPermissionRepository.PrepareUpdate(f);
                });

                var caringTasks = await _unitOfWork.CaringTaskRepository.GetAllCaringTasks(planId: id);
                foreach (var caringTask in caringTasks)
                {
                    if (caringTask.Status.Trim().ToLower().Equals("ongoing"))
                    {
                        caringTask.Status = "InComplete";
                        _unitOfWork.CaringTaskRepository.PrepareUpdate(caringTask);
                    }
                }

                var harvestingTasks = await _unitOfWork.HarvestingTaskRepository.GetHarvestingTasks(planId: id);
                foreach (var harvestingTask in harvestingTasks)
                {
                    if (harvestingTask.Status.Trim().ToLower().Equals("ongoing"))
                    {
                        harvestingTask.Status = "InComplete";
                        _unitOfWork.HarvestingTaskRepository.PrepareUpdate(harvestingTask);
                    }
                }

                var packagingTasks = await _unitOfWork.PackagingTaskRepository.GetPackagingTasks(planId: id);
                foreach (var packagingTask in packagingTasks)
                {
                    if (packagingTask.Status.Trim().ToLower().Equals("ongoing"))
                    {
                        packagingTask.Status = "InComplete";
                        _unitOfWork.PackagingTaskRepository.PrepareUpdate(packagingTask);
                    }
                }

                var inspectingForms = await _unitOfWork.InspectingFormRepository.GetInspectingForms(planId: id);
                foreach (var inspectingForm in inspectingForms)
                {
                    if (inspectingForm.Status.Trim().ToLower().Equals("ongoing"))
                    {
                        inspectingForm.Status = "InComplete";
                        _unitOfWork.InspectingFormRepository.PrepareUpdate(inspectingForm);
                    }
                }

                if (plan.YieldId.HasValue)
                {
                    var yield = await _unitOfWork.YieldRepository.GetByIdAsync(plan.YieldId.Value);
                    yield.Status = "Available";

                    _unitOfWork.YieldRepository.PrepareUpdate(yield);
                }

                var farmerCaring = await _unitOfWork.FarmerCaringTaskRepository.GetFarmerCaringTasksByPlanId(id);
                farmerCaring.ForEach(f =>
                {
                    f.Status = "Inactive";
                    f.ExpiredDate = DateTime.Now;
                    _unitOfWork.FarmerCaringTaskRepository.PrepareUpdate(f);
                });

                var farmerHarvesting = await _unitOfWork.FarmerHarvestingTaskRepository.GetFarmerHarvestingTasksByPlanId(id);
                farmerHarvesting.ForEach(f =>
                {
                    f.Status = "Inactive";
                    f.ExpiredDate = DateTime.Now;
                    _unitOfWork.FarmerHarvestingTaskRepository.PrepareUpdate(f);
                });

                var farmerPackaging = await _unitOfWork.FarmerPackagingTaskRepository.GetFarmerPackagingTasksByPlanId(id);
                farmerPackaging.ForEach(f =>
                {
                    f.Status = "Inactive";
                    f.ExpiredDate = DateTime.Now;
                    _unitOfWork.FarmerPackagingTaskRepository.PrepareUpdate(f);
                });

                await _unitOfWork.PlanRepository.SaveAsync();
                await _unitOfWork.CaringTaskRepository.SaveAsync();
                await _unitOfWork.HarvestingTaskRepository.SaveAsync();
                await _unitOfWork.PackagingTaskRepository.SaveAsync();
                await _unitOfWork.InspectingFormRepository.SaveAsync();
                await _unitOfWork.YieldRepository.SaveAsync();
                await _unitOfWork.FarmerPermissionRepository.SaveAsync();
                await _unitOfWork.FarmerCaringTaskRepository.SaveAsync();
                await _unitOfWork.FarmerHarvestingTaskRepository.SaveAsync();
                await _unitOfWork.FarmerPackagingTaskRepository.SaveAsync();

                return new BusinessResult(200, "Complete plan successfull !");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> ChangeCancelStatus(int id)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);
                if (plan == null)
                {
                    return new BusinessResult(404, "Not found any plans !");
                }

                if (plan.Status.ToLower().Trim().Equals("ongoing"))
                {
                    return new BusinessResult(400, $"Can not cancel plan with {plan.Status} status");
                }

                var farmer = await _unitOfWork.FarmerPermissionRepository.GetFarmerPermissionsByPlanId(id);
                farmer.ForEach(f =>
                {
                    f.Status = "Inactive";
                    _unitOfWork.FarmerPermissionRepository.PrepareUpdate(f);
                });

                plan.Status = "Cancel";
                _unitOfWork.PlanRepository.PrepareUpdate(plan);

                var caringTasks = await _unitOfWork.CaringTaskRepository.GetAllCaringTasks(planId: id);
                foreach (var caringTask in caringTasks)
                {
                    caringTask.Status = "Cancel";
                    _unitOfWork.CaringTaskRepository.PrepareUpdate(caringTask);
                }

                var harvestingTasks = await _unitOfWork.HarvestingTaskRepository.GetHarvestingTasks(planId: id);
                foreach (var harvestingTask in harvestingTasks)
                {
                    harvestingTask.Status = "Cancel";
                    _unitOfWork.HarvestingTaskRepository.PrepareUpdate(harvestingTask);
                }

                var packagingTasks = await _unitOfWork.PackagingTaskRepository.GetPackagingTasks(planId: id);
                foreach (var packagingTask in packagingTasks)
                {
                    packagingTask.Status = "Cancel";
                    _unitOfWork.PackagingTaskRepository.PrepareUpdate(packagingTask);
                }

                var inspectingForms = await _unitOfWork.InspectingFormRepository.GetInspectingForms(planId: id);
                foreach (var inspectingForm in inspectingForms)
                {
                    inspectingForm.Status = "Cancel";
                    _unitOfWork.InspectingFormRepository.PrepareUpdate(inspectingForm);
                }

                var farmerCaring = await _unitOfWork.FarmerCaringTaskRepository.GetFarmerCaringTasksByPlanId(id);
                farmerCaring.ForEach(f =>
                {
                    f.Status = "Inactive";
                    f.ExpiredDate = DateTime.Now;
                    _unitOfWork.FarmerCaringTaskRepository.PrepareUpdate(f);
                });

                var farmerHarvesting = await _unitOfWork.FarmerHarvestingTaskRepository.GetFarmerHarvestingTasksByPlanId(id);
                farmerHarvesting.ForEach(f =>
                {
                    f.Status = "Inactive";
                    f.ExpiredDate = DateTime.Now;
                    _unitOfWork.FarmerHarvestingTaskRepository.PrepareUpdate(f);
                });

                var farmerPackaging = await _unitOfWork.FarmerPackagingTaskRepository.GetFarmerPackagingTasksByPlanId(id);
                farmerPackaging.ForEach(f =>
                {
                    f.Status = "Inactive";
                    f.ExpiredDate = DateTime.Now;
                    _unitOfWork.FarmerPackagingTaskRepository.PrepareUpdate(f);
                });

                if (plan.YieldId.HasValue)
                {
                    var yield = await _unitOfWork.YieldRepository.GetByIdAsync(plan.YieldId.Value);
                    yield.Status = "Available";

                    _unitOfWork.YieldRepository.PrepareUpdate(yield);
                }

                await _unitOfWork.PlanRepository.SaveAsync();
                await _unitOfWork.CaringTaskRepository.SaveAsync();
                await _unitOfWork.HarvestingTaskRepository.SaveAsync();
                await _unitOfWork.PackagingTaskRepository.SaveAsync();
                await _unitOfWork.InspectingFormRepository.SaveAsync();
                await _unitOfWork.YieldRepository.SaveAsync();
                await _unitOfWork.FarmerPermissionRepository.SaveAsync();
                await _unitOfWork.FarmerCaringTaskRepository.SaveAsync();
                await _unitOfWork.FarmerHarvestingTaskRepository.SaveAsync();
                await _unitOfWork.FarmerPackagingTaskRepository.SaveAsync();

                return new BusinessResult(200, "Cancel plan successfull !");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> PublicPlan(int id)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);
                if (plan == null)
                {
                    return new BusinessResult(404, "Not found any plans !");
                }

                if (!plan.Status.ToLower().Trim().Equals("draft"))
                {
                    return new BusinessResult(400, $"Can not public plan with {plan.Status} status");
                }

                plan.Status = "Pending";
                _unitOfWork.PlanRepository.PrepareUpdate(plan);

                var caringTasks = await _unitOfWork.CaringTaskRepository.GetAllCaringTasks(planId: id);
                foreach (var caringTask in caringTasks)
                {
                    caringTask.Status = "Pending";
                    _unitOfWork.CaringTaskRepository.PrepareUpdate(caringTask);
                }

                var harvestingTasks = await _unitOfWork.HarvestingTaskRepository.GetHarvestingTasks(planId: id);
                foreach (var harvestingTask in harvestingTasks)
                {
                    harvestingTask.Status = "Pending";
                    _unitOfWork.HarvestingTaskRepository.PrepareUpdate(harvestingTask);
                }

                var packagingTasks = await _unitOfWork.PackagingTaskRepository.GetPackagingTasks(planId: id);
                foreach (var packagingTask in packagingTasks)
                {
                    packagingTask.Status = "Pending";
                    _unitOfWork.PackagingTaskRepository.PrepareUpdate(packagingTask);
                }

                var inspectingForms = await _unitOfWork.InspectingFormRepository.GetInspectingForms(planId: id);
                foreach (var inspectingForm in inspectingForms)
                {
                    inspectingForm.Status = "Pending";
                    _unitOfWork.InspectingFormRepository.PrepareUpdate(inspectingForm);
                }

                await _unitOfWork.PlanRepository.SaveAsync();
                await _unitOfWork.CaringTaskRepository.SaveAsync();
                await _unitOfWork.HarvestingTaskRepository.SaveAsync();
                await _unitOfWork.PackagingTaskRepository.SaveAsync();
                await _unitOfWork.InspectingFormRepository.SaveAsync();

                return new BusinessResult(200, "Public plan successfull !");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetSuggestTasksByPlanId(int planId, int suggestPlanId)
        {
            var result = new SuggestTasksModel();
            var plan = await _unitOfWork.PlanRepository.GetByIdAsync(planId);
            if (plan == null) { return new BusinessResult(400, "Not Found this Plan"); }
            var suggestPlan = await _unitOfWork.PlanRepository.GetTasksByPlanId(suggestPlanId);
            if (suggestPlan == null) { return new BusinessResult(400, "Not Found this Suggest Plan"); }
            if (suggestPlan.EstimatedProduct == null) { return new BusinessResult(400, "This Plan do not have EstimatedProduct"); }
            var ratioEstimate = MathF.Round(plan.EstimatedProduct.Value / suggestPlan.EstimatedProduct.Value, 2);
            if (suggestPlan.StartDate == null) { return new BusinessResult(400, "This Plan do not have StartDate"); }
            var ratioDate = (suggestPlan.StartDate - plan.StartDate).Value.Days;
            foreach (var care in suggestPlan.CaringTasks)
            {
                var caringtask = _mapper.Map<CreateCaringPlan>(care);
                caringtask.PlanId = planId;
                caringtask.StartDate = caringtask.StartDate.AddDays(ratioDate);
                caringtask.EndDate = caringtask.EndDate.AddDays(ratioDate);
                foreach (var pesticide in care.CaringPesticides)
                {
                    var pesticideTask = _mapper.Map<PesCare>(pesticide);
                    pesticideTask.Quantity = pesticideTask.Quantity * ratioEstimate;
                    caringtask.Pesticides.Add(pesticideTask);
                }
                foreach (var item in care.CaringItems)
                {
                    var ItemTask = _mapper.Map<ItemCare>(item);
                    ItemTask.Quantity = (int)(ItemTask.Quantity * ratioEstimate);
                    caringtask.Items.Add(ItemTask);
                }
                foreach (var fertilizer in care.CaringFertilizers)
                {
                    var fertilizerTask = _mapper.Map<FerCare>(fertilizer);
                    fertilizerTask.Quantity = (int)fertilizerTask.Quantity * ratioEstimate;
                    caringtask.Fertilizers.Add(fertilizerTask);
                }
                result.CreateCaringPlans.Add(caringtask);
            }
            foreach (var harvest in suggestPlan.HarvestingTasks)
            {
                var harvestingTask = _mapper.Map<CreateHarvestingPlan>(harvest);
                harvest.PlanId = planId;
                harvest.StartDate = harvest.StartDate.AddDays(ratioDate);
                harvest.EndDate = harvest.EndDate.AddDays(ratioDate);
                foreach (var item in harvest.HarvestingItems)
                {
                    var ItemTask = _mapper.Map<HarvestItem>(item);
                    ItemTask.Quantity = (int)(ItemTask.Quantity * ratioEstimate);
                    harvestingTask.Items.Add(ItemTask);
                }
                result.CreateHarvestingPlans.Add(harvestingTask);
            }
            foreach (var inspect in suggestPlan.InspectingForms)
            {
                var inspectingForm = _mapper.Map<CreateInspectingPlan>(inspect);
                inspectingForm.PlanId = planId;
                inspectingForm.StartDate = inspectingForm.StartDate.AddDays(ratioDate);
                inspectingForm.EndDate = inspectingForm.EndDate.AddDays(ratioDate);
                result.CreateInspectingPlans.Add(inspectingForm);
            }
            foreach (var packing in suggestPlan.PackagingTasks)
            {
                var packagingTasks = _mapper.Map<CreatePackagingPlan>(packing);
                packagingTasks.PlanId = planId;
                packagingTasks.StartDate = packagingTasks.StartDate.AddDays(ratioDate);
                packagingTasks.EndDate = packagingTasks.EndDate.AddDays(ratioDate);
                foreach (var item in packing.PackagingItems)
                {
                    var ItemTask = _mapper.Map<PackageItem>(item);
                    ItemTask.Quantity = (int)(ItemTask.Quantity * ratioEstimate);
                    packagingTasks.Items.Add(ItemTask);
                }
                result.CreatePackagingPlans.Add(packagingTasks);
            }
            return new BusinessResult(200, "Suggest Tasks by PlanId", result);
        }

        public async Task<IBusinessResult> GetSuggestPlansByPlanId(int planId)
        {
            var plan = await _unitOfWork.PlanRepository.GetByIdAsync(planId);
            if (plan == null) { return new BusinessResult(400, "Not found this plan"); }
            var list = await _unitOfWork.PlanRepository.GetSuggestPlansByPlanId(plan.PlantId, planId, plan.EstimatedProduct.Value);
            var result = _mapper.Map<List<PlanModel>>(list);
            return new BusinessResult(200, "Get list suggested plans by plan id", result);
        }

        public async Task<IBusinessResult> CreateBigPlan(CreatePlanTemplate model)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var newPlan = _mapper.Map<Plan>(model);
                newPlan.Status = "Draft";
                newPlan.IsApproved = false;
                newPlan.CreatedAt = DateTime.Now;

                var plan = await _unitOfWork.PlanRepository.CreateAsync(newPlan);

                if (model.Orders != null)
                {
                    foreach (var order in model.Orders)
                    {
                        var existed = await _unitOfWork.OrderRepository.GetOrderByOrderId(order.OrderId);
                        if (order == null)
                        {
                            return new BusinessResult(404, $"Not found order {order.OrderId} !");
                        }
                    }
                }

                if (model.Orders != null)
                {
                    foreach (var o in model.Orders)
                    {
                        await _unitOfWork.OrderPlanRepository.CreateAsync(new OrderPlan
                        {
                            OrderId = o.OrderId,
                            PlanId = plan.Id,
                            Quantity = o.Quantity
                        });
                    }
                }

                foreach (var care in model.PlanCaringTasks)
                {
                    var careTask = _mapper.Map<CaringTask>(care);
                    careTask.PlanId = plan.Id;
                    careTask.Status = "Draft";
                    careTask.CreatedAt = DateTime.Now;
                    var caring = await _unitOfWork.CaringTaskRepository.CreateAsync(careTask);

                    foreach (var fer in care.Fertilizers)
                    {
                        await _unitOfWork.CaringFertilizerRepository.CreateAsync(new CaringFertilizer
                        {
                            FertilizerId = fer.FertilizerId,
                            TaskId = caring.Id,
                            Quantity = fer.Quantity,
                            Unit = fer.Unit
                        });
                    }

                    foreach (var pes in care.Pesticides)
                    {
                        await _unitOfWork.CaringPesticideRepository.CreateAsync(new CaringPesticide
                        {
                            PesticideId = pes.PesticideId,
                            TaskId = caring.Id,
                            Quantity = pes.Quantity,
                            Unit = pes.Unit
                        });
                    }

                    foreach (var item in care.Items)
                    {
                        await _unitOfWork.CaringItemRepository.CreateAsync(new CaringItem
                        {
                            ItemId = item.ItemId,
                            TaskId = caring.Id,
                            Quantity = item.Quantity,
                            Unit = item.Unit
                        });
                    }
                }

                foreach (var har in model.PlanHarvestingTasks)
                {
                    var harTask = _mapper.Map<HarvestingTask>(har);
                    harTask.PlanId = plan.Id;
                    harTask.Status = "Draft";
                    harTask.CreatedAt = DateTime.Now;
                    var harvesting = await _unitOfWork.HarvestingTaskRepository.CreateAsync(harTask);

                    foreach (var item in har.Items)
                    {
                        await _unitOfWork.HarvestingItemRepository.CreateAsync(new HarvestingItem
                        {
                            ItemId = item.ItemId,
                            TaskId = harvesting.Id,
                            Quantity = item.Quantity,
                            Unit = item.Unit
                        });
                    }
                }

                foreach (var form in model.PlanInspectingForms)
                {
                    var insForm = _mapper.Map<InspectingForm>(form);
                    insForm.PlanId = plan.Id;
                    insForm.Status = "Draft";
                    insForm.CreatedAt = DateTime.Now;
                    var ff = await _unitOfWork.InspectingFormRepository.CreateAsync(insForm);
                }

                foreach (var pack in model.PlanPackagingTasks)
                {
                    var paTask = _mapper.Map<PackagingTask>(pack);
                    paTask.PlanId = plan.Id;
                    paTask.Status = "Draft";
                    paTask.CreatedAt = DateTime.Now;
                    var packaging = await _unitOfWork.PackagingTaskRepository.CreateAsync(paTask);

                    foreach (var item in pack.Items)
                    {
                        await _unitOfWork.PackagingItemRepository.CreateAsync(new PackagingItem
                        {
                            ItemId = item.ItemId,
                            TaskId = packaging.Id,
                            Quantity = item.Quantity,
                            Unit = item.Unit
                        });
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
                return new BusinessResult(200, "Create plan success !", model);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetPlanOrderById(int id)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);
                if (plan == null)
                {
                    return new BusinessResult(404, "Not found any plan !");
                }

                var res = await _unitOfWork.PlanRepository.GetPlan(id);
                var rs = _mapper.Map<PlanOrderModel>(res);

                return new BusinessResult(200, "Plan Order: ", rs);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetTemplatePlan(RequestTemplatePlan model)
        {
            var result = new List<CreatePlanTemplate>();
            var templates = new List<PlanTemplate>();
            var list = await _unitOfWork.SeasonalPlantRepository.GetSeasonalPlantByPlantIdAndDay(model.PlantId, model.StartDate);
            foreach (var obj in list)
            {
                var plan = new CreatePlanTemplate();
                plan.PlantId = model.PlantId;
                plan.StartDate = model.StartDate;
                plan.EndDate = model.StartDate.AddDays(obj.DurationDays);
                plan.PlantId = model.PlantId;
                plan.EstimatedProduct = model.EstimatedProduct;
                plan.SeasonName = obj.SeasonType;
                plan.SeedQuantity = model.SeedQuantity;
                plan.YieldId = model.YieldId;
                plan.ExpertId = model.ExpertId;
                plan.CreatedBy = model.CreatedBy;
                float rate = (model.SeedQuantity * 1.0f) / 100;
                string json = obj.TemplatePlan;
                PlanTemplate template = JsonSerializer.Deserialize<PlanTemplate>(json);
                if (model.Orders != null)
                {
                    foreach (var order in model.Orders)
                    {
                        var orderTask = await _unitOfWork.OrderRepository.GetOrderByIdAsync(order.Id);
                        var packagingTask = new PlanPack();
                        if (orderTask == null) { return new BusinessResult(400, "Not found this Order"); }
                        if (orderTask.PlantId != model.PlantId) { return new BusinessResult(400, "Order do not order that plant"); }
                        packagingTask.PackagingTypeId = orderTask.PackagingTypeId;
                        packagingTask.TotalPackagedWeight = order.Quantity;
                        packagingTask.TaskName = "Đóng gói cho Order " + orderTask.Id;
                        packagingTask.Description = "Đóng gói theo loại " + orderTask.PackagingType.Name;
                        packagingTask.EndDate = orderTask.EstimatedPickupDate.AddDays(-0.5).Date;
                        packagingTask.CreatedBy = model.CreatedBy;
                        packagingTask.StartDate = orderTask.EstimatedPickupDate.AddDays(-1.5).Date;
                        plan.PlanPackagingTasks.Add(packagingTask);
                        plan.Orders.Add(new PO { OrderId = order.Id, Quantity = order.Quantity });
                    }

                }
                foreach (var caring in template.CaringTasks)
                {
                    var caringTask = new PlanCare();
                    caringTask.StartDate = model.StartDate.AddHours(caring.StartIn);
                    caringTask.EndDate = model.StartDate.AddHours(caring.EndIn);
                    caringTask.Description = caring.Description;
                    caringTask.TaskName = caring.TaskName;
                    caringTask.TaskType = caring.TaskType;
                    caringTask.CreatedBy = model.CreatedBy;
                    caring.Items.ForEach(r =>
                    {
                        var ItemCare = new ItemCare { ItemId = r.ItemId, Quantity = 1, Unit = r.Unit };
                        caringTask.Items.Add(ItemCare);
                    });
                    caring.Fertilizers.ForEach(r =>
                    {
                        var fertilizerCare = new FerCare { FertilizerId = r.FertilizerId, Quantity = r.Quantity * rate, Unit = r.Unit };
                        caringTask.Fertilizers.Add(fertilizerCare);
                    });
                    caring.Pesticides.ForEach(r =>
                    {
                        var pesticideCare = new PesCare { PesticideId = r.PesticideId, Quantity = r.Quantity * rate, Unit = r.Unit };
                        caringTask.Pesticides.Add(pesticideCare);
                    });
                    plan.PlanCaringTasks.Add(caringTask);
                }
                foreach (var harvesting in template.HarvestingTaskTemplates)
                {
                    var harvestTask = new PlanHar();
                    harvesting.Items.ForEach(r =>
                    {
                        var ItemCare = new HarvestItem { ItemId = r.ItemId, Quantity = 1, Unit = r.Unit };
                        harvestTask.Items.Add(ItemCare);
                    });
                    harvestTask.StartDate = model.StartDate.AddHours(harvesting.StartIn);
                    harvestTask.EndDate = model.StartDate.AddHours(harvesting.EndIn);
                    harvestTask.Description = harvesting.Description;
                    harvestTask.CreatedBy = model.CreatedBy;
                    harvestTask.TaskName = "Thu hoạch";
                    plan.PlanHarvestingTasks.Add(harvestTask);
                }
                foreach (var inspecting in template.InspectingTasks)
                {
                    var inspectingTask = new PlanForm();
                    inspectingTask.FormName = inspecting.FormName;
                    inspectingTask.Description = inspecting.Description;
                    inspectingTask.StartDate = model.StartDate.AddHours(inspecting.StartIn);
                    inspectingTask.EndDate = model.StartDate.AddHours(inspecting.EndIn);
                    inspectingTask.CreatedBy = model.CreatedBy;
                    plan.PlanInspectingForms.Add(inspectingTask);
                }
                result.Add(plan);
            }
            return new BusinessResult(200, "Get Template", result);
        }

        public async Task<IBusinessResult> NotificationforExperts(NotificationExpertsRequest model)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            var qrString = model.Url;
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrString, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(5);
            
            string base64String = Convert.ToBase64String(qrCodeAsPngByteArr, 0, qrCodeAsPngByteArr.Length);
            string imgSrc = $"data:image/png;base64,{base64String}";

            foreach(var customer in model.Infors)
            {
                var body = EmailHelper.GetEmailBody("QRCodeSend.html", new Dictionary<string, string>
                {
                    { "{{userName}}", customer.Name },
                    { "{{srcQRCode}}", imgSrc }
                });

                await EmailHelper.SendMail(customer.Email, "BFARMX - Blockchain FarmXperience xin gửi bạn QR Code!", customer.Name, body);
            }
            
            return new BusinessResult(200,"Send QR code successfull !");
        }
    }
}