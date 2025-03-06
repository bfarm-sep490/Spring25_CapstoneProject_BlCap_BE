using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class PackagingImageRepository : GenericRepository<PackagingImage>
    {
        public PackagingImageRepository() { }
        public PackagingImageRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<PackagingImage>> GetPackagingImagesByTaskId(int id)
        {
            return await _context.PackagingImages
                .Where(hi => hi.TaskId == id)
                .ToListAsync();
        }
    }
}
