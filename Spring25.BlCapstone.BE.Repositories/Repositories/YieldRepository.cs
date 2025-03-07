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
    }
}
