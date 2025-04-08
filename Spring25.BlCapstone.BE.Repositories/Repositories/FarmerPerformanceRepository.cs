using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class FarmerPerformanceRepository : GenericRepository<FarmerPerformance>
    {
        public FarmerPerformanceRepository() { }
        public FarmerPerformanceRepository(Context context)
        {
            _context = context;
        }

        public async Task<FarmerPerformance> GetFarmerByTaskId(int? caringTaskId = null, int? harvestingTaskId = null, int? packagingTaskId = null)
        {
            var query = _context.FarmerPerformances
                                .Include(f => f.Farmer)
                                    .ThenInclude(f => f.FarmerCaringTasks)
                                .Include(f => f.Farmer)
                                    .ThenInclude(f => f.FarmerHarvestingTasks)
                                .Include(f => f.Farmer)
                                    .ThenInclude(f => f.FarmerPackagingTasks)
                                .AsQueryable();

            if (caringTaskId.HasValue)
            {
                return await query.FirstOrDefaultAsync(fp =>
                    fp.Farmer.FarmerCaringTasks.Any(t =>
                        t.TaskId == caringTaskId && t.Status.ToLower().Trim() == "active"));
            }

            if (harvestingTaskId.HasValue)
            {
                return await query.FirstOrDefaultAsync(fp =>
                    fp.Farmer.FarmerHarvestingTasks.Any(t =>
                        t.TaskId == harvestingTaskId && t.Status.ToLower().Trim() == "active"));
            }

            if (packagingTaskId.HasValue)
            {
                return await query.FirstOrDefaultAsync(fp =>
                    fp.Farmer.FarmerPackagingTasks.Any(t =>
                        t.TaskId == packagingTaskId && t.Status.ToLower().Trim() == "active"));
            }

            return null;
        }
    }
}
