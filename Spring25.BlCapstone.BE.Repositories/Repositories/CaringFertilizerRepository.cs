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
    }
}
