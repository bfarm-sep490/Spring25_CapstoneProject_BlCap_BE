using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class SeasonalPlantRepository : GenericRepository<SeasonalPlant>
    {
        public SeasonalPlantRepository() { }
        public SeasonalPlantRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<SeasonalPlant>> GetSeasonalPlantByPlantIdAndDay(int plantId, DateTime date)
        {
            int month = date.Month;
            int day = date.Day;

            var result = await _context.SeasonalPlants
                .Where(x => x.PlantId == plantId)
                .Where(x =>
                    (
                        (x.StartDate.Month < x.EndDate.Month ||
                        (x.StartDate.Month == x.EndDate.Month && x.StartDate.Day <= x.EndDate.Day)) &&
                        (
                            (x.StartDate.Month < month || (x.StartDate.Month == month && x.StartDate.Day <= day)) &&
                            (x.EndDate.Month > month || (x.EndDate.Month == month && x.EndDate.Day >= day))
                        )
                    )
                    ||
                    (
                        (x.StartDate.Month > x.EndDate.Month ||
                        (x.StartDate.Month == x.EndDate.Month && x.StartDate.Day > x.EndDate.Day)) &&
                        (
                            (x.StartDate.Month < month || (x.StartDate.Month == month && x.StartDate.Day <= day)) ||
                            (x.EndDate.Month > month || (x.EndDate.Month == month && x.EndDate.Day >= day))
                        )
                    )
                )
                .ToListAsync();
            return result;
        }
        public async Task<List<SeasonalPlant>> GetSeasonalPlantsByPlantId(int id)
        {
            return await _context.SeasonalPlants.Where(x=>x.PlantId == id).ToListAsync();
        }
    }
}
