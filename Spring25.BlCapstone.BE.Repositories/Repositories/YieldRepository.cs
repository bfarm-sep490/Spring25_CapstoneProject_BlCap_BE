using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class YieldRepository:GenericRepository<Yield>
    {
        public YieldRepository() 
        {
            _context ??= new Context();
        }
        public YieldRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Yield>> GetYields(string? status = null)
        {
            var query = _context.Yields.AsQueryable();

            if (status != null)
            {
                query = query.Where(x => x.Status == status);
            }

            return await query.ToListAsync();
        }
        public async Task<List<Plant>> GetSuggestPlantsbyYieldId(int id)
        {
                return await _context.PlantYields.Where(x => x.YieldId == id)
                .Include(x => x.Plant)
                .Select(x => x.Plant)
                .ToListAsync();
        }

        public async Task<List<Plan>> GetHistoryPlan(int yieldId)
        {
            return await _context.Plans
                                 .Include(y => y.Yield)
                                 .Include(y => y.Plant)
                                 .Where(y => y.YieldId == yieldId && !y.Status.ToLower().Trim().Equals("draft") && !y.Status.ToLower().Trim().Equals("pending"))
                                 .OrderBy(y => y.EndDate)
                                 .ToListAsync();
        }
    }
}
