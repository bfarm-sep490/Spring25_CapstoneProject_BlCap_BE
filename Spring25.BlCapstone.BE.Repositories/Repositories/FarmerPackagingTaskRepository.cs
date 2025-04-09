using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class FarmerPackagingTaskRepository : GenericRepository<FarmerPackagingTask>
    {
        public FarmerPackagingTaskRepository() { }
        public FarmerPackagingTaskRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<FarmerPackagingTask>> GetFarmerPackagingTasksByPlanId(int planId)
        {
            return await _context.FarmerPackagingTasks
                                 .Include(fct => fct.PackagingTask)
                                 .Where(fp => fp.PackagingTask.PlanId == planId && fp.Status.ToLower().Trim().Equals("active"))
                                 .ToListAsync();
        }

        public async Task<List<FarmerPackagingTask>> GetFarmerPackagingTasks(int? taskId = null)
        {
            var query = _context.FarmerPackagingTasks
                                .Include(h => h.Farmer)
                                    .ThenInclude(h => h.Account)
                                .AsQueryable();

            if (taskId.HasValue)
            {
                query = query.Where(i => i.TaskId == taskId);
            }

            return await query.ToListAsync();
        }

        public async Task<bool> CheckFarmerAssignInPlan(int planId, int farmerId)
        {
            return await _context.FarmerPackagingTasks
                                 .AnyAsync(fct => fct.Status.ToLower() == "active"
                                             && _context.PackagingTasks
                                                        .Where(ct => ct.PlanId == planId)
                                                        .Any(ct => ct.Id == fct.TaskId)
                                             && fct.FarmerId == farmerId);
        }
    }
}
