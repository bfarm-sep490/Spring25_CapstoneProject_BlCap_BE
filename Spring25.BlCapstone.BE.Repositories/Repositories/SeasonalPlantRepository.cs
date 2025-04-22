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

        public async Task<List<SeasonalPlant>> GetAllSeasonalPlants(int? plantId = null, string? seasonName = null, DateTime? start = null, DateTime? end = null)
        {
            var query = _context.SeasonalPlants.AsQueryable();

            if (plantId.HasValue)
            {
                query = query.Where(p => p.PlantId == plantId);
            }

            if (!string.IsNullOrEmpty(seasonName))
            {
                query = query.Where(p => p.SeasonType.ToLower().Trim().Equals(seasonName.Trim().ToLower()));
            }

            if (start.HasValue && !end.HasValue)
            {
                var firstMatch = await query
                                        .Where(p => p.StartDate >= start.Value)
                                        .OrderBy(p => p.StartDate)
                                        .FirstOrDefaultAsync();

                return firstMatch != null ? new List<SeasonalPlant> { firstMatch } : new List<SeasonalPlant>();
            }
            else if (end.HasValue && !start.HasValue)
            {
                var firstMatch = await query
                                        .Where(p => p.EndDate <= end.Value)
                                        .OrderByDescending(p => p.EndDate) 
                                        .FirstOrDefaultAsync();

                return firstMatch != null ? new List<SeasonalPlant> { firstMatch } : new List<SeasonalPlant>();
            }
            else if (start.HasValue && end.HasValue)
            {
                var seasonalPlants = await query.ToListAsync();

                var maxOverlap = seasonalPlants
                    .Select(p => new
                    {
                        Plant = p,
                        OverlapDays = (Math.Min(p.EndDate.Ticks, end.Value.Ticks) - Math.Max(p.StartDate.Ticks, start.Value.Ticks)) / TimeSpan.TicksPerDay
                    })
                    .Where(x => x.OverlapDays > 0)
                    .OrderByDescending(x => x.OverlapDays)
                    .FirstOrDefault();

                return maxOverlap != null ? new List<SeasonalPlant> { maxOverlap.Plant } : new List<SeasonalPlant>();
            }

            return await query.ToListAsync();
        }
    }
}
