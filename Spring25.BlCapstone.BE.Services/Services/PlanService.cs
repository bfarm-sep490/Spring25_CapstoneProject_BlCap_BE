using AutoMapper;
using QRCoder;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.BlockChain;
using Spring25.BlCapstone.BE.Repositories.Dashboards;
using Spring25.BlCapstone.BE.Repositories.Helper;
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
using Spring25.BlCapstone.BE.Services.Untils;
using Spring25.BlCapstone.BE.Services.Utils;
using System;
using System.Collections.Generic;
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
        Task<IBusinessResult> RejectPlan(int id, string? reason);
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

                return new BusinessResult(200, "Plan ne", res);
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

                return new BusinessResult(200, "List of Plans", rs.OrderByDescending(c => c.CreatedAt));

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

                return new BusinessResult(200, "Plan ne", res);
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
                    return new BusinessResult { Status = 400, Message = "Plan not existed !", Data = null };
                }

                var probs = await _unitOfWork.ProblemRepository.GetProblemByPlanId(planId);

                var res = _mapper.Map<List<ProblemPlan>>(probs);

                return new BusinessResult { Status = 200, Message = "Problems ne!", Data = res };
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
                    return new BusinessResult { Status = 400, Message = "Plan not existed !", Data = null };
                }

                var farmers = await _unitOfWork.FarmerRepository.GetFarmersByPlanId(planId);

                var res = _mapper.Map<List<FarmerPlan>>(farmers);

                return new BusinessResult { Status = 200, Message = "Farmers ne!", Data = res };
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
                    return new BusinessResult { Status = 400, Message = "Plan not existed !", Data = null };
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
                    return new BusinessResult { Status = 400, Message = "Not found any Plans", Data = null };
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
                                CreatedAt = DateTimeHelper.NowVietnamTime(),
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
                                CreatedAt = DateTimeHelper.NowVietnamTime(),
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
                                CreatedAt = DateTimeHelper.NowVietnamTime(),
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
                                CreatedAt = DateTimeHelper.NowVietnamTime(),
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
                    return new BusinessResult { Status = 400, Message = "Not found any plan!", Data = null };
                }

                if (!plan.YieldId.HasValue)
                {
                    return new BusinessResult(400, "Plan does not have any yield. Can not approve!");
                }

                if (string.IsNullOrEmpty(plan.PlanName))
                {
                    return new BusinessResult(400, "Plan does not have a name. Can not approve!");
                }

                if (string.IsNullOrEmpty(plan.Description))
                {
                    return new BusinessResult(400, "Plan does not have a description. Can not approve!");
                }

                if (!plan.StartDate.HasValue)
                {
                    return new BusinessResult(400, "Plan does not have a start date. Can not approve!");
                }

                if (!plan.EndDate.HasValue)
                {
                    return new BusinessResult(400, "Plan does not have an end date. Can not approve!");
                }

                if (!plan.EstimatedProduct.HasValue)
                {
                    return new BusinessResult(400, "Plan does not have an estimated product. Can not approve!");
                }

                if (!plan.SeedQuantity.HasValue)
                {
                    return new BusinessResult(400, "Plan does not have a seed quantity. Can not approve!");
                }

                if (!plan.Status.ToLower().Trim().Equals("pending"))
                {
                    return new BusinessResult(400, "Can not approve plan that not have status pending!");
                }

                var orderIds = await _unitOfWork.OrderPlanRepository.GetListOrderIdsByPlanId(id);
                if (orderIds.Count > 0)
                {
                    var isEnoughPackTask = await _unitOfWork.PackagingTaskRepository.ArePackagingTasksEnoughForAllOrderIds(id, orderIds);
                    if (!isEnoughPackTask)
                    {
                        return new BusinessResult(400, "Can not approve plan because you have not create packaging task for your order in plan yet !");
                    }

                    var isEnoughTotalPackaging = await _unitOfWork.PackagingTaskRepository.AreAllPreOrdersRequiredHasPackagedProduct(id);
                    if (!isEnoughTotalPackaging)
                    {
                        return new BusinessResult(400, "There is an order you have not create task to pack it enough as their pre-order quantity!");
                    }
                }

                var plant = await _unitOfWork.PlantRepository.GetByIdAsync(plan.PlantId);
                if (plant == null)
                {
                    return new BusinessResult(400, "Can not approve plan that not have seed");
                }

                if (plant.Quantity < plan.SeedQuantity)
                {
                    return new BusinessResult(400, "Seed quantity available in system is not enough for this plan. Please add more seed in system !");
                }

                var itemCs = await _unitOfWork.CaringItemRepository.GetCaringItemByPlanId(id);
                var itemQuantityDict = itemCs
                                    .GroupBy(x => x.ItemId)
                                    .ToDictionary(g => g.Key, g => g.Sum(x => x.Quantity));
                foreach (var kvp in itemQuantityDict)
                {
                    var itemId = kvp.Key;
                    var totalQuantityNeeded = kvp.Value;

                    var item = await _unitOfWork.ItemRepository.GetByIdAsync(itemId);

                    if (item.Quantity < totalQuantityNeeded)
                    {
                        return new BusinessResult(400, $"Not enough {item.Name} in system !");
                    }

                    item.Quantity -= totalQuantityNeeded;
                    _unitOfWork.ItemRepository.PrepareUpdate(item);
                }

                var fers = await _unitOfWork.CaringFertilizerRepository.GetCareFertilizersByPlanId(id);
                var ferQuantityDict = fers
                                    .GroupBy(x => x.FertilizerId)
                                    .ToDictionary(g => g.Key, g => g.Sum(x => x.Quantity));
                foreach (var kvp in ferQuantityDict)
                {
                    var ferId = kvp.Key;
                    var totalQuantityNeeded = kvp.Value;

                    var fer = await _unitOfWork.FertilizerRepository.GetByIdAsync(ferId);

                    if (fer.Quantity < totalQuantityNeeded)
                    {
                        return new BusinessResult(400, $"Not enough {fer.Name} in system !");
                    }

                    fer.Quantity -= totalQuantityNeeded;
                    _unitOfWork.FertilizerRepository.PrepareUpdate(fer);
                }

                var pests = await _unitOfWork.CaringPesticideRepository.GetCarePesticidesByPlanId(id);
                var pesQuantityDict = pests
                                    .GroupBy(x => x.PesticideId)
                                    .ToDictionary(g => g.Key, g => g.Sum(x => x.Quantity));
                foreach (var kvp in pesQuantityDict)
                {
                    var pesId = kvp.Key;
                    var totalQuantityNeeded = kvp.Value;

                    var pes = await _unitOfWork.PesticideRepository.GetByIdAsync(pesId);

                    if (pes.Quantity < totalQuantityNeeded)
                    {
                        return new BusinessResult(400, $"Not enough {pes.Name} in system !");
                    }

                    pes.Quantity -= totalQuantityNeeded;
                    _unitOfWork.PesticideRepository.PrepareUpdate(pes);
                }

                var itemHs = await _unitOfWork.HarvestingItemRepository.GetHarvestingItemByPlanId(id);
                var itemHQuantityDict = itemHs
                                    .GroupBy(x => x.ItemId)
                                    .ToDictionary(g => g.Key, g => g.Sum(x => x.Quantity));
                foreach (var kvp in itemHQuantityDict)
                {
                    var itemId = kvp.Key;
                    var totalQuantityNeeded = kvp.Value;

                    var item = await _unitOfWork.ItemRepository.GetByIdAsync(itemId);

                    if (item.Quantity < totalQuantityNeeded)
                    {
                        return new BusinessResult(400, $"Not enough {item.Name} in system !");
                    }

                    item.Quantity -= totalQuantityNeeded;
                    _unitOfWork.ItemRepository.PrepareUpdate(item);
                }

                var itemPs = await _unitOfWork.PackagingItemRepository.GetPackagingItemByPlanId(id);
                var itemPQuantityDict = itemHs
                                    .GroupBy(x => x.ItemId)
                                    .ToDictionary(g => g.Key, g => g.Sum(x => x.Quantity));
                foreach (var kvp in itemPQuantityDict)
                {
                    var itemId = kvp.Key;
                    var totalQuantityNeeded = kvp.Value;

                    var item = await _unitOfWork.ItemRepository.GetByIdAsync(itemId);

                    if (item.Quantity < totalQuantityNeeded)
                    {
                        return new BusinessResult(400, $"Not enough {item.Name} in system !");
                    }

                    item.Quantity -= totalQuantityNeeded;
                    _unitOfWork.ItemRepository.PrepareUpdate(item);
                }

                plant.Quantity -= plan.SeedQuantity.Value;
                _unitOfWork.PlantRepository.PrepareUpdate(plant);

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
                else
                {
                    return new BusinessResult(400, "Plan does not have any Inspecting Form");
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
                await _unitOfWork.PlantRepository.SaveAsync();
                await _unitOfWork.ItemRepository.SaveAsync();
                await _unitOfWork.FertilizerRepository.SaveAsync();
                await _unitOfWork.PesticideRepository.SaveAsync();

                var orders = await _unitOfWork.OrderRepository.GetAllOrdersByPlanId(id);
                foreach (var order in orders)
                {
                    var retaileraChanel = $"retailer-{order.RetailerId}";
                    var message = "Kế hoạch trồng cây cho đơn hàng của bạn đã chính thức đi vào hoạt động. Vui lòng kiểm tra email thường xuyên để cập nhật những thông tin mới nhất về đơn hàng.";
                    var title = $"Kế hoạch trồng cây đã bắt đầu - {plant.PlantName}";
                    await AblyHelper.SendMessageWithChanel(title, message, retaileraChanel);
                    await _unitOfWork.NotificationRetailerRepository.CreateAsync(new NotificationRetailer
                    {
                        RetailerId = order.RetailerId,
                        Message = message,
                        Title = title,
                        IsRead = false,
                        Image = plant.ImageUrl,
                        CreatedDate = DateTimeHelper.NowVietnamTime()
                    });
                }

                var farmers = await _unitOfWork.FarmerRepository.GetFarmersByPlanId(id);
                foreach (var farmer in farmers)
                {
                    var message = "Kế hoạch đã được duyệt và chính thức đi vào hoạt động. Vui lòng kiểm tra lại lịch làm việc và kế hoạch để chuẩn bị thực hiện các công việc theo kế hoạch. Chúc bạn làm việc hiệu quả và đạt được kết quả tốt trong quá trình thực hiện!";
                    var title = $"Kế hoạch {plan.PlanName} đã chính thức triển khai – Kiểm tra lịch làm việc và bắt đầu!";
                    await AblyHelper.SendMessageToDevice(title, message, farmer.FarmerId);
                    await _unitOfWork.NotificationFarmerRepository.CreateAsync(new NotificationFarmer
                    {
                        FarmerId = farmer.FarmerId,
                        Message = message,
                        Title = title,
                        IsRead = false,
                        CreatedDate = DateTimeHelper.NowVietnamTime(),
                    });
                }

                var inspectors = await _unitOfWork.InspectorRepository.GetInspectorsByPlanId(id);
                foreach (var inspector in inspectors)
                {
                    var inspectorChanel = $"inspector-{inspector.Id}";
                    var message = $"Kế hoạch đã chính thức đi vào hoạt động. Quý đơn vị có một đợt kiểm định dự kiến vào ngày {plan.InspectingForms.FirstOrDefault(c => c.InspectorId == inspector.Id).StartDate.ToString("dd MMMM, yyyy")}, trước thời điểm thu hoạch.Vui lòng ghi nhớ hoặc đặt lời nhắc cho thời điểm này để tiến hành lấy mẫu và thực hiện kiểm định đúng hạn, đảm bảo tiến độ và chất lượng của kế hoạch. Xin cảm ơn sự phối hợp của quý đơn vị!";
                    var title = $"Kế hoạch trồng {plan.PlanName} đã bắt đầu – Chuẩn bị cho kiểm định trước thu hoạch!";
                    await AblyHelper.SendMessageWithChanel(title, message, inspectorChanel);
                    await _unitOfWork.NotificationInspectorRepository.CreateAsync(new NotificationInspector
                    {
                        InspectorId = inspector.Id,
                        Message = message,
                        Title = title,
                        IsRead = false,
                        CreatedDate = DateTimeHelper.NowVietnamTime(),
                    });
                }

                var expert = await _unitOfWork.ExpertRepository.GetByIdAsync(plan.ExpertId);
                var expertChanel = $"expert-{expert.Id}";
                var m = "Kế hoạch của bạn đã được phê duyệt và chính thức đi vào hoạt động. Vui lòng xem lại nội dung kế hoạch và bắt đầu theo dõi quá trình thực hiện để đảm bảo chất lượng nông sản đạt mức tối ưu. Cảm ơn bạn đã đồng hành cùng chúng tôi trong hành trình nâng cao hiệu quả sản xuất nông nghiệp!";
                var t = $"Kế hoạch {plan.PlanName} đã được phê duyệt – Bắt đầu theo dõi và đồng hành cùng nông dân!";
                await AblyHelper.SendMessageWithChanel(t, m, expertChanel);
                await _unitOfWork.NotificationExpertRepository.CreateAsync(new NotificationExpert
                {
                    ExpertId = expert.Id,
                    Message = m,
                    Title = t,
                    IsRead = false,
                    CreatedDate = DateTimeHelper.NowVietnamTime(),
                });

                var ownerChanel = "owner";
                var to = "Kế hoạch đã được duyệt thành công";
                var mo = "Bạn đã duyệt kế hoạch thành công. Kế hoạch hiện đã chính thức đi vào hoạt động và các bên liên quan đã được thông báo để bắt đầu thực hiện. Cảm ơn bạn đã hoàn tất bước quan trọng trong quy trình quản lý kế hoạch!";
                await AblyHelper.SendMessageWithChanel(to, mo, ownerChanel);
                await _unitOfWork.NotificationOwnerRepository.CreateAsync(new NotificationOwner
                {
                    OwnerId = 1,
                    Message = mo,
                    Title = to,
                    IsRead = false,
                    CreatedDate = DateTimeHelper.NowVietnamTime(),
                });

                return new BusinessResult { Status = 200, Message = "Approve success", Data = null };
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }

        public async Task<IBusinessResult> RejectPlan(int id, string? reason)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(id);
                if (plan == null)
                {
                    return new BusinessResult(400, "Not found any plans !");
                }

                plan.Status = "Draft";
                _unitOfWork.PlanRepository.PrepareUpdate(plan);

                var caringTasks = await _unitOfWork.CaringTaskRepository.GetAllCaringTasks(planId: id);
                foreach (var caringTask in caringTasks)
                {
                    caringTask.Status = "Draft";
                    _unitOfWork.CaringTaskRepository.PrepareUpdate(caringTask);
                }

                var harvestingTasks = await _unitOfWork.HarvestingTaskRepository.GetHarvestingTasks(planId: id);
                foreach (var harvestingTask in harvestingTasks)
                {
                    harvestingTask.Status = "Draft";
                    _unitOfWork.HarvestingTaskRepository.PrepareUpdate(harvestingTask);
                }

                var packagingTasks = await _unitOfWork.PackagingTaskRepository.GetPackagingTasks(planId: id);
                foreach (var packagingTask in packagingTasks)
                {
                    packagingTask.Status = "Draft";
                    _unitOfWork.PackagingTaskRepository.PrepareUpdate(packagingTask);
                }

                var inspectingForms = await _unitOfWork.InspectingFormRepository.GetInspectingForms(planId: id);
                foreach (var inspectingForm in inspectingForms)
                {
                    inspectingForm.Status = "Draft";
                    _unitOfWork.InspectingFormRepository.PrepareUpdate(inspectingForm);
                }

                await _unitOfWork.PlanRepository.SaveAsync();
                await _unitOfWork.CaringTaskRepository.SaveAsync();
                await _unitOfWork.HarvestingTaskRepository.SaveAsync();
                await _unitOfWork.PackagingTaskRepository.SaveAsync();
                await _unitOfWork.InspectingFormRepository.SaveAsync();

                var expert = await _unitOfWork.ExpertRepository.GetExpert(plan.ExpertId);
                var expertChanel = $"expert-{expert.Id}";
                var message = $"Kế hoạch của bạn đã bị từ chối với lí do: {reason}. Bạn vẫn có thể xem kế hoạch đấy dưới dạng nháp ở mục Kế Hoạch nhé !";
                var title = $"Kế hoạch {plan.PlanName} của bạn đã bị chủ trang trại từ chối!";
                await _unitOfWork.NotificationExpertRepository.CreateAsync(new NotificationExpert
                {
                    ExpertId = expert.Id,
                    Message = message,
                    Title = title,
                    IsRead = false,
                    CreatedDate = DateTimeHelper.NowVietnamTime()
                });
                await AblyHelper.SendMessageWithChanel(title, message, expertChanel);

                return new BusinessResult(200, "Reject plan success !");
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
                    return new BusinessResult(400, "Not found any plant!");
                }

                if (model.YieldId != null)
                {
                    var yield = await _unitOfWork.YieldRepository.GetByIdAsync(model.YieldId.Value);
                    if (yield == null)
                    {
                        return new BusinessResult(400, "Not found any yield!");
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
                            return new BusinessResult(400, $"Not found order {order.OrderId} !");
                        }
                    }
                }

                var expert = await _unitOfWork.ExpertRepository.GetExpert(model.ExpertId);
                if (expert == null)
                {
                    return new BusinessResult(400, "Not found any expert!");
                }

                var plan = _mapper.Map<Plan>(model);
                plan.Status = "Draft";
                plan.CreatedAt = DateTimeHelper.NowVietnamTime();
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
                    return new BusinessResult(400, "Not found any plans !");
                }

                plan.Status = status;
                plan.UpdatedAt = DateTimeHelper.NowVietnamTime();
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
                    return new BusinessResult(400, "Not found any plan !");
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
                    return new BusinessResult(400, "Not found any plans !");
                }

                plan.YieldId = model.YieldId;
                plan.PlantId = model.PlantId;
                plan.PlanName = model.PlanName;
                plan.Description = model.Description;
                plan.EstimatedProduct = model.EstimatedProduct;
                plan.ExpertId = model.ExpertId;
                plan.StartDate = model.StartDate;
                plan.EndDate = model.EndDate;
                plan.UpdatedAt = DateTimeHelper.NowVietnamTime();
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
                    return new BusinessResult(400, "Not found any farmers !");
                }

                var plans = await _unitOfWork.FarmerPermissionRepository.GetPlanFarmerAssign(id, is_active_in_plan, status);
                var rs = _mapper.Map<List<PlanListFarmerAssignTo>>(plans);

                if (rs.Count <= 0)
                {
                    return new BusinessResult(400, "Not found any plans !");
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
            if (plan == null) return new BusinessResult(400, "Plan not found !", null);

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
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(planId);
                if (plan == null)
                {
                    return new BusinessResult(400, "Not found any plan !");
                }

                if (!plan.Status.ToLower().Trim().Equals("pending") && !plan.Status.ToLower().Trim().Equals("draft"))
                {
                    return new BusinessResult(400, "Can not remove farmer if plan is not pending or draft!");
                }

                var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(farmerId);
                if (farmer == null)
                {
                    return new BusinessResult(400, "Not found any farmer !");
                }

                var farmersInCaring = await _unitOfWork.FarmerCaringTaskRepository.CheckFarmersAssignInPlan(planId, farmerId);
                foreach (var f in farmersInCaring)
                {
                    _unitOfWork.FarmerCaringTaskRepository.PrepareRemove(f);
                }

                var farmersInHarvesting = await _unitOfWork.FarmerHarvestingTaskRepository.CheckFarmersAssignInPlan(planId, farmerId);
                foreach (var f in farmersInHarvesting)
                {
                    _unitOfWork.FarmerHarvestingTaskRepository.PrepareRemove(f);
                }

                var farmerPackaging = await _unitOfWork.FarmerPackagingTaskRepository.CheckFarmersAssignInPlan(planId, farmerId);
                foreach (var f in farmerPackaging)
                {
                    _unitOfWork.FarmerPackagingTaskRepository.PrepareRemove(f);
                }

                await _unitOfWork.FarmerCaringTaskRepository.SaveAsync();
                await _unitOfWork.FarmerHarvestingTaskRepository.SaveAsync();
                await _unitOfWork.FarmerPackagingTaskRepository.SaveAsync();

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
                    return new BusinessResult(400, "Not found any plan !");
                }

                var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(farmerId);
                if (farmer == null)
                {
                    return new BusinessResult(400, "Not found any farmers");
                }

                await _unitOfWork.FarmerPermissionRepository.CreateAsync(new FarmerPermission
                {
                    CreatedAt = DateTimeHelper.NowVietnamTime(),
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
                    return new BusinessResult(400, "Not found any plan !");
                }

                var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
                if (order == null)
                {
                    return new BusinessResult(400, "Not found any order !");
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
                    return new BusinessResult(400, "Not found any plan !");
                }

                var order = await _unitOfWork.OrderRepository.GetOrderByOrderId(orderId);
                if (order == null)
                {
                    return new BusinessResult(400, "Not found any order !");
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
                    return new BusinessResult(400, "Not found any plan !");
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

                return new BusinessResult(200, "Plans that farmer assigned in : ", result);
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
                    return new BusinessResult(400, "Not found any plans !");
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
                    f.ExpiredDate = DateTimeHelper.NowVietnamTime();
                    _unitOfWork.FarmerCaringTaskRepository.PrepareUpdate(f);
                });

                var farmerHarvesting = await _unitOfWork.FarmerHarvestingTaskRepository.GetFarmerHarvestingTasksByPlanId(id);
                farmerHarvesting.ForEach(f =>
                {
                    f.Status = "Inactive";
                    f.ExpiredDate = DateTimeHelper.NowVietnamTime();
                    _unitOfWork.FarmerHarvestingTaskRepository.PrepareUpdate(f);
                });

                var farmerPackaging = await _unitOfWork.FarmerPackagingTaskRepository.GetFarmerPackagingTasksByPlanId(id);
                farmerPackaging.ForEach(f =>
                {
                    f.Status = "Inactive";
                    f.ExpiredDate = DateTimeHelper.NowVietnamTime();
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

                var orders = await _unitOfWork.OrderRepository.GetAllOrdersByPlanId(id);
                var plant = await _unitOfWork.PlantRepository.GetByIdAsync(plan.PlantId);
                foreach (var order in orders)
                {
                    var retaileraChanel = $"retailer-{order.RetailerId}";
                    var message = "Kế hoạch trồng cây trong đơn hàng của bạn đã hoàn tất. Vui lòng đến nhận cây đúng với ngày dự kiến mà bạn đã cập nhật trong đơn hàng để đảm bảo chất lượng sản phẩm tốt nhất." +
                        $"\n Ngày dự kiến đến lấy: {order.EstimatedPickupDate.ToString("MMM-dd, yyyy HH:mm")}";
                    var title = $"Kế hoạch trồng cây đã hoàn tất – Sẵn sàng giao cây - {plant.PlantName}";
                    await AblyHelper.SendMessageWithChanel(title, message, retaileraChanel);
                    await _unitOfWork.NotificationRetailerRepository.CreateAsync(new NotificationRetailer
                    {
                        RetailerId = order.RetailerId,
                        Message = message,
                        Title = title,
                        Image = plant.ImageUrl,
                        IsRead = false,
                        CreatedDate = DateTimeHelper.NowVietnamTime(),
                    });
                }

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
                    return new BusinessResult(400, "Not found any plans !");
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
                    f.ExpiredDate = DateTimeHelper.NowVietnamTime();
                    _unitOfWork.FarmerCaringTaskRepository.PrepareUpdate(f);
                });

                var farmerHarvesting = await _unitOfWork.FarmerHarvestingTaskRepository.GetFarmerHarvestingTasksByPlanId(id);
                farmerHarvesting.ForEach(f =>
                {
                    f.Status = "Inactive";
                    f.ExpiredDate = DateTimeHelper.NowVietnamTime();
                    _unitOfWork.FarmerHarvestingTaskRepository.PrepareUpdate(f);
                });

                var farmerPackaging = await _unitOfWork.FarmerPackagingTaskRepository.GetFarmerPackagingTasksByPlanId(id);
                farmerPackaging.ForEach(f =>
                {
                    f.Status = "Inactive";
                    f.ExpiredDate = DateTimeHelper.NowVietnamTime();
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
                    return new BusinessResult(400, "Not found any plans !");
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

                var expert = await _unitOfWork.ExpertRepository.GetExpert(plan.ExpertId);
                var expertChanel = $"expert-{expert.Id}";
                var message = "Kế hoạch bạn vừa tạo đã được gửi lên chủ trang trại để xem xét. Vui lòng đợi trong thời gian ngắn để chủ trang trại xem qua kế hoạch và phản hồi. Chúng tôi sẽ thông báo ngay khi có cập nhật mới.!";
                var title = $"Kế hoạch {plan.PlanName} của bạn đã được gửi lên chủ trang trại – Vui lòng đợi phản hồi!";
                await _unitOfWork.NotificationExpertRepository.CreateAsync(new NotificationExpert
                {
                    ExpertId = expert.Id,
                    Message = message,
                    Title = title,
                    IsRead = false,
                    CreatedDate = DateTimeHelper.NowVietnamTime()
                });
                await AblyHelper.SendMessageWithChanel(title, message, expertChanel);

                var ownerChanel = "owner";
                var mo = "Bạn nhận được yêu cầu chờ duyệt kế hoạch mới, hãy check phần kế hoạch với trạng thái chờ duyệt để duyệt kế hoạch đi vào hoạt động!";
                var to = $"Bạn nhận được yêu cầu duyệt kế hoạch {plan.PlanName} của {expert.Account.Name} – Vui lòng phản hồi!";
                await _unitOfWork.NotificationOwnerRepository.CreateAsync(new NotificationOwner
                {
                    OwnerId = 1,
                    Message = message,
                    Title = title,
                    IsRead = false,
                    CreatedDate = DateTimeHelper.NowVietnamTime()
                });
                await AblyHelper.SendMessageWithChanel(title, message, ownerChanel);

                return new BusinessResult(200, "Public plan successfull !");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> CreateBigPlan(CreatePlanTemplate model)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var newPlan = _mapper.Map<Plan>(model);
                newPlan.Status = "Draft";
                newPlan.IsApproved = false;
                newPlan.CreatedAt = DateTimeHelper.NowVietnamTime();

                var plan = await _unitOfWork.PlanRepository.CreateAsync(newPlan);

                if (model.Orders != null)
                {
                    foreach (var order in model.Orders)
                    {
                        var existed = await _unitOfWork.OrderRepository.GetOrderByOrderId(order.OrderId);
                        if (order == null)
                        {
                            return new BusinessResult(400, $"Not found order {order.OrderId} !");
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
                    careTask.CreatedAt = DateTimeHelper.NowVietnamTime();
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
                    harTask.CreatedAt = DateTimeHelper.NowVietnamTime();
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
                    insForm.CreatedAt = DateTimeHelper.NowVietnamTime();
                    var ff = await _unitOfWork.InspectingFormRepository.CreateAsync(insForm);
                }

                foreach (var pack in model.PlanPackagingTasks)
                {
                    var paTask = _mapper.Map<PackagingTask>(pack);
                    paTask.PlanId = plan.Id;
                    paTask.Status = "Draft";
                    paTask.CreatedAt = DateTimeHelper.NowVietnamTime();
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
                    return new BusinessResult(400, "Not found any plan !");
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
                string json = obj.TemplatePlan;
                PlanTemplate template = JsonSerializer.Deserialize<PlanTemplate>(json);
                if (template != null)
                {
                    float rate = (model.SeedQuantity * 1.0f) / 100;
                    if (model.Orders != null)
                    {
                        foreach (var order in model.Orders)
                        {
                            var orderTask = await _unitOfWork.OrderRepository.GetOrderByIdAsync(order.Id);
                            var packagingTask = new PlanPack();
                            if (orderTask == null) { return new BusinessResult(400, "Not found this Order"); }
                            if (orderTask.PlantId != model.PlantId) { return new BusinessResult(400, "Order do not order that plant"); }
                            packagingTask.PackagingTypeId = orderTask.PackagingTypeId;
                            packagingTask.OrderId = orderTask.Id;
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
            }
            return new BusinessResult(200, "Get Template", result);
        }

        public async Task<IBusinessResult> NotificationforExperts(NotificationExpertsRequest model)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(model.Url, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(5);

            var (publicId, qrImageUrl) = await CloudinaryHelper.UploadImageQRCode(qrCodeAsPngByteArr);

            foreach (var customer in model.Infors)
            {
                var body = EmailHelper.GetEmailBody("QRCodeSend.html", new Dictionary<string, string>
                {
                    { "{{userName}}", customer.Name },
                    { "{{srcQRCode}}", qrImageUrl },
                    { "{{link}}", model.Url }
                });

                await EmailHelper.SendMail(customer.Email, "BFARMX - Blockchain FarmXperience xin gửi bạn QR Code!", customer.Name, body);
            }

            return new BusinessResult(200, "Send QR code successfull !");
        }
    }
}