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
                //.Include(x=>x.InspectingImages)
                .Include(x=>x.Inspector)
                .ThenInclude(x => x.Account)
                .FirstOrDefaultAsync();
        }

        public async Task<List<InspectingForm>> GetInspectingForms(int? planId = null, int? inspectorId = null)
        {
            var query = _context.InspectingForms
                                //.Include(x => x.InspectingImages)
                                .Include(x => x.Inspector)
                                    .ThenInclude(x => x.Account)
                                .AsQueryable();
            if (planId.HasValue)
            {
                query = query.Where(ifs => ifs.PlanId == planId);
            }

            if (inspectorId.HasValue)
            {
                query = query.Where(ifs => ifs.InspectorId == inspectorId);
            }

            return await query.ToListAsync();
        }
    }
}
