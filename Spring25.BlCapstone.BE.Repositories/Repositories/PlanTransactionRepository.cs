using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class PlanTransactionRepository : GenericRepository<PlanTransaction>
    {
        public PlanTransactionRepository()
        {
            _context ??= new Context();
        }
        public PlanTransactionRepository(Context context)
        {
            _context = context;
        }

        public async Task<PlanTransaction> GetPlanTransactionByTaskId(int? caringTaskId = null, int? packagingTaskId = null, int? harvestingTaskId = null, int? inspectingFormId = null)
        {
            var query = _context.PlanTransactions
                                .Include(pt => pt.Plan)
                                    .ThenInclude(pt => pt.CaringTasks)
                                .Include(pt => pt.Plan)
                                    .ThenInclude(pt => pt.PackagingTasks)
                                .Include(pt => pt.Plan)
                                    .ThenInclude(pt => pt.HarvestingTasks)
                                .Include(pt => pt.Plan)
                                    .ThenInclude(pt => pt.InspectingForms)
                                .AsQueryable();

            if (caringTaskId.HasValue)
            {
                return await query.FirstOrDefaultAsync(pt => pt.Plan.CaringTasks.Any(t => t.Id == caringTaskId));
            }
            
            if (packagingTaskId.HasValue)
            {
                return await query.FirstOrDefaultAsync(pt => pt.Plan.PackagingTasks.Any(t => t.Id == packagingTaskId));
            }
            
            if (harvestingTaskId.HasValue)
            {
                return await query.FirstOrDefaultAsync(pt => pt.Plan.HarvestingTasks.Any(t => t.Id == harvestingTaskId));
            }
            
            if (inspectingFormId.HasValue)
            {
                return await query.FirstOrDefaultAsync(pt => pt.Plan.InspectingForms.Any(t => t.Id == inspectingFormId));
            }

            return null;
        }
    }
}
