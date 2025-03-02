using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class InspectingFormRepository : GenericRepository<InspectingForm>
    {
        public InspectingFormRepository() { }
        public InspectingFormRepository(Context context)
        {
            _context = context;
        }

        public async Task<InspectingForm> GetDetailInspectingFormById(int id)
        {
            return await _context.InspectingForms.Where(x =>x.Id == id)
                .Include(x=>x.InspectingImages)
                .Include(x=>x.Inspector)
                .ThenInclude(x => x.Account)
                .FirstOrDefaultAsync();
        }

        public async Task<List<InspectingForm>> GetInspectingForms(int? planId)
        {
            if (planId != null)
            {
                return await _context.InspectingForms
                    .Where(ifs => ifs.PlanId == planId)
                    .Include(x => x.InspectingImages)
                    .Include(x => x.Inspector)
                    .ThenInclude(x => x.Account)
                    .ToListAsync();
            }
            else
            {
                return await _context.InspectingForms
                    .Include(x => x.InspectingImages)
                    .Include(x => x.Inspector)
                    .ThenInclude(x => x.Account)
                    .ToListAsync();
            }
        }
    }
}
