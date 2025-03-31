using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository() { }
        public OrderRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrder(string? status, int? retailer, int? planId)
        {
            var query = _context.Orders
                                .Include(o => o.Transactions)
                                .Include(o => o.Retailer)
                                    .ThenInclude(o => o.Account)
                                .Include(o => o.Plant)
                                .Include(o => o.Plan)
                                .Include(o => o.PackagingType)
                                .Include(o => o.OrderProducts)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status.ToLower() == status.ToLower());
            }

            if (retailer.HasValue)
            {
                query = query.Where(o => o.RetailerId == retailer);
            }

            if (planId.HasValue)
            {
                query = query.Where(o => o.PlanId == planId);
            }

            return await query.ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders
                                 .Include(o => o.Plant)
                                 .Include(o => o.Retailer)
                                    .ThenInclude(o => o.Account)
                                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _context.Orders
                                 .Include(o => o.Transactions)
                                 .Include(o => o.Retailer)
                                    .ThenInclude(o => o.Account)
                                 .Include(o => o.Plant)
                                 .Include(o => o.Plan)
                                 .Include(o => o.PackagingType)
                                 .Include(o => o.OrderProducts)
                                 .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetOrderWithNoPlan()
        {
            return await _context.Orders
                                .Include(o => o.Transactions)
                                .Include(o => o.Retailer)
                                    .ThenInclude(o => o.Account)
                                .Include(o => o.Plant)
                                .Include(o => o.Plan)
                                .Include(o => o.PackagingType)
                                .Include(o => o.OrderProducts)
                                .Where(o => o.PlanId == null)
                                .ToListAsync();
        }
    }
}
