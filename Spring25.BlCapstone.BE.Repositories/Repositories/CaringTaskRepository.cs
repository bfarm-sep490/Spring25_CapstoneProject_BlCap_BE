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

        public async Task<List<CaringTask>> GetAllCaringTasks(int? planId = null, int? farmerId = null, int? taskId = null, int? problemId = null, List<string>? status = null)
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

            if (problemId.HasValue)
            {
                query = query.Where(ct => ct.ProblemId == problemId);
            }

            if (status?.Any() == true)
            {
                var normalizedStatus = status.Select(s => s.ToLower().Trim()).ToList();
                query = query.Where(ct => normalizedStatus.Contains(ct.Status.ToLower().Trim()));
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

        public async Task<List<CaringTask>> GetExpiredCaringTasks()
        {
            return await _context.CaringTasks
                                 .Include(ct => ct.Plan)
                                 .Where(ct => ct.Plan.Status.ToLower() == "ongoing" && ct.EndDate < DateTime.Now
                                        && ct.Status.ToLower() == "ongoing")
                                 .ToListAsync();
        }

        public async Task<List<CaringTask>> GetCaringCalander(int farmerId, DateTime? startDate = null, DateTime? endDate= null)
        {
            var query = _context.FarmerCaringTasks
                                 .Include(ct => ct.CaringTask)
                                 .Where(ct => ct.FarmerId == farmerId
                                        && ct.Status.ToLower().Trim().Equals("active"))
                                 .AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(ct => ct.CaringTask.StartDate >= startDate);
            }
            
            if (endDate.HasValue)
            {
                query = query.Where(ct => ct.CaringTask.EndDate <= endDate);
            }

            return await query.Select(ct => ct.CaringTask).ToListAsync();
        }

        public async Task<StatusTask> GetStatusTaskCaringByPlanId(int planId)
        {
            return await _context.CaringTasks.Where(x => x.PlanId == planId)
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
        
        public async Task<List<CaringType>> GetTypeTasksStatus(int id)
        {
            var tasks = await _context.CaringTasks
                .Where(x => x.PlanId == id)
                .GroupBy(x => x.TaskType)
                .Select(g => new CaringType
                {
                    Type = g.Key,
                    OnGoingQuantity = g.Count(x => x.Status.ToLower() == "ongoing"),
                    PendingQuantity = g.Count(x => x.Status.ToLower() == "pending"),
                    InCompleteQuantity = g.Count(x => x.Status.ToLower() == "incomplete"),
                    CancelQuantity = g.Count(x => x.Status.ToLower() == "cancel"),
                    CompleteQuantity = g.Count(x => x.Status.ToLower() == "complete")
                })
                .ToListAsync();
            return tasks;
        }

        public async Task<List<CaringTask>> GetAllCaringTasksByProblemId(int problemId)
        {
            return await _context.CaringTasks
                                 .Include(ct => ct.Problem)
                                 .Where(ct => ct.ProblemId == problemId)
                                 .ToListAsync();
        }

        public async Task<CaringTask> GetCaringTaskById(int id)
        {
            var rs = await _context.CaringTasks
                                 .Include(ct => ct.FarmerCaringTasks)
                                        .ThenInclude(ct => ct.Farmer)
                                            .ThenInclude(ct => ct.Account)
                                 .Include(ct => ct.CaringFertilizers)
                                        .ThenInclude(ct => ct.Fertilizer)
                                 .Include(ct => ct.CaringPesticides)
                                        .ThenInclude(ct => ct.Pesticide)
                                 .Include(ct => ct.CaringItems)
                                        .ThenInclude(ct => ct.Item)
                                 .FirstOrDefaultAsync(ct => ct.Id == id);
            return rs;
        }
    }
}
