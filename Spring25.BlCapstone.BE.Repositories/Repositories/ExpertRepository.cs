using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class ExpertRepository : GenericRepository<Expert>
    {
        public ExpertRepository() { }
        public ExpertRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Expert>> GetExperts(int? id = null)
        {
            var query = _context.Experts
                                .Include(f => f.Account)
                                .AsQueryable();
            if (id.HasValue)
            {
                query = query.Where(e => e.Id == id);
            }

            return await query.ToListAsync();
        }

        public async Task<Expert> GetExpertbyAccountId(int id)
        {
            return await _context.Experts
                .Where(f => f.AccountId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Expert> GetExpert(int id)
        {
            return await _context.Experts
                .Include(e => e.Account)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
