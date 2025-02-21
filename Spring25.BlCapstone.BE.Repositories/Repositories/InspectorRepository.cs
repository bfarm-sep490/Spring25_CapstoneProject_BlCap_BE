using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class InspectorRepository : GenericRepository<Inspector>
    {
        public InspectorRepository() { }
        public InspectorRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Inspector>> GetInspectors()
        {
            return await _context.Inspectors
                .Include(f => f.Account)
                .ToListAsync();
        }

        public async Task<Inspector> GetInspector(int id)
        {
            return await _context.Inspectors
                .Include(f => f.Account)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Inspector> GetInspectorbyAccountId(int id)
        {
            return await _context.Inspectors
                .Where(f => f.AccountId == id)
                .FirstOrDefaultAsync();
        }

    }
}
