using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class FarmerRepository : GenericRepository<Farmer>
    {
        public FarmerRepository() {
            _context ??= new Context();
        }

        public FarmerRepository(Context context) 
        {
            _context = context;
        }

        public async Task<List<Farmer>> GetFarmers()
        {
            return await _context.Farmers
                .Include(f => f.Account)
                .ToListAsync();
        }
        public async Task<Farmer> GetFarmerbyAccountId(int id)
        {
            return await _context.Farmers
                .Where(f => f.AccountId == id)
                .FirstOrDefaultAsync() ;
        }

        public async Task<List<Farmer>> GetFarmersByPlanId(int planId)
        {
            return await _context.Farmers
                .Include(f => f.Account)
                .Include(f => f.FarmerPermissions)
                    .Where(f => f.FarmerPermissions.Any(fp => fp.PlanId == planId && fp.Status.ToLower().Trim() == "Active"))
                .ToListAsync();
        }
    }
}
