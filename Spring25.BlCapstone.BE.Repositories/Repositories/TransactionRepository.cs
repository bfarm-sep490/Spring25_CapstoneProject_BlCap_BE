using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
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
    }
}
