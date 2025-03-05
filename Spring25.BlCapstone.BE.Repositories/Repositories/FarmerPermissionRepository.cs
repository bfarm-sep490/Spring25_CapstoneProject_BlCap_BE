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
    }
}
