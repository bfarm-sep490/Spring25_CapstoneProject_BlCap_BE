using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class CaringItemRepository : GenericRepository<CaringItem>
    {
        public CaringItemRepository() { }
        public CaringItemRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<CaringItem>> GetCaringItemByPlanId(int planId)
        {
            return await _context.CaringItems
                .Include(ci => ci.Item)
                .Include(ci => ci.CaringTask)
                    .ThenInclude(ci => ci.Plan)
                .Where(ci => ci.CaringTask.PlanId == planId)
                .ToListAsync();
        }
    }
}
