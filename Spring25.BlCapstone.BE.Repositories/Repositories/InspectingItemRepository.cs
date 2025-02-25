using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class InspectingItemRepository : GenericRepository<InspectingItem>
    {
        public InspectingItemRepository() { }
        public InspectingItemRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<InspectingItem>> GetInspectingItemByPlanId(int planId)
        {
            return await _context.InspectingItems
                .Include(ci => ci.Item)
                .Include(ci => ci.InspectingForm)
                    .ThenInclude(ci => ci.Plan)
                .Where(ci => ci.InspectingForm.PlanId == planId)
                .ToListAsync();
        }
    }
}
