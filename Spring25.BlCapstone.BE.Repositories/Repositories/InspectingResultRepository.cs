using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class InspectingResultRepository : GenericRepository<InspectingResult>
    {
        public InspectingResultRepository() { }
        public InspectingResultRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<InspectingResult>> GetInspectingResults(string? evaluatedResult = null, int? resultId = null)
        {
            var query = _context.InspectingResults
                                .Include(ir => ir.InspectingImages)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(evaluatedResult))
            {
                query = query.Where(ir => ir.EvaluatedResult.ToLower().Trim() == evaluatedResult.Trim().ToLower());
            }

            if (resultId.HasValue)
            {
                query = query.Where(ir => ir.Id == resultId.Value);
            }

            return await query.ToListAsync();
        }
    }
}
