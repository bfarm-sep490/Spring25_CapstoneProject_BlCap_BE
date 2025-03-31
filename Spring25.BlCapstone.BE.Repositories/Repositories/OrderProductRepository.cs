using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class OrderProductRepository : GenericRepository<OrderProduct>
    {
        public OrderProductRepository() { }
        public OrderProductRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<OrderProduct>> GetAllOrderProductsByOrderId(int orderId)
        {
            return await _context.OrderProducts
                                .Where(op => op.OrderId == orderId && op.Status.Trim().ToLower().Equals("waitingforpayment"))
                                .ToListAsync();
        }
    }
}
