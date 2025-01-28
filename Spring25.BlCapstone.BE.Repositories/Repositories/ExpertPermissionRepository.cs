using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class ExpertPermissionRepository : GenericRepository<ExpertPermission>
    {
        public ExpertPermissionRepository() { }
        public ExpertPermissionRepository(Context context)
        {
            _context = context;
        }
    }
}
