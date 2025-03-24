using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class PlantRepository:GenericRepository<Plant>
    {
        public PlantRepository() 
        {
            _context ??= new Context();
        }
        public PlantRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Plant>> GetPlants(bool? isAvailable = null)
        {
            var query = _context.Plants.AsQueryable();

            return await query.ToListAsync();
        }
        public async Task<List<Yield>> GetSuggestPlantById(int id)
        {
            return await _context.PlantYields.Where(p => p.PlantId == id)
                .Include(x=>x.Yield)
                .Select(x=>x.Yield)
                .ToListAsync();          
        }

        public async Task<Plant> GetPlantByHarvestingTask(int taskId)
        {
            return await _context.HarvestingTasks
                                 .Include(ht => ht.Plan)
                                 .Where(ht => ht.Id == taskId)
                                    .Select(ht => ht.Plan.Plant)
                                 .FirstOrDefaultAsync();
        }
    }
}
