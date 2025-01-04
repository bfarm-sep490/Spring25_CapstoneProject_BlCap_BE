using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class TaskRepository : GenericRepository<Models.Task>
    {
        public TaskRepository() { }
        public TaskRepository(Context context)
        {
            _context = context;
        }
    }
}
