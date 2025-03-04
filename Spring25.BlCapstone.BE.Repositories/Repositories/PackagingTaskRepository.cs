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

        public async Task<List<PackagingTask>> GetPackagingTasks(int? planId = null, int? farmerId = null)
        {
            var query = _context.PackagingTasks
                                .Include(x => x.PackagingImages)
                                .Include(x => x.PackagingItems)
                                .Include(x => x.Farmer)
                                    .ThenInclude(x => x.Account)
                                .AsQueryable();

            if (planId.HasValue)
            {
                query = query.Where(pt => pt.PlanId == planId);
            } 

            if (farmerId.HasValue) 
            {
                query = query.Where(pt => pt.FarmerId == farmerId);
            }

            return await query.ToListAsync();
        }
    }
}
