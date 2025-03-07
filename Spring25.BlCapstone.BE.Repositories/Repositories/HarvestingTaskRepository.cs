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
    public class HarvestingTaskRepository : GenericRepository<HarvestingTask>
    {
        public HarvestingTaskRepository() { }
        public HarvestingTaskRepository(Context context)
        {
            _context = context;
        }
        public async Task<HarvestingTask> GetHarvestingTaskById(int id)
        {
           return await _context.HarvestingTasks.Where(x=>x.Id == id)
                .Include(x=>x.HarvestingImages)
                .Include(x => x.HarvestingItems)
                .Include(x => x.Farmer)
                .ThenInclude(x => x.Account)
                .FirstOrDefaultAsync();       
        }
        public async Task<List<AdminData>> GetDashboardHarvestingTasks()
        {
            var data = await _context.HarvestingTasks.Where(x => x.CompleteDate.HasValue).ToListAsync();
            
            var result = data.GroupBy(x => x.CompleteDate.Value.Date).OrderBy(x => x.Key)
                .Select(g => new AdminData
                {
                    Date = g.Key,
                    Value = g.Count()
                })
                .ToList();
            return result;
        }
        


        public async Task<List<HarvestingTask>> GetHarvestingTasks(int? planId = null, int? farmerId = null)
        {
            var query = _context.HarvestingTasks
                                .Include(x => x.HarvestingImages)
                                .Include(x => x.HarvestingItems)
                                .Include(x => x.Farmer)
                                    .ThenInclude(x => x.Account)
                                .AsQueryable();
            if (planId.HasValue)
            {
                query = query.Where(ht => ht.PlanId == planId);
            } 

            if (farmerId.HasValue)
            {
                query = query.Where(ht => ht.FarmerId == farmerId);
            }

            return await query.ToListAsync();
        }
        public async Task<List<AdminData>> GetDashboardHarvestingTasksByPlanId(int id)
        {
            var data = await _context.HarvestingTasks.Where(x => x.CompleteDate.HasValue && x.PlanId==id).ToListAsync();

            var result = data.GroupBy(x => x.CompleteDate.Value.Date).OrderBy(x => x.Key)
                .Select(g => new AdminData
                {
                    Date = g.Key,
                    Value = g.Count()
                })
                .ToList();
            return result;
        }     
        public async Task<HavestedTask> GetHavestedTaskDashboardByPlanId(int planId)
        {
            var data = await _context.HarvestingTasks.Where(x=>x.PlanId==planId && x.Status.ToLower() == "completed").ToListAsync();
            var kg = data.Where(x => x.HarvestedUnit.ToLower() == "kg").Sum(x => x.HarvestedQuantity.Value);
            var tan = data.Where(x => x.HarvestedUnit.ToLower() == "tấn").Sum(x => x.HarvestedQuantity.Value);
            return new HavestedTask { HavestedValue = (tan + kg / 1000) , Unit= "tấn" };
        }
    }
}
