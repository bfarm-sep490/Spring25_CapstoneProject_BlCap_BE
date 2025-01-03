using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class FieldRepository : GenericRepository<Field>
    {
        public FieldRepository() { }
        public FieldRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Field>> GetFields()
        {
            return await _context.Fields
                .Include(f => f.ImageFields)
                .Include(f => f.FarmOwner)
                .ToListAsync();
        }

        public async Task<Field> GetFieldsById(int id)
        {
            return await _context.Fields
                .Include(f => f.ImageFields)
                .Include(f => f.FarmOwner)
                .FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
