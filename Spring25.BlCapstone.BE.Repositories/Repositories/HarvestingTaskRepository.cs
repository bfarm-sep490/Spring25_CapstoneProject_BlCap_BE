using Microsoft.EntityFrameworkCore;
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
    }
}
