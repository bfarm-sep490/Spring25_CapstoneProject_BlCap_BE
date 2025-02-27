using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class HarvestingImageRepository : GenericRepository<HarvestingImage>
    {
        public HarvestingImageRepository() { }
        public HarvestingImageRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<HarvestingImage>> GetHarvestingImagesByTaskId(int id)
        {
            return await _context.HarvestingImages
                .Where(hi => hi.TaskId == id)
                .ToListAsync();
        }
    }
}
