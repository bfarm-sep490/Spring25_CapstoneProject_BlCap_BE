using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class PackagingItemRepository : GenericRepository<PackagingItem>
    {
        public PackagingItemRepository() { }
        public PackagingItemRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<PackagingItem>> GetPackagingItemByPlanId(int planId)
        {
            return await _context.PackagingItems
                .Include(pi => pi.Item)
                .Include(pi => pi.PackagingTask)
                    .ThenInclude(pi => pi.Plan)
                .Where(pi => pi.PackagingTask.PlanId == planId)
                .ToListAsync();
        }
    }
}
