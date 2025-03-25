﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Order>> GetAllOrder(string? status, int? retailer)
        {
            var query = _context.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status.ToLower() == status.ToLower());
            }

            if (retailer.HasValue)
            {
                query = query.Where(o => o.RetailerId == retailer.Value);
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

        public async Task<Order> GetOrderByTransactionId(int id)
        {
            return await _context.Orders
                                 .Include(o => o.Transactions)
                                 .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
