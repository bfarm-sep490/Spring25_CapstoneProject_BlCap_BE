using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class ProductPickupBatchRepository : GenericRepository<ProductPickupBatch>
    {
        public ProductPickupBatchRepository() { }
        public ProductPickupBatchRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ProductPickupBatch>> GetAllBatches(int? orderId = null)
        {
            var query = _context.ProductPickupBatchs
                                .Include(b => b.PackagingProduct)
                                    .ThenInclude(b => b.PackagingTask)
                                .AsQueryable();

            if (orderId.HasValue)
            {
                query = query.Where(b => b.PackagingProduct.PackagingTask.OrderId == orderId);
            }

            return await query.ToListAsync();
        }
    }
}
