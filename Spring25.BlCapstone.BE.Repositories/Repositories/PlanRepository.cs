﻿using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class PlanRepository : GenericRepository<Plan>
    {
        public PlanRepository() { }
        public PlanRepository(Context context)
        {
            _context = context;
        }

        public async Task<Plan> GetPlan(int id)
        {
            return await _context.Plans
                .Include(p => p.Plant)
                .Include(p => p.CaringTasks)
                .Include(p => p.HarvestingTasks)
                .Include(p => p.InspectingForms)
                    .ThenInclude(p => p.InspectingResult)
                .Include(p => p.PackagingTasks)
                .Include(p => p.Problems)
                .Include(p => p.Yield)
                .Include(p => p.Expert)
                    .ThenInclude(p => p.Account)
                .Include(p => p.OrderPlans)
                    .ThenInclude(p => p.Order)
                        .ThenInclude(p => p.Retailer)
                            .ThenInclude(p => p.Account)
                .Include(p => p.OrderPlans)
                    .ThenInclude(p => p.Order)
                        .ThenInclude(p => p.PackagingType)
                .Include(p => p.PlanTransaction)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Plan>> GetAllPlans(int? expertId = null, string? status = null, int? orderId = null)
        {
            var query = _context.Plans
                                .Include(p => p.Expert)
                                    .ThenInclude(e => e.Account)
                                .Include(p => p.Plant)
                                .Include(p => p.Yield)
                                .Include(p => p.PlanTransaction)
                                .Include(p => p.OrderPlans)
                                    .ThenInclude(p => p.Order)
                                .Include(p => p.InspectingForms)
                                    .ThenInclude(p => p.InspectingResult)
                                .AsQueryable();

            if (expertId != null)
            {
                query = query.Where(p => p.ExpertId == expertId);
            }

            if (status != null)
            {
                query = query.Where(p => p.Status.ToLower() == status.ToLower());
            }

            if (orderId.HasValue)
            {
                query = query.Where(p => p.OrderPlans.Any(p => p.OrderId == orderId));
            }

            return await query.ToListAsync();
        }

        public async Task<List<Plan>> GetPlanNotHaveOrder()
        {
            return await _context.Plans
                .Where(x => !x.OrderPlans.Any())
                .ToListAsync();
        }

        public async Task<List<Plan>> GetSuggestPlansByPlanId(int plantId, int planId, float estimatedProduct)
        {
            var result = await _context.Plans.Where(x => x.PlantId == plantId && x.Id != planId && x.Status.ToLower() == "complete")
                 .OrderBy(x => Math.Abs(x.EstimatedProduct.Value - estimatedProduct))
                 .ToListAsync();
            return result;
        }

        public async Task<Plan> GetTasksByPlanId(int planid)
        {
            var result = await _context.Plans.Where(x => x.Id == planid)
                .Include(x => x.HarvestingTasks).ThenInclude(x => x.HarvestingItems)
                .Include(x => x.CaringTasks)
                    .ThenInclude(x => x.CaringItems)
                .Include(x => x.CaringTasks)
                    .ThenInclude(x => x.CaringFertilizers)
                .Include(x => x.CaringTasks)
                    .ThenInclude(x => x.CaringPesticides)
                .Include(x => x.HarvestingTasks)
                    .ThenInclude(x => x.HarvestingItems)
                .Include(x => x.InspectingForms)
                .Include(x => x.PackagingTasks)
                    .ThenInclude(x=>x.PackagingItems)
                .FirstOrDefaultAsync();
            return result;
        }
    }
}