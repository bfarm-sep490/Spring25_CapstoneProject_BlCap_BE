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

            if (isAvailable != null)
            {
                query = query.Where(p => p.IsAvailable == isAvailable);
            }

            return await query.ToListAsync();
        }
    }
}
