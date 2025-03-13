using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class PlanRepository : GenericRepository<Plan>
    {
        public PlanRepository() { }
        public PlanRepository(Context context)
        {
            _context = context;
        }

        public async Task<Plan> GetPlan(int id)
        {
            return await _context.Plans
                .Include(p => p.Plant)        
                .Include(p => p.CaringTasks)
                .Include(p => p.HarvestingTasks)
                .Include(p => p.InspectingForms)
                .Include(p => p.PackagingTasks)
                .Include(p => p.Problems)
                .Include(p => p.Yield)
                .Include(p => p.Expert)
                    .ThenInclude(p => p.Account)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Plan>> GetAllPlans(int? expertId = null, string? status = null)
        {
            var query = _context.Plans
                                .Include(p => p.Expert)
                                    .ThenInclude(e => e.Account)
                                .Include(p => p.Plant)
                                .Include(p => p.Yield)
                                .AsQueryable();

            if (expertId != null)
            {
                query = query.Where(p => p.ExpertId == expertId);
            }

            if (status != null)
            {
                query = query.Where(p => p.Status.ToLower() == status.ToLower());
            }

            return await query.ToListAsync();
        }
    }
}
