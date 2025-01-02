using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class FarmerRepository :GenericRepository<Farmer>
    {
        public FarmerRepository()
        {
            this._context ??= new Context();
        }
        public FarmerRepository(Context context)
        {
            this._context = context;
        }
    }
}
