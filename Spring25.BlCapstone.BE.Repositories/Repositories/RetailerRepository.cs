﻿using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class RetailerRepository : GenericRepository<Retailer>
    {
        public RetailerRepository() { }
        public RetailerRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Retailer>> GetRetailers()
        {
            return await _context.Retailers
                .Include(d => d.Account)
                .ToListAsync();
        }
        public async Task<Retailer> GetRetailerbyAccountId(int id)
        {
            return await _context.Retailers
                .Where(f => f.AccountId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Retailer> GetRetailerById(int id)
        {
            return await _context.Retailers
                .Include(d => d.Account)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
