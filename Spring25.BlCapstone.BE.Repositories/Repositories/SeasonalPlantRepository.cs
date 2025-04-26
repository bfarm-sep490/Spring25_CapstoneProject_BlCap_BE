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

        public async Task<List<SeasonalPlant>> GetAllSeasonalPlants(int? plantId = null, string? seasonName = null, DateTime? start = null)
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

            if (start.HasValue)
            {
                var end = start.Value.AddMonths(2);

                var seasonalPlants = await query.ToListAsync();

                var maxOverlap = seasonalPlants
                    .Select(p => new
                    {
                        Plant = p,
                        OverlapDays = CalculateOverlapDays(start.Value, end, p.StartDate, p.EndDate)
                    })
                    .Where(x => x.OverlapDays > 0)
                    .OrderByDescending(x => x.OverlapDays)
                    .FirstOrDefault();

                return maxOverlap != null ? new List<SeasonalPlant> { maxOverlap.Plant } : new List<SeasonalPlant>();
            }

            return await query.ToListAsync();
        }

        private int CalculateOverlapDays(DateTime searchStart, DateTime searchEnd, DateTime plantStart, DateTime plantEnd)
        {
            var searchStartFake = new DateTime(1, searchStart.Month, searchStart.Day);
            var searchEndFake = new DateTime(1, searchEnd.Month, searchEnd.Day);

            var plantStartFake = new DateTime(1, plantStart.Month, plantStart.Day);
            var plantEndFake = new DateTime(1, plantEnd.Month, plantEnd.Day);

            bool searchCrossYear = searchStartFake > searchEndFake;
            bool plantCrossYear = plantStartFake > plantEndFake;

            if (!searchCrossYear && !plantCrossYear)
            {
                var overlapStart = searchStartFake > plantStartFake ? searchStartFake : plantStartFake;
                var overlapEnd = searchEndFake < plantEndFake ? searchEndFake : plantEndFake;

                var overlap = (overlapEnd - overlapStart).Days;
                return overlap > 0 ? overlap : 0;
            }

            if (searchCrossYear)
            {
                var endOfYear = new DateTime(1, 12, 31);
                var startOfYear = new DateTime(1, 1, 1);

                int overlap1 = CalculateOverlapDaysInternal(searchStartFake, endOfYear, plantStartFake, plantEndFake);
                int overlap2 = CalculateOverlapDaysInternal(startOfYear, searchEndFake, plantStartFake, plantEndFake);

                return overlap1 + overlap2;
            }

            if (plantCrossYear)
            {
                var endOfYear = new DateTime(1, 12, 31);
                var startOfYear = new DateTime(1, 1, 1);

                int overlap1 = CalculateOverlapDaysInternal(searchStartFake, searchEndFake, plantStartFake, endOfYear);
                int overlap2 = CalculateOverlapDaysInternal(searchStartFake, searchEndFake, startOfYear, plantEndFake);

                return overlap1 + overlap2;
            }

            return 0;
        }

        private int CalculateOverlapDaysInternal(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            var overlapStart = start1 > start2 ? start1 : start2;
            var overlapEnd = end1 < end2 ? end1 : end2;

            var overlap = (overlapEnd - overlapStart).Days;
            return overlap > 0 ? overlap : 0;
        }

    }
}