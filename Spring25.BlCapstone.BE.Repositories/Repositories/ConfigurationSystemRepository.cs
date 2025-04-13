using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class ConfigurationSystemRepository : GenericRepository<ConfigurationSystem>
    {
        public ConfigurationSystemRepository() { }
        public ConfigurationSystemRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ConfigurationSystem>> GetAllConfigs(string? status)
        {
            var query = _context.ConfigurationSystems
                                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x => x.Status.ToLower().Trim().Equals(status.ToLower().Trim()));
            }

            return await query.ToListAsync();
        }
    }
}
