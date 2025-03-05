using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class CaringImageRepository : GenericRepository<CaringImage>
    {
        public CaringImageRepository() { }
        public CaringImageRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<CaringImage>> GetCaringImagesByTaskId(int taskId)
        {
            return await _context.CaringImages
                                 .Where(c => c.TaskId == taskId)
                                 .ToListAsync();
        }
    }
}
