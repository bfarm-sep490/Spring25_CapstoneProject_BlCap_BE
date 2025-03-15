using Microsoft.EntityFrameworkCore;
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

        public async Task<List<PackagingTask>> GetPackagingTasks(int? planId = null, int? farmerId = null)
        {
            var query = _context.PackagingTasks
                                .Include(x => x.PackagingImages)
                                .Include(x => x.PackagingItems)
                                //.Include(x => x.Farmer)
                                //    .ThenInclude(x => x.Account)
                                .AsQueryable();

            if (planId.HasValue)
            {
                query = query.Where(pt => pt.PlanId == planId);
            } 

            return await query.ToListAsync();
        }

        public async Task<PackagingTask> GetPackagingTaskById(int taskId)
        {
            return await _context.PackagingTasks
                                 .Include(x => x.PackagingImages)
                                 .Include(x => x.PackagingItems)
                                 //.Include(x => x.Farmer)
                                  //   .ThenInclude(x => x.Account)
                                 .FirstOrDefaultAsync(pt => pt.Id == taskId);
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
    }
}
