using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class NotificationRetailerRepository : GenericRepository<NotificationRetailer>
    {
        public NotificationRetailerRepository() { }
        public NotificationRetailerRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<NotificationRetailer>> GetNotificationsByRetailerId(int id)
        {
            return await _context.NotificationRetailers
                                 .Where(nf => nf.RetailerId == id)
                                 .ToListAsync();
        }
    }
}
