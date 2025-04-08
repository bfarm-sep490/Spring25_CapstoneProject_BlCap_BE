using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class SpecializationRepository : GenericRepository<Specialization>
    {
        public SpecializationRepository() { }
        public SpecializationRepository(Context context)
        {
            _context = context;
        }
    }
}
