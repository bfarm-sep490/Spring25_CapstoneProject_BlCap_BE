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

        public async Task<List<FarmerPermission>> GetPlanFarmerAssign(int farmerId, bool? is_active_in_plan = null, List<string>? listStatus = null)
        {
            var query = _context.FarmerPermissions
                                 .Include(p => p.Plan)
                                    .ThenInclude(p => p.Plant)
                                 .Include(p => p.Plan)
                                    .ThenInclude(p => p.Yield)
                                 .Include(p => p.Plan)
                                    .ThenInclude(p => p.Expert)
                                        .ThenInclude(p => p.Account)
                                .Include(p => p.Plan)
                                    .ThenInclude(p => p.InspectingForms)
                                        .ThenInclude(p => p.InspectingResult)
                                 .Where(p => p.FarmerId == farmerId);

            if (is_active_in_plan.HasValue)
            {
                var status = is_active_in_plan.Value ? "active" : "inactive";
                query = query.Where(p => p.Status.Trim().ToLower() == status);
            }

            if (listStatus?.Any() == true)
            {
                var normalizedStatus = listStatus.Select(s => s.ToLower().Trim()).ToList();
                query = query.Where(ct => normalizedStatus.Contains(ct.Plan.Status.ToLower().Trim()));
            }

            return await query.ToListAsync();
        }
    }
}
