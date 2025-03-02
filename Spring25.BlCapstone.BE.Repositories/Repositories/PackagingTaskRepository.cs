using Microsoft.EntityFrameworkCore;
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

        public async Task<List<PackagingTask>> GetPackagingTask(int? planId)
        {
            if (planId == null)
            {
                return await _context.PackagingTasks
                    .Include(x => x.PackagingImages)
                    .Include(x => x.PackagingItems)
                    .Include(x => x.Farmer)
                        .ThenInclude(x => x.Account)
                    .ToListAsync();
            } 
            else
            {
                return await _context.PackagingTasks
                    .Where(pt => pt.PlanId == planId)
                    .Include(x => x.PackagingImages)
                    .Include(x => x.PackagingItems)
                    .Include(x => x.Farmer)
                        .ThenInclude(x => x.Account)
                    .ToListAsync();
            }
        }
    }
}
