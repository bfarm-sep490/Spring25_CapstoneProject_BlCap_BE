﻿using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Dashboards;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class HarvestingTaskRepository : GenericRepository<HarvestingTask>
    {
        public HarvestingTaskRepository() { }
        public HarvestingTaskRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<AdminData>> GetDashboardHarvestingTasks()
        {
            var data = await _context.HarvestingTasks.Where(x => x.CompleteDate.HasValue).ToListAsync();
            
            var result = data.GroupBy(x => x.CompleteDate.Value.Date).OrderBy(x => x.Key)
                .Select(g => new AdminData
                {
                    Date = g.Key,
                    Value = g.Count()
                })
                .ToList();
            return result;
        }

        public async Task<List<HarvestingTask>> GetHarvestingTasks(int? planId = null, int? farmerId = null, int? taskId = null, List<string>? status = null, int? pageNumber = null, int? pageSize = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.HarvestingTasks
                                .Include(x => x.HarvestingImages)
                                .Include(x => x.HarvestingItems)
                                    .ThenInclude(x => x.Item)
                                .Include(x => x.FarmerHarvestingTasks)
                                    .ThenInclude(x => x.Farmer)
                                    .ThenInclude(x => x.Account)
                                .Include(x => x.Plan)
                                .AsQueryable();

            if (planId.HasValue)
            {
                query = query.Where(ht => ht.PlanId == planId);
            }

            if (farmerId.HasValue)
            {
                query = query.Where(ct => ct.FarmerHarvestingTasks.Any(c => c.FarmerId == farmerId));
            }

            if (taskId.HasValue)
            {
                query = query.Where(ct => ct.Id == taskId);
            }

            if (status?.Any() == true)
            {
                var normalizedStatus = status.Select(s => s.ToLower().Trim()).ToList();
                query = query.Where(ct => normalizedStatus.Contains(ct.Status.ToLower().Trim()));
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(ht =>
                    ht.StartDate <= endDate && ht.EndDate >= startDate);
            }
            else if (startDate.HasValue)
            {
                query = query.Where(ht => ht.EndDate >= startDate);
            }
            else if (endDate.HasValue)
            {
                query = query.Where(ht => ht.StartDate <= endDate);
            }

            if (pageNumber.HasValue && pageSize.HasValue && pageNumber > 0 && pageSize > 0)
            {
                int skip = (pageNumber.Value - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<AdminData>> GetDashboardHarvestingTasksByPlanId(int id)
        {
            var data = await _context.HarvestingTasks.Where(x => x.CompleteDate.HasValue && x.PlanId==id).ToListAsync();

            var result = data.GroupBy(x => x.CompleteDate.Value.Date).OrderBy(x => x.Key)
                .Select(g => new AdminData
                {
                    Date = g.Key,
                    Value = g.Count()
                })
                .ToList();
            return result;
        }     

        public async Task<HavestedTask> GetHavestedTaskDashboardByPlanId(int planId)
        {
            var data = await _context.HarvestingTasks.Where(x=>x.PlanId==planId && x.Status.ToLower() == "completed").ToListAsync();
            //var kg = data.Where(x => x.HarvestedUnit.ToLower() == "kg").Sum(x => x.HarvestedQuantity.Value);
            //var tan = data.Where(x => x.HarvestedUnit.ToLower() == "tấn").Sum(x => x.HarvestedQuantity.Value);
            //return new HavestedTask { HavestedValue = (tan + kg / 1000) , Unit= "tấn" };
            return null;
        }

        public async Task<TasksDashboard> GetHarvestingTasksStatusDashboardByPlanId(int planId)
        {
            var data = await _context.HarvestingTasks.Where(x => x.PlanId == planId).ToListAsync();
            var result = new TasksDashboard();
            result.TotalTasks = data.Count();
            result.IncompleteTasks = data.Where(x => x.Status.ToLower() == "incomplete").Count();
            result.CompleteTasks = data.Where(x => x.Status.ToLower() == "complete").Count();
            result.CancelTasks = data.Where(x => x.Status.ToLower() == "cancel").Count();
            result.OnGoingTasks = data.Where(x => x.Status.ToLower() == "ongoing").Count();
            result.PendingTasks = data.Where(x => x.Status.ToLower() == "pending").Count();
            return result;
        }

        public async Task<List<HarvestingTask>> GetExpiredHarvestingTasks()
        {
            return await _context.HarvestingTasks
                                 .Include(ct => ct.Plan)
                                 .Where(ct => ct.Plan.Status.ToLower() == "ongoing" && ct.EndDate < DateTime.Now
                                        && ct.Status.ToLower() == "ongoing")
                                 .ToListAsync();
        }

        public async Task<List<HarvestingTask>> GetHarvestingCalander(int farmerId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.FarmerHarvestingTasks
                                 .Include(ct => ct.HarvestingTask)
                                 .Where(ct => ct.FarmerId == farmerId
                                        && ct.Status.ToLower().Trim().Equals("active"))
                                 .AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(ct => ct.HarvestingTask.StartDate >= startDate);
            }

            if (endDate.HasValue)
            {
                query = query.Where(ct => ct.HarvestingTask.EndDate <= endDate);
            }

            return await query.Select(ct => ct.HarvestingTask).ToListAsync();
        }

        public async Task<StatusTask> GetStatusTaskHarvestingByPlanId(int planId)
        {
            return await _context.HarvestingTasks.Where(x => x.PlanId == planId)
                .GroupBy(x => x.PlanId)
                .Select(g => new StatusTask
                {
                    CancelQuantity = g.Count(x => x.Status.ToLower() == "cancel"),
                    CompleteQuantity = g.Count(x => x.Status.ToLower() == "complete"),
                    InCompleteQuantity = g.Count(x => x.Status.ToLower() == "incomplete"),
                    PendingQuantity = g.Count(x => x.Status.ToLower() == "pending"), 
                    OnGoingQuantity = g.Count(x => x.Status.ToLower() == "ongoing"),
                    LastCreateDate = g.Max(x => x.CreatedAt)
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<HarvestingTask>> GetHarvestingProductions(int? planId = null, int? packagingTaskId = null)
        {
            var query = _context.HarvestingTasks
                                .Include(x => x.Plan)
                                    .ThenInclude(x => x.Plant)
                                .Include(x => x.PackagingProducts)
                                    .ThenInclude(x => x.PackagingTask)
                                .Include(x => x.Plan)
                                    .ThenInclude(x => x.InspectingForms)
                                        .ThenInclude(x => x.InspectingResult)
                                .Where(x => x.Status.ToLower().Trim().Equals("complete"))
                                .AsQueryable();

            if (planId.HasValue)
            {
                query = query.Where(p => p.PlanId == planId);
            }

            if (packagingTaskId.HasValue)
            {
                query = query.Where(p => p.PackagingProducts.Any(pp => pp.PackagingTaskId == packagingTaskId));
            }

            return await query.ToListAsync();
        }

        public async Task<HarvestingTask> GetHarvestingProduction(int id)
        {
            return await _context.HarvestingTasks
                                .Include(x => x.Plan)
                                    .ThenInclude(x => x.Plant)
                                .Include(x => x.PackagingProducts)
                                    .ThenInclude(x => x.PackagingTask)
                                .Include(x => x.Plan)
                                    .ThenInclude(x => x.InspectingForms)
                                        .ThenInclude(x => x.InspectingResult)
                                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<HarvestingTask> GetHarvestingTaskById(int id)
        {
            var rs = await _context.HarvestingTasks
                                 .Include(ct => ct.FarmerHarvestingTasks)
                                        .ThenInclude(ct => ct.Farmer)
                                            .ThenInclude(ct => ct.Account)
                                 .Include(ct => ct.HarvestingItems)
                                        .ThenInclude(ct => ct.Item)
                                 .FirstOrDefaultAsync(ct => ct.Id == id);
            return rs;
        }
    }
}
