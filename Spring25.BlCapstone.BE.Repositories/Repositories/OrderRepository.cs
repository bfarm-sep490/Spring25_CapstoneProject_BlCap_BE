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

        public async Task<List<Order>> GetAllOrder(string? status = null, int? retailer = null, int? planId = null)
        {
            var query = _context.Orders
                                .Include(o => o.Transactions)
                                .Include(o => o.Retailer)
                                    .ThenInclude(o => o.Account)
                                .Include(o => o.Plant)
                                .Include(o => o.OrderPlans)
                                    .ThenInclude(o => o.Plan)
                                .Include(o => o.PackagingType)
                                .Include(o => o.OrderPlans)
                                    .ThenInclude(o => o.Plan)
                                        .ThenInclude(o => o.InspectingForms)
                                            .ThenInclude(o => o.InspectingResult)
                                .Include(o => o.PackagingTasks)
                                    .ThenInclude(o => o.PackagingProducts)
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
                query = query.Where(o => o.OrderPlans.Any(p => p.PlanId == planId));
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
                                 .Include(o => o.OrderPlans)
                                    .ThenInclude(o => o.Plan)
                                 .Include(o => o.PackagingType)
                                 //.Include(o => o.OrderProducts)
                                 .Include(o => o.OrderPlans)
                                    .ThenInclude(o => o.Plan)
                                        .ThenInclude(o => o.InspectingForms)
                                            .ThenInclude(o => o.InspectingResult)
                                 .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetOrderWithNoPlan()
        {
            var query = _context.Orders
                                .Include(o => o.Transactions)
                                .Include(o => o.Retailer)
                                    .ThenInclude(o => o.Account)
                                .Include(o => o.Plant)
                                .Include(o => o.OrderPlans)
                                    .ThenInclude(o => o.Plan)
                                .Include(o => o.PackagingType)
                                .Include(o => o.PackagingTasks)
                                    .ThenInclude(o => o.PackagingProducts)
                                .Include(o => o.OrderPlans)
                                    .ThenInclude(o => o.Plan)
                                        .ThenInclude(o => o.InspectingForms)
                                            .ThenInclude(o => o.InspectingResult)
                                .Where(o => o.OrderPlans.Sum(op => op.Quantity) < o.PreOrderQuantity);

            return await query.ToListAsync();
        }

        public async Task<Order> GetOrderByOrderId(int id)
        {
            return await _context.Orders
                                 .Include(o => o.OrderPlans)
                                    .ThenInclude(o => o.Plan)
                                 .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> ProductHasOrderOrNo(int productId)
        {
            return await _context.PackagingProducts
                                 .Include(p => p.PackagingTask)
                                 .AnyAsync(p => p.Id == productId && p.PackagingTask.OrderId != null);
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                                 .Include(o => o.PackagingType)   
                                 .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetAllOrdersByPlanId(int planId)
        {
            return await _context.Orders
                                 .Include(o => o.OrderPlans)
                                 .Where(o => o.OrderPlans.Any(p => p.PlanId == planId))
                                 .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrderPendingHasNoOrder()
        {
            return await _context.Orders
                                 .Include(o => o.OrderPlans)
                                 .Include(o => o.Plant)
                                 .Where(o => !o.OrderPlans.Any() && o.Status.ToLower().Trim().Equals("pending"))
                                 .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrderReachPickupDate()
        {
            return await _context.Orders
                                 .Where(o => o.EstimatedPickupDate.AddDays(1) > DateTime.Now && o.Status.ToLower().Trim().Equals("deposit"))
                                 .ToListAsync();
        }
    }
}
