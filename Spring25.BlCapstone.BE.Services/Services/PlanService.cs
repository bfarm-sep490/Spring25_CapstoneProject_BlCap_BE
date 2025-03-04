using AutoMapper;
using IO.Ably;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
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
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetGeneralPlan(int id);
        Task<IBusinessResult> GetAllProblems(int planId);
        Task<IBusinessResult> GetAllFarmers(int planId);
        Task<IBusinessResult> GetAllItems(int planId);
        Task<IBusinessResult> AssignTasks(int id, AssigningPlan model);
        Task<IBusinessResult> ApprovePlan(int id);
        Task<IBusinessResult> Create(CreatePlan model);
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

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetAllPlans();

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
                var plan = await _unitOfWork.PlantRepository.GetByIdAsync(planId);
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
                var plan = await _unitOfWork.PlantRepository.GetByIdAsync(planId);
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

                if (model.AssignCaringTasks.Any() && model.AssignCaringTasks.Count > 0)
                {
                    foreach (var task in model.AssignCaringTasks)
                    {
                        var caring = await _unitOfWork.CaringTaskRepository.GetByIdAsync(task.Id);
                        caring.FarmerId = task.FarmerId;
                        caring.Status = task.Status;

                        await _unitOfWork.CaringTaskRepository.UpdateAsync(caring);
                    }
                }

                if (model.AssignHarvestingTasks.Any() && model.AssignHarvestingTasks.Count > 0)
                {
                    foreach (var task in model.AssignHarvestingTasks)
                    {
                        var harvesting = await _unitOfWork.HarvestingTaskRepository.GetByIdAsync(task.Id);
                        harvesting.FarmerId = task.FarmerId;
                        harvesting.Status = task.Status;

                        await _unitOfWork.HarvestingTaskRepository.UpdateAsync(harvesting);
                    }
                }
                
                if (model.AssignInspectingTasks.Any() && model.AssignInspectingTasks.Count > 0)
                {
                    foreach (var task in model.AssignInspectingTasks)
                    {
                        var inspecting = await _unitOfWork.InspectingFormRepository.GetByIdAsync(task.Id);
                        inspecting.InspectorId = task.InspectorId;
                        inspecting.Status = task.Status;

                        await _unitOfWork.InspectingFormRepository.UpdateAsync(inspecting);
                    }
                }

                if (model.AssignPackagingTasks.Any() && model.AssignPackagingTasks.Count > 0)
                {
                    foreach (var task in model.AssignPackagingTasks)
                    {
                        var packaging = await _unitOfWork.PackagingTaskRepository.GetByIdAsync(task.Id);
                        packaging.FarmerId = task.FarmerId;
                        packaging.Status = task.Status;

                        await _unitOfWork.PackagingTaskRepository.UpdateAsync(packaging);
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

                plan.Status = "Pending";
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

                var yield = await _unitOfWork.YieldRepository.GetByIdAsync(model.YieldId);
                if (yield == null)
                {
                    return new BusinessResult(404, "Not found any yield!");
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

                return new BusinessResult(200, "Create plan successfully!", rs);
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }
    }
}
