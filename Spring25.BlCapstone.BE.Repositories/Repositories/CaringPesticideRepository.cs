using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class CaringPesticideRepository : GenericRepository<CaringPesticide>
    {
        public CaringPesticideRepository() { }
        public CaringPesticideRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<CaringPesticide>> GetCaringPesticidesByTaskId(int taskId)
        {
            return await _context.CaringPesticides
                                 .Where(c => c.TaskId == taskId)
                                 .ToListAsync();
        }
        public async Task<float> EstimatePesticideInPlan(int planId, int pesticideId)
        {
            var list = await _context.CaringTasks
                                .Where(x => x.PlanId == planId)
                                .SelectMany(x => x.CaringPesticides)
                                .ToListAsync();
            var kilogam = list.Where(x => x.PesticideId == pesticideId && x.Unit.ToLower() == "kg").Sum(x => x.Quantity);
            var gam = list.Where(x => x.PesticideId == pesticideId && x.Unit.ToLower() == "gam").Sum(x => x.Quantity);
            return kilogam + gam / 1000;
        }
        public async Task<float> UsedPesticideInPlan(int planId, int pesticideId)
        {
            var list = await _context.CaringTasks
                              .Where(x => x.PlanId == planId && x.Status.ToLower() != "cancel" && x.Status.ToLower() != "pending")
                              .SelectMany(x => x.CaringPesticides)
                              .ToListAsync();
            var kilogam = list.Where(x => x.PesticideId == pesticideId && x.Unit.ToLower() == "kg").Sum(x => x.Quantity);
            var gam = list.Where(x => x.PesticideId == pesticideId && x.Unit.ToLower() == "gam").Sum(x => x.Quantity);
            return kilogam + gam / 1000;
        }

        public async Task<List<CaringPesticide>> GetCarePesticidesByPlanId(int planId)
        {
            return await _context.CaringPesticides
                                 .Include(cp => cp.CaringTask)
                                 .Where(cp => cp.CaringTask.PlanId == planId)
                                 .ToListAsync();
        }
    }
}
