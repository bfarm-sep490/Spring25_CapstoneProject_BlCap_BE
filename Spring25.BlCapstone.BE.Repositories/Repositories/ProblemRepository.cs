using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class ProblemRepository : GenericRepository<Problem>
    {
        public ProblemRepository() { }
        public ProblemRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Problem>> GetProblems(int? planId = null, int? farmerId = null, string? name = null, string? status = null)
        {
            var query = _context.Problems
                                .Include(p => p.Plan)
                                .Include(p => p.Farmer)
                                    .ThenInclude(f => f.Account)
                                .Include(p => p.ProblemImages)
                                .AsQueryable();

            if (planId.HasValue)
            {
                query = query.Where(p => p.PlanId == planId);
            }

            if (farmerId.HasValue)
            {
                query = query.Where(p => p.FarmerId == farmerId);
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.ProblemName.Contains(name));
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(p => p.Status.ToLower().Trim().Equals(status));
            }
            
            return await query.ToListAsync();
        }
        
        public async Task<Problem> GetProblem(int id)
        {
            return await _context.Problems
                .Include(p => p.ProblemImages)
                .Include(p => p.Plan)
                .Include(p => p.Farmer)
                    .ThenInclude(f => f.Account)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Problem>> GetProblemByPlanId(int planId)
        {
            return await _context.Problems
                .Where(p => p.PlanId == planId)
                .Include(p => p.ProblemImages)
                .Include(p => p.Farmer)
                    .ThenInclude(f => f.Account)
                .ToListAsync();
        }
    }
}
