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
            var data = await _context.HarvestingTasks.GroupBy(x => x.HarvestDate.Value).OrderBy(x => x.Key)
                .Select(g => new AdminData
                {
                    Date = g.Key,
                    Value = g.Count()
                })
                .ToListAsync();
            return data;
        }
    }
}
