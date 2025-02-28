using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class CaringTaskRepository : GenericRepository<CaringTask>
    {
        public CaringTaskRepository() { }
        public CaringTaskRepository(Context context)
        {
            _context = context;
        }
        public async Task<CaringTask> GetDetail(int id)
        {
            return await _context.CaringTasks
                .Where(x => x.Id == id)
                .Include(x => x.CaringItems)
                .Include(x => x.CaringFertilizers)
                .Include(x => x.CaringPesticides)
                .Include(x => x.CaringImages)
                .Include(x => x.Farmer)
                .ThenInclude(x=>x.Account)
                .FirstOrDefaultAsync();
        }
    }
}
