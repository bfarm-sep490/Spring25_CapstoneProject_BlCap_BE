using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class PlantYieldRepository : GenericRepository<PlantYield>
    {
        public PlantYieldRepository() { }
        public PlantYieldRepository(Context context)
        {
            _context = context;
        }

        public async Task<PlantYield> CreatePlantYield(int yieldId, int plantId)
        {
          var result =  await _context.PlantYields.AddAsync(new PlantYield
            {
                YieldId= yieldId,
                PlantId = plantId
            }
            );
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task DeletePlantYield(PlantYield plantYield)
        {
            _context.PlantYields.Remove(plantYield);
            await _context.SaveChangesAsync();
        }
        public async Task<PlantYield> GetPlantYield(int yieldId, int plantId)
        {
          return await _context.PlantYields
                .Where(x=>x.YieldId==yieldId && x.PlantId==plantId)
                .FirstOrDefaultAsync();
        }
    }
}
