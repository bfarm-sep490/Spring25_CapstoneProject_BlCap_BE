using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
                .Include(f => f.FarmerSpecializations)
                    .ThenInclude(f => f.Specialization)
                .Include(f => f.FarmerPerformance)
                .ToListAsync();
        }
        public async Task<Farmer> GetFarmerbyAccountId(int id)
        {
            return await _context.Farmers
                .Where(f => f.AccountId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<FarmerPermission>> GetFarmersByPlanId(int planId)
        {
            return await _context.FarmerPermissions.Include(x => x.Farmer).ThenInclude(x => x.Account).Where(x => x.PlanId == planId).ToListAsync();
        }
        public async Task<List<Farmer>> GetFreeFarmersByPlanId(int planId, DateTime start, DateTime end)
        {
            var freeFarmers = await _context.FarmerPermissions
        .Where(fp => fp.PlanId == planId)
        .Where(fp => !_context.FarmerCaringTasks.Include(x => x.CaringTask)
            .Any(fct => fct.FarmerId == fp.FarmerId
                        && fct.Status == "Active"
                        && fct.CaringTask.StartDate < end
                        && fct.CaringTask.EndDate > start)
        )
        .Where(fp => !_context.FarmerHarvestingTasks.Include(x => x.HarvestingTask)
            .Any(fht => fht.FarmerId == fp.FarmerId
                        && fht.Status == "Active"
                        && fht.HarvestingTask.StartDate < end
                        && fht.HarvestingTask.EndDate > start)
        )
        .Where(fp => !_context.FarmerPackagingTasks.Include(x => x.PackagingTask)
            .Any(fpt => fpt.FarmerId == fp.FarmerId
                        && fpt.Status == "Active"
                        && fpt.PackagingTask.StartDate < end
                        && fpt.PackagingTask.EndDate > start)
        )
        .Include(fp => fp.Farmer).ThenInclude(x => x.Account)
        .Select(fp => fp.Farmer)
        .ToListAsync();

            return freeFarmers;
        }
        public async Task<List<Farmer>> GetFarmersByListId(List<int> ids)
        {
            if (ids == null) return new List<Farmer>(); 
            return await _context.Farmers
                .Where(f => ids.Contains(f.Id))
                .ToListAsync();
        }
        public async Task<List<Farmer>> GetFreeFarmerByListId(List<int> ids, DateTime start, DateTime end)
        {
            var freeFarmers = await _context.Farmers
                .Where(fp => ids.Contains(fp.Id)) 
                .Where(fp => !_context.FarmerCaringTasks
                    .Any(fct => fct.FarmerId == fp.Id
                                && fct.Status == "Active"
                                && fct.CaringTask.StartDate < end
                                && fct.CaringTask.EndDate > start))
                .Where(fp => !_context.FarmerHarvestingTasks
                    .Any(fht => fht.FarmerId == fp.Id
                                && fht.Status == "Active"
                                && fht.HarvestingTask.StartDate < end
                                && fht.HarvestingTask.EndDate > start)) 
                .Where(fp => !_context.FarmerPackagingTasks
                    .Any(fpt => fpt.FarmerId == fp.Id
                                && fpt.Status == "Active"
                                && fpt.PackagingTask.StartDate < end
                                && fpt.PackagingTask.EndDate > start)) 
                .Include(f => f.Account) 
                .ToListAsync();
            return freeFarmers;
        }
    }
}
