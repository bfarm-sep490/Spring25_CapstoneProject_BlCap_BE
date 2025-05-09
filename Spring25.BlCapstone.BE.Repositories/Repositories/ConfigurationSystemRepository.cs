﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<ConfigurationSystem> GetConfig()
        {
            var query = _context.ConfigurationSystems
                                .AsQueryable();

            return await query.FirstOrDefaultAsync();
        }
    }
}
