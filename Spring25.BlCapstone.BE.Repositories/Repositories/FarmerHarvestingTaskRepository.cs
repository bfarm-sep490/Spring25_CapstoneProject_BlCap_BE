﻿using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class FarmerHarvestingTaskRepository : GenericRepository<FarmerHarvestingTask>
    {
        public FarmerHarvestingTaskRepository() { }
        public FarmerHarvestingTaskRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<FarmerHarvestingTask>> GetFarmerHarvestingTasksByPlanId(int planId)
        {
            return await _context.FarmerHarvestingTasks
                                 .Include(fct => fct.HarvestingTask)
                                 .Where(fp => fp.HarvestingTask.PlanId == planId)
                                 .ToListAsync();
        }
    }
}
