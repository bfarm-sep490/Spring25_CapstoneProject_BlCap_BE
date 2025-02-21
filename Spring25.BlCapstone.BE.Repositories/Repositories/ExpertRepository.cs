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

        public async Task<List<Expert>> GetExperts()
        {
            return await _context.Experts
                .Include(f => f.Account)
                .ToListAsync();
        }
        public async Task<Expert> GetExpertbyAccountId(int id)
        {
            return await _context.Experts
                .Where(f => f.AccountId == id)
                .FirstOrDefaultAsync();
        }
    }
}
