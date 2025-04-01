using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class PackagingProductRepository : GenericRepository<PackagingProduct>
    {
        public PackagingProductRepository() { }
        public PackagingProductRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<PackagingProduct>> GetPackagingProducts(int? planId, string? status)
        {
            var query = _context.PackagingProducts
                                .Include(pp => pp.PackagingTask)
                                    .ThenInclude(pp => pp.Plan)
                                        .ThenInclude(pp => pp.InspectingForms)
                                            .ThenInclude(pp => pp.InspectingResult)
                                .Include(pp => pp.PackagingTask)
                                    .ThenInclude(pp => pp.Plan)
                                        .ThenInclude(pp =>  pp.Plant)
                                .Include(pp => pp.HarvestingTask)
                                .AsQueryable();
            if (planId.HasValue)
            {
                query = query.Where(x => x.PackagingTask.PlanId == planId);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x => x.Status.ToLower().Trim().Equals(status.ToLower().Trim()));
            }

            return await query.ToListAsync();
        }

        public async Task<PackagingProduct> GetProductById(int id)
        {
            return await _context.PackagingProducts
                                 .Include(pp => pp.PackagingTask)
                                    .ThenInclude(pp => pp.Plan)
                                        .ThenInclude(pp => pp.InspectingForms)
                                            .ThenInclude(pp => pp.InspectingResult)
                                .Include(pp => pp.PackagingTask)
                                    .ThenInclude(pp => pp.Plan)
                                        .ThenInclude(pp => pp.Plant)
                                .Include(pp => pp.HarvestingTask)
                                .FirstOrDefaultAsync(pp => pp.Id == id);
        }

        public async Task<List<PackagingProduct>> GetExpiredProducts()
        {
            return await _context.PackagingProducts
                                 .Include(pp => pp.HarvestingTask)
                                 .Where(pp => pp.HarvestingTask.ProductExpiredDate < DateTime.Now && pp.HarvestingTask.ProductExpiredDate.HasValue)
                                 .ToListAsync();
        }
    }
}
