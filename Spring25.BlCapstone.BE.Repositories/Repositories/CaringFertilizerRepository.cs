using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class CaringFertilizerRepository : GenericRepository<CaringFertilizer>
    {
        public CaringFertilizerRepository() { }
        public CaringFertilizerRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<CaringFertilizer>> GetCaringFertilizersByTaskId(int taskId)
        {
            return await _context.CaringFertilizers
                                 .Where(cf => cf.TaskId == taskId)
                                 .ToListAsync();
        }

        public async Task<List<CaringFertilizer>> GetCareFertilizersByPlanId(int planId)
        {
            return await _context.CaringFertilizers
                                 .Include(cf => cf.CaringTask)
                                 .Where(cf => cf.CaringTask.PlanId == planId)
                                 .ToListAsync();
        }
        public async Task<List<CaringFertilizer>> GetCaringFertilizersByPlanId(int planid)
        {
            var listTask = await _context.CaringTasks                
                                 .Where(cf => cf.PlanId == planid)
                                 .SelectMany(x => x.CaringFertilizers)
                                 .Include(x=>x.Fertilizer)
                                 .ToListAsync();          
            return listTask;
        }
        public async Task<List<Fertilizer>> GetFertilizersByPlanId(int planid)
        {
            var result = await _context.CaringTasks
                                 .Where(cf => cf.PlanId == planid)
                                 .Include(x => x.CaringFertilizers)
                                 .ThenInclude(x => x.Fertilizer)
                                 .SelectMany(x=>x.CaringFertilizers.Select(y=>y.Fertilizer))
                                 .Distinct()
                                 .ToListAsync();
            return result;
        }
        public async Task<float> EstimateFertilizerInPlan(int planId, int fertilizerId)
        {
            var list = await _context.CaringTasks
                                .Where(x => x.PlanId == planId)
                                .SelectMany(x => x.CaringFertilizers)
                                .ToListAsync();
            var kilogam = list.Where(x => x.FertilizerId == fertilizerId && x.Unit.ToLower() == "kg").Sum(x => x.Quantity);
            var gam = list.Where(x => x.FertilizerId == fertilizerId && x.Unit.ToLower() == "gam").Sum(x => x.Quantity);
            return kilogam + gam / 1000; 
        }
        public async Task<float> UsedFertilizerInPlan(int planId, int fertilizerId)
        {
            var list = await _context.CaringTasks
                              .Where(x => x.PlanId == planId && x.Status.ToLower() != "cancel" && x.Status.ToLower() != "pending")
                              .SelectMany(x => x.CaringFertilizers)
                              .ToListAsync();
            var kilogam = list.Where(x => x.FertilizerId == fertilizerId && x.Unit.ToLower()=="kg").Sum(x => x.Quantity);
            var gam = list.Where(x => x.FertilizerId == fertilizerId && x.Unit.ToLower()== "gam").Sum(x => x.Quantity);
            return kilogam + gam/1000;
        }
        public async Task<List<Pesticide>> GetPesticidesByPlanId(int planid)
        {
            var result = await _context.CaringTasks
                                 .Where(cf => cf.PlanId == planid)
                                 .Include(x => x.CaringPesticides)
                                 .ThenInclude(x => x.Pesticide)
                                 .SelectMany(x => x.CaringPesticides.Select(y => y.Pesticide))
                                 .Distinct()
                                 .ToListAsync();
            return result;
        }
    }
}
