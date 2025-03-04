using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class ProblemImageRepository : GenericRepository<ProblemImage>
    {
        public ProblemImageRepository() { }
        public ProblemImageRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ProblemImage>> GetImages(int? problemId = null)
        {
            var query = _context.ProblemImages
                                .Include(pi => pi.Problem)
                                .AsQueryable();

            if (problemId.HasValue)
            {
                query = query.Where(pi => pi.ProblemId == problemId);
            }

            return await query.ToListAsync();
        }
    }
}
