using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class FertilizerRepository : GenericRepository<Fertilizer>
    {
        public FertilizerRepository() { }
        public FertilizerRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<Fertilizer>> GetFertilizersByFarmOwnerId(int farmId)
        {
            return await _context.Fertilizers.Where(x=>x.FarmOwnerId == farmId).ToListAsync();
        } 
    }
}
