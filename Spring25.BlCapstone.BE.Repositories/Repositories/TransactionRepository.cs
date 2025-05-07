using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.BusinessModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>
    {
        public TransactionRepository() { }
        public TransactionRepository(Context context)
        {
            _context = context;
        }

        public async Task<Transaction> GetTransactionByOrderCode(long OrderCode)
        {
            return await _context.Transactions
                                 .FirstOrDefaultAsync(t => t.OrderCode == OrderCode);
        }

        public async Task<List<Transaction>> GetTransactions(int? orderId = null, string? status = null)
        {
            var query = _context.Transactions
                                .AsQueryable();

            if (orderId.HasValue)
            {
                query = query.Where(t => t.OrderId == orderId);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(t => t.Status.Trim().ToLower() == status.ToLower().Trim());
            }

            return await query.ToListAsync();
        }

        public async Task<List<Transaction>> GetPendingTransactionByOrderId(int orderId)
        {
            return await _context.Transactions
                                 .Where(t => t.OrderId == orderId && t.Status.ToLower().Trim().Equals("pending"))
                                 .ToListAsync();
        }

        public async Task<List<DashboardTransactions>> GetDashboardTransactionsAsync(DateTime? start = null, DateTime? end = null)
        {
            var query = _context.Transactions
                                .Where(t => (t.Status.ToLower() == "paid" || t.Status.ToLower() == "cash") && t.PaymentDate.HasValue);

            var effectiveEndDate = end?.Date ?? DateTime.UtcNow.Date;

            if (start.HasValue)
            {
                query = query.Where(t => t.PaymentDate.Value.Date >= start.Value.Date);
            }

            query = query.Where(t => t.PaymentDate.Value.Date <= effectiveEndDate);

            return await query
                     .GroupBy(t => t.PaymentDate.Value.Date)
                     .Select(g => new DashboardTransactions
                     {
                         Date = g.Key,
                         PricePerDay = g.Sum(t => t.Price)
                     })
                     .OrderBy(d => d.Date)
                     .ToListAsync();
        }
    }
}
