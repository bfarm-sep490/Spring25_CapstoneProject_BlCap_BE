using Spring25.BlCapstone.BE.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class PlanTransactionRepository : GenericRepository<PlanTransaction>
    {
        public PlanTransactionRepository()
        {
            _context ??= new Context();
        }
        public PlanTransactionRepository(Context context)
        {
            _context = context;
        }
    }
}
