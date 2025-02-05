using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class DriverRepository : GenericRepository<Driver>
    {
        public DriverRepository() { }
        public DriverRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Driver>> GetDrivers()
        {
            return await _context.Drivers
                .Include(d => d.Account)
                .ToListAsync();
        }
    }
}
