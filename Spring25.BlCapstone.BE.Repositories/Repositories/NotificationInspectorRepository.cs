using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class NotificationInspectorRepository : GenericRepository<NotificationInspector>
    {
        public NotificationInspectorRepository() { }
        public NotificationInspectorRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<NotificationInspector>> GetNotificationsByInspectorId(int id)
        {
            return await _context.NotificationInspectors
                                 .Where(ni => ni.InspectorId == id)
                                 .ToListAsync();
        }
    }
}
