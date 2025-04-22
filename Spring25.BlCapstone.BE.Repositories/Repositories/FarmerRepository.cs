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

        public async Task<Farmer> GetFarmerById(int id)
        {
            return await _context.Farmers
                .Include(f => f.Account)
                .Include(f => f.FarmerSpecializations)
                    .ThenInclude(f => f.Specialization)
                .Include(f => f.FarmerPerformance)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Farmer> GetFarmerbyAccountId(int id)
        {
            return await _context.Farmers
                .Where(f => f.AccountId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<FarmerPermission>> GetFarmersByPlanId(int planId)
        {
            return await _context.FarmerPermissions
                                 .Include(x => x.Farmer)
                                    .ThenInclude(x => x.Account)
                                 .Where(x => x.PlanId == planId)
                                 .ToListAsync();
        }

        public async Task<List<Farmer>> GetBusyFarmersByPlanId(int planId, DateTime? start = null, DateTime? end = null)
        {
            var plan = await _context.Plans.FindAsync(planId);

            DateTime effectiveStart = start ?? plan.StartDate.Value;
            DateTime effectiveEnd = end ?? plan.EndDate.Value;

            var farmerPermissions = await _context.FarmerPermissions
                                                  .Include(fp => fp.Farmer)
                                                      .ThenInclude(f => f.Account)
                                                  .Include(fp => fp.Farmer)
                                                      .ThenInclude(f => f.FarmerCaringTasks)
                                                          .ThenInclude(fct => fct.CaringTask)
                                                  .Include(fp => fp.Farmer)
                                                      .ThenInclude(f => f.FarmerHarvestingTasks)
                                                          .ThenInclude(fht => fht.HarvestingTask)
                                                  .Include(fp => fp.Farmer)
                                                      .ThenInclude(f => f.FarmerPackagingTasks)
                                                          .ThenInclude(fpt => fpt.PackagingTask)
                                                  .Where(fp => fp.PlanId == planId)
                                                  .ToListAsync();

            var busyFarmers = farmerPermissions.Select(fp => fp.Farmer)
                                               .Where(farmer =>
                                    farmer.FarmerCaringTasks.Any(fct =>
                                        fct.Status == "Active" &&
                                        fct.CaringTask.Status.ToLower().Trim() == "ongoing" &&
                                        fct.CaringTask.Status.ToLower().Trim() == "pending" &&
                                        fct.CaringTask.Status.ToLower().Trim() == "draft" &&
                                        IsDateOverlap(fct.CaringTask.StartDate, fct.CaringTask.EndDate, effectiveStart, effectiveEnd)
                                    )
                                    ||
                                    farmer.FarmerHarvestingTasks.Any(fht =>
                                        fht.Status == "Active" &&
                                        fht.HarvestingTask.Status.ToLower().Trim() == "ongoing" &&
                                        fht.HarvestingTask.Status.ToLower().Trim() == "pending" &&
                                        fht.HarvestingTask.Status.ToLower().Trim() == "draft" &&
                                        IsDateOverlap(fht.HarvestingTask.StartDate, fht.HarvestingTask.EndDate, effectiveStart, effectiveEnd)
                                    )
                                    ||
                                    farmer.FarmerPackagingTasks.Any(fpt =>
                                        fpt.Status == "Active" &&
                                        fpt.PackagingTask.Status.ToLower().Trim() == "ongoing" &&
                                        fpt.PackagingTask.Status.ToLower().Trim() == "pending" &&
                                        fpt.PackagingTask.Status.ToLower().Trim() == "draft" &&
                                        IsDateOverlap(fpt.PackagingTask.StartDate, fpt.PackagingTask.EndDate, effectiveStart, effectiveEnd)
                                    )
                                ).ToList();

            return busyFarmers;
        }

        private bool IsDateOverlap(DateTime taskStart, DateTime taskEnd, DateTime rangeStart, DateTime rangeEnd)
        {
            return taskStart <= rangeEnd && taskEnd >= rangeStart;
        }

        public async Task<bool> GetFarmersByListId(List<int> ids, int planId)
        {
            var farmers = await _context.FarmerPermissions
                .Where(fp => fp.PlanId == planId
                     && ids.Contains(fp.FarmerId)
                     && fp.Status == "Active")
                .Select(fp => fp.FarmerId)
                .ToListAsync();

            return farmers.Count == ids.Count;
        }

        public async Task<List<Farmer>> GetFreeFarmerByListId(List<int> ids, DateTime start, DateTime end)
        {
            var farmerPermissions = await _context.Farmers
                                                      .Include(f => f.Account)
                                                      .Include(f => f.FarmerCaringTasks)
                                                          .ThenInclude(fct => fct.CaringTask)
                                                      .Include(f => f.FarmerHarvestingTasks)
                                                          .ThenInclude(fht => fht.HarvestingTask)
                                                      .Include(f => f.FarmerPackagingTasks)
                                                          .ThenInclude(fpt => fpt.PackagingTask)
                                                  .ToListAsync();

            var freeFarmers = farmerPermissions
                .Where(fp => ids.Contains(fp.Id)) 
                .Where(farmer =>
                                    !farmer.FarmerCaringTasks.Any(fct =>
                                        fct.Status == "Active" &&
                                        fct.CaringTask.Status.ToLower().Trim() == "ongoing" &&
                                        fct.CaringTask.Status.ToLower().Trim() == "pending" &&
                                        fct.CaringTask.Status.ToLower().Trim() == "draft" &&
                                        IsDateOverlap(fct.CaringTask.StartDate, fct.CaringTask.EndDate, start, end)
                                    )
                                    &&
                                    !farmer.FarmerHarvestingTasks.Any(fht =>
                                        fht.Status == "Active" &&
                                        fht.HarvestingTask.Status.ToLower().Trim() == "ongoing" &&
                                        fht.HarvestingTask.Status.ToLower().Trim() == "pending" &&
                                        fht.HarvestingTask.Status.ToLower().Trim() == "draft" &&
                                        IsDateOverlap(fht.HarvestingTask.StartDate, fht.HarvestingTask.EndDate, start, end)
                                    )
                                    &&
                                    !farmer.FarmerPackagingTasks.Any(fpt =>
                                        fpt.Status == "Active" &&
                                        fpt.PackagingTask.Status.ToLower().Trim() == "ongoing" &&
                                        fpt.PackagingTask.Status.ToLower().Trim() == "pending" &&
                                        fpt.PackagingTask.Status.ToLower().Trim() == "draft" &&
                                        IsDateOverlap(fpt.PackagingTask.StartDate, fpt.PackagingTask.EndDate, start, end)
                                    ))
                .ToList();
            return freeFarmers;
        }
    }
}
