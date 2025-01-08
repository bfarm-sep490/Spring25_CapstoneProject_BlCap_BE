using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class PesticideRepository:GenericRepository<Pesticide>
    {
        public PesticideRepository() { }
        public PesticideRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<Pesticide>> GetFertilizersByFarmOwnerId(int farmId)
        {
            return await _context.Pesticides.Where(x => x.FarmOwnerId == farmId).ToListAsync();
        }
    }
}
