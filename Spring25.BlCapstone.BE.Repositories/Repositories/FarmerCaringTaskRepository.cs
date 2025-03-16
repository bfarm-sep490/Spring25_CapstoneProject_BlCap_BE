using Microsoft.EntityFrameworkCore;
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
                                 .Where(fp => fp.CaringTask.PlanId == planId)
                                 .ToListAsync();
        }

        public async Task<List<FarmerCaringTask>> GetFarmerCaringTasks(int? taskId = null)
        {
            var query = _context.FarmerCaringTasks
                                .AsQueryable();

            if (taskId.HasValue)
            {
                query = query.Where(i => i.TaskId == taskId);
            }

            return await query.ToListAsync();
        }
    }
}
