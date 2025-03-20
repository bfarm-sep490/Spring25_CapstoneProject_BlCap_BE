using Microsoft.EntityFrameworkCore;
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
                                 .Where(fp => fp.HarvestingTask.PlanId == planId && fp.Status.ToLower().Trim().Equals("ongoing"))
                                 .ToListAsync();
        }

        public async Task<List<FarmerHarvestingTask>> GetFarmerHarvestingTasks(int? taskId = null)
        {
            var query = _context.FarmerHarvestingTasks
                                .AsQueryable();

            if (taskId.HasValue)
            {
                query = query.Where(i => i.TaskId == taskId);
            }

            return await query.ToListAsync();
        }

        public async Task<bool> CheckFarmerAssignInPlan(int planId, int farmerId)
        {
            return await _context.FarmerHarvestingTasks
                                 .AnyAsync(fct => fct.Status.ToLower() == "active"
                                             && _context.HarvestingTasks
                                                        .Where(ct => ct.PlanId == planId)
                                                        .Any(ct => ct.Id == fct.TaskId)
                                             && fct.FarmerId == farmerId);
        }
    }
}
