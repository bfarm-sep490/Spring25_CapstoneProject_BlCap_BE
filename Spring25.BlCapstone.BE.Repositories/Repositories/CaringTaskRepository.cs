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
    public class CaringTaskRepository : GenericRepository<CaringTask>
    {
        public CaringTaskRepository() { }
        public CaringTaskRepository(Context context)
        {
            _context = context;
        }
        public async Task<CaringTask> GetDetail(int id)
        {
            return await _context.CaringTasks
                .Where(x => x.Id == id)
                .Include(x => x.CaringItems)
                .Include(x => x.CaringFertilizers)
                .Include(x => x.CaringPesticides)
                .Include(x => x.CaringImages)
                .FirstOrDefaultAsync();
        }

        public async Task<List<CaringTask>> GetAllCaringTasks(int? planId = null, int? farmerId = null, int? taskId = null)
        {
            var query = _context.CaringTasks
                                      .Include(x => x.CaringItems)
                                            .ThenInclude(x => x.Item)
                                      .Include(x => x.CaringFertilizers)
                                            .ThenInclude(x => x.Fertilizer)
                                      .Include(x => x.CaringPesticides)
                                            .ThenInclude(x => x.Pesticide)
                                      .Include(x => x.CaringImages)
                                      .Include(x => x.FarmerCaringTasks)
                                            .ThenInclude(x => x.Farmer)
                                            .ThenInclude(x => x.Account)
                                      .Include(x => x.Problem)
                                      .Include(x => x.Plan)
                                      .AsQueryable();  

            if (planId.HasValue)
            {
                query = query.Where(ct => ct.PlanId == planId);
            }

            if (farmerId.HasValue)
            {
                query = query.Where(ct => ct.FarmerCaringTasks.Any(c => c.FarmerId == farmerId));
            }

            if (taskId.HasValue)
            {
                query = query.Where(ct => ct.Id == taskId);
            }

            return await query.ToListAsync();
        }
        
        public async Task<List<AdminData>> GetDashboardCaringTasks()
        {
            var data = await _context.CaringTasks.Where(x => x.CompleteDate.HasValue).ToListAsync();

            var result = data.GroupBy(x => x.CompleteDate.Value.Date).OrderBy(x => x.Key)
                .Select(g => new AdminData
                {
                    Date = g.Key,
                    Value = g.Count()
                })
                .ToList();
            return result;
        }

        public async Task<List<AdminData>> GetDashboardCaringTasksByPlanId(int id)
        {
            var data = await _context.CaringTasks.Where(x => x.CompleteDate.HasValue && x.PlanId==id).ToListAsync();

            var result = data.GroupBy(x => x.CompleteDate.Value.Date).OrderBy(x => x.Key)
                .Select(g => new AdminData
                {
                    Date = g.Key,
                    Value = g.Count()
                })
                .ToList();
            return result;
        }
        public async Task<TasksDashboard> GetTasksStatusDashboardByPlanId(int planId)
        {
            var data = await _context.CaringTasks.Where(x => x.PlanId == planId).ToListAsync();
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
