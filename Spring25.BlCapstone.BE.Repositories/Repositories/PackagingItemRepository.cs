using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class PackagingItemRepository : GenericRepository<PackagingItem>
    {
        public PackagingItemRepository() { }
        public PackagingItemRepository(Context context)
        {
            _context = context;
        }
    }
}
