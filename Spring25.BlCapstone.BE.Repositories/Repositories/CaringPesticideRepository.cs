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
    }
}
