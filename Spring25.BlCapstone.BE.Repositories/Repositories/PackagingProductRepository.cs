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

        public async Task<List<PackagingProduct>> GetPackagingProducts()
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

            return await query.ToListAsync();
        }

        public async Task<PackagingProduct> GetProductById(int id)
        {
            return await _context.PackagingProducts
                                 .Include(pp => pp.PackagingTask)
                                    .ThenInclude(pp => pp.Plan)
                                        .ThenInclude(pp => pp.Plant)
                                .Include(pp => pp.HarvestingTask)
                                .FirstOrDefaultAsync(pp => pp.Id == id);
        }
    }
}
