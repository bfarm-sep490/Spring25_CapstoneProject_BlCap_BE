﻿using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class CaringTaskRepository : GenericRepository<CaringTask>
    {
        public CaringTaskRepository() { }
        public CaringTaskRepository(Context context)
        {
            _context = context;
        }
        public async Task<CaringTask> GetDetail(int id)
        {
            return await _context.CaringTasks
                .Where(x => x.Id == id)
                .Include(x => x.CaringItems)
                .Include(x => x.CaringFertilizers)
                .Include(x => x.CaringPesticides)
                .Include(x => x.CaringImages)
                .Include(x => x.Farmer)
                .ThenInclude(x=>x.Account)
                .FirstOrDefaultAsync();
        }

        public async Task<List<CaringTask>> GetAllCaringTasks(int? planId = null, int? farmerId = null)
        {
            var query = _context.CaringTasks
                                      .Include(x => x.CaringItems)
                                      .Include(x => x.CaringFertilizers)
                                      .Include(x => x.CaringPesticides)
                                      .Include(x => x.CaringImages)
                                      .Include(x => x.Farmer)
                                        .ThenInclude(x => x.Account)
                                      .AsQueryable();  

            if (planId.HasValue)
            {
                query = query.Where(ct => ct.PlanId == planId);
            }
            
            if (farmerId.HasValue)
            {
                query = query.Where(ct => ct.FarmerId == farmerId);
            }

            return await query.ToListAsync();
        }
    }
}
