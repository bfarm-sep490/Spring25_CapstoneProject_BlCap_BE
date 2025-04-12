using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class FarmerPermissionRepository : GenericRepository<FarmerPermission>
    {
        public FarmerPermissionRepository() { }
        public FarmerPermissionRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<FarmerPermission>> GetFarmerPermissionsByPlanId(int planId)
        {
            return await _context.FarmerPermissions
                                 .Where(fp => fp.PlanId == planId)
                                 .ToListAsync();
        }

        public async Task<FarmerPermission> GetFarmerPermission(int planId, int farmerId)
        {
            return await _context.FarmerPermissions
                                 .FirstOrDefaultAsync(fp => fp.PlanId == planId && fp.FarmerId == farmerId);
        }

        public async Task<List<FarmerPermission>> GetPlanFarmerAssign(int farmerId)
        {
            return await _context.FarmerPermissions
                                 .Include(p => p.Plan)
                                    .ThenInclude(p => p.Plant)
                                 .Include(p => p.Plan)
                                    .ThenInclude(p => p.Yield)
                                 .Include(p => p.Plan)
                                    .ThenInclude(p => p.Expert)
                                        .ThenInclude(p => p.Account)
                                    .Where(p => p.FarmerId == farmerId)
                                 .ToListAsync();
        }
    }
}
