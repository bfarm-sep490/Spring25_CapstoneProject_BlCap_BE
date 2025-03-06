using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class HarvestingItemRepository : GenericRepository<HarvestingItem>
    {
        public HarvestingItemRepository() { }
        public HarvestingItemRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<HarvestingItem>> GetHarvestingItemByPlanId(int planId)
        {
            return await _context.HarvestingItems
                .Include(ci => ci.Item)
                .Include(ci => ci.HarvestingTask)
                    .ThenInclude(ci => ci.Plan)
                .Where(ci => ci.HarvestingTask.PlanId == planId)
                .ToListAsync();
        }

        public async Task<List<HarvestingItem>> GetHarvestingItemsByTaskId(int taskId)
        {
            return await _context.HarvestingItems
                .Where(hi => hi.TaskId == taskId)
                .ToListAsync();
        }
    }
}
