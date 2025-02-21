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
        public async Task<Inspector> GetInspectorbyAccountId(int id)
        {
            return await _context.Inspectors
                .Where(f => f.AccountId == id)
                .FirstOrDefaultAsync();
        }
    }
}
