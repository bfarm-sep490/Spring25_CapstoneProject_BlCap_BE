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

        public async Task<List<Problem>> GetProblems()
        {
            return await _context.Problems
                .Include(p => p.ProblemImages)
                .Include(p => p.Issues)
                .ToListAsync();
        }
        
        public async Task<Problem> GetProblem(int id)
        {
            return await _context.Problems
                .Include(p => p.ProblemImages)
                .Include(p => p.Issues)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Problem>> GetProblemByPlanId(int planId)
        {
            return await _context.Problems
                .Where(p => p.PlanId == planId)
                .Include(p => p.ProblemImages)
                .Include(p => p.Issues)
                .ToListAsync();
        }
    }
}
