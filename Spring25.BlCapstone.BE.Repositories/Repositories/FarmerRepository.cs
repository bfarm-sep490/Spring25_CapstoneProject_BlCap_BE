﻿using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class FarmerRepository : GenericRepository<Farmer>
    {
        public FarmerRepository() {
            _context ??= new Context();
        }

        public FarmerRepository(Context context) 
        {
            _context = context;
        }

        public async Task<List<Farmer>> GetFarmers()
        {
            return await _context.Farmers
                .Include(f => f.Account)
                .ToListAsync();
        }
    }
}
