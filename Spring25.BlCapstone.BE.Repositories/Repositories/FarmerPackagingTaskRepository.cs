using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class FarmerPackagingTaskRepository : GenericRepository<FarmerPackagingTask>
    {
        public FarmerPackagingTaskRepository() { }
        public FarmerPackagingTaskRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<FarmerPackagingTask>> GetFarmerPackagingTasksByPlanId(int planId)
        {
            return await _context.FarmerPackagingTasks
                                 .Include(fct => fct.PackagingTask)
                                 .Where(fp => fp.PackagingTask.PlanId == planId)
                                 .ToListAsync();
        }
    }
}
