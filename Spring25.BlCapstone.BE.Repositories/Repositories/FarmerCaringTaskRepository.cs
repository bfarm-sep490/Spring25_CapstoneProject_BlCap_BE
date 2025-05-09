﻿using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class FarmerCaringTaskRepository : GenericRepository<FarmerCaringTask>
    {
        public FarmerCaringTaskRepository() { }
        public FarmerCaringTaskRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<FarmerCaringTask>> GetFarmerCaringTasksByPlanId(int planId)
        {
            return await _context.FarmerCaringTasks
                                 .Include(fct => fct.CaringTask)
                                 .Where(fp => fp.CaringTask.PlanId == planId && fp.Status.ToLower().Trim().Equals("active"))
                                 .ToListAsync();
        }

        public async Task<List<FarmerCaringTask>> GetFarmerCaringTasks(int? taskId = null)
        {
            var query = _context.FarmerCaringTasks
                                .Include(f => f.Farmer)
                                    .ThenInclude(f => f.Account)
                                .AsQueryable();

            if (taskId.HasValue)
            {
                query = query.Where(i => i.TaskId == taskId);
            }

            return await query.ToListAsync();
        }

        public async Task<List<FarmerCaringTask>> CheckFarmersAssignInPlan(int planId, int farmerId)
        {
            return await _context.FarmerCaringTasks
                                 .Where(fct => fct.Status.ToLower() == "active"
                                             && _context.CaringTasks
                                                        .Where(ct => ct.PlanId == planId)
                                                        .Any(ct => ct.Id == fct.TaskId) 
                                             && fct.FarmerId == farmerId)
                                 .ToListAsync();
        }

        public async Task<List<FarmerCaringTask>> GetAllFarmersCaringTaskByProblemId(int problemId)
        {
            return await _context.FarmerCaringTasks
                                 .Include(p => p.CaringTask)
                                 .Where(p => p.CaringTask.ProblemId == problemId && p.Status == "Active")
                                 .ToListAsync();
        }
    }
}
