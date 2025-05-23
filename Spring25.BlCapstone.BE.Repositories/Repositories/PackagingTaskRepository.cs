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
    public class PackagingTaskRepository : GenericRepository<PackagingTask>
    {
        public PackagingTaskRepository() { }
        public PackagingTaskRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<PackagingTask>> GetPackagingTasks(int? planId = null, int? farmerId = null, int? taskId = null, List<string>? status = null, int? pageNumber = null, int? pageSize = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.PackagingTasks
                                .Include(x => x.PackagingImages)
                                .Include(x => x.PackagingItems)
                                    .ThenInclude(x => x.Item)
                                .Include(x => x.FarmerPackagingTasks)
                                    .ThenInclude(x => x.Farmer)
                                        .ThenInclude(x => x.Account)
                                .Include(x => x.Plan)
                                .Include(x => x.Order)
                                    .ThenInclude(x => x.Retailer)
                                        .ThenInclude(x => x.Account)
                                .AsQueryable();

            if (planId.HasValue)
            {
                query = query.Where(pt => pt.PlanId == planId);
            }

            if (farmerId.HasValue)
            {
                query = query.Where(ct => ct.FarmerPackagingTasks.Any(c => c.FarmerId == farmerId));
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

        public async Task<TasksDashboard> GetPackagingTasksStatusDashboardByPlanId(int planId)
        {
            var data = await _context.PackagingTasks.Where(x => x.PlanId == planId).ToListAsync();
            var result = new TasksDashboard();
            result.TotalTasks = data.Count();
            result.IncompleteTasks = data.Where(x => x.Status.ToLower() == "incomplete").Count();
            result.CompleteTasks = data.Where(x => x.Status.ToLower() == "complete").Count();
            result.CancelTasks = data.Where(x => x.Status.ToLower() == "cancel").Count();
            result.OnGoingTasks = data.Where(x => x.Status.ToLower() == "ongoing").Count();
            result.PendingTasks = data.Where(x => x.Status.ToLower() == "pending").Count();
            return result;
        }

        public async Task<List<PackagingTask>> GetExpiredPackagingTasks()
        {
            return await _context.PackagingTasks
                                 .Include(ct => ct.Plan)
                                 .Where(ct => ct.Plan.Status.ToLower() == "ongoing" && ct.EndDate < DateTime.Now
                                        && ct.Status.ToLower() == "ongoing")
                                 .ToListAsync();
        }


        public async Task<List<PackagingTask>> GetPackagingCalander(int farmerId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.FarmerPackagingTasks
                                 .Include(ct => ct.PackagingTask)
                                 .Where(ct => ct.FarmerId == farmerId
                                        && ct.Status.ToLower().Trim().Equals("active"))
                                 .AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(ct => ct.PackagingTask.StartDate >= startDate);
            }

            if (endDate.HasValue)
            {
                query = query.Where(ct => ct.PackagingTask.EndDate <= endDate);
            }

            return await query.Select(ct => ct.PackagingTask).ToListAsync();
        }

        public async Task<StatusTask> GetStatusTaskPackagingByPlanId(int planId)
        {
            return await _context.PackagingTasks.Where(x => x.PlanId == planId)
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

        public async Task<PackagingTask> GetPackagingTaskById(int id)
        {
            var rs = await _context.PackagingTasks
                                 .Include(ct => ct.FarmerPackagingTasks)
                                        .ThenInclude(ct => ct.Farmer)
                                            .ThenInclude(ct => ct.Account)
                                 .Include(ct => ct.PackagingItems)
                                        .ThenInclude(ct => ct.Item)
                                 .FirstOrDefaultAsync(ct => ct.Id == id);
            return rs;
        }

        public async Task<bool> ArePackagingTasksEnoughForAllOrderIds(int planId, List<int> orderIds)
        {
            var existingOrderIds = await _context.PackagingTasks
                .Where(pt => pt.PlanId == planId && pt.OrderId.HasValue && orderIds.Contains(pt.OrderId.Value))
                .Select(pt => pt.OrderId.Value)
                .Distinct()
                .ToListAsync();

            return existingOrderIds.Count == orderIds.Count;
        }

        public async Task<bool> AreAllPreOrdersRequiredHasPackagedProduct(int planId)
        {
            var orderPlans = await _context.OrderPlans
                .Where(op => op.PlanId == planId)
                .ToListAsync();

            if (!orderPlans.Any()) return true;

            var orderPlanQuantities = orderPlans.ToDictionary(op => op.OrderId, op => op.Quantity);

            var orderIds = orderPlanQuantities.Keys.ToList();

            var packagingTasks = await _context.PackagingTasks
                .Where(pt => pt.PlanId == planId && pt.OrderId.HasValue && orderIds.Contains(pt.OrderId.Value))
                .ToListAsync();

            var packagedQuantities = packagingTasks
                .GroupBy(pt => pt.OrderId.Value)
                .ToDictionary(g => g.Key, g => g.Sum(pt => pt.TotalPackagedWeight ?? 0));

            foreach (var order in orderPlanQuantities)
            {
                var orderId = order.Key;
                var requiredQuantity = order.Value;
                var packagedQuantity = packagedQuantities.ContainsKey(orderId) ? packagedQuantities[orderId] : 0;

                if (packagedQuantity < requiredQuantity)
                {
                    return false; 
                }
            }

            return true;
        }
    }
}
