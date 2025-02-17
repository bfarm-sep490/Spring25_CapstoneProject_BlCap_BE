using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class OrderPlantRepository : GenericRepository<OrderPlant>
    {
        public OrderPlantRepository() { }
        public OrderPlantRepository(Context context)
        {
            _context = context;
        }
    }
}
