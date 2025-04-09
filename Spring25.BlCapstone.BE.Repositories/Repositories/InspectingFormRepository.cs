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

        public async Task<List<InspectingForm>> GetInspectingForms(int? planId = null, int? inspectorId = null, int? formId = null)
        {
            var query = _context.InspectingForms
                                .Include(x => x.InspectingResult)
                                    .ThenInclude(x => x.InspectingImages)
                                .Include(x => x.Inspector)
                                    .ThenInclude(x => x.Account)
                                .Include(x => x.Plan)
                                .AsQueryable();
            if (planId.HasValue)
            {
                query = query.Where(ifs => ifs.PlanId == planId);
            }

            if (inspectorId.HasValue)
            {
                query = query.Where(ifs => ifs.InspectorId == inspectorId);
            }

            if (formId.HasValue)
            {
                query = query.Where(ifs => ifs.Id == formId);
            }

            return await query.ToListAsync();
        }

        public async Task<InspectingForm> GetInspectingFormById(int id)
        {
            return await _context.InspectingForms
                                        .Include(ir => ir.Inspector)
                                            .ThenInclude(ir => ir.Account)
                                 .FirstOrDefaultAsync(ir => ir.Id == id);
        }
    }
}
