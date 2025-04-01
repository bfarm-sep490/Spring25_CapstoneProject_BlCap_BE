using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks
{
   public class GenerateTasksModel
   {
        public int PlanId {  get; set; }
        public List<HarvestingTaskGenerate> HavestingTasks { get; set; } = new List<HarvestingTaskGenerate>();
        public List<CaringTaskGenerate> CaringTasks { get; set; } = new List<CaringTaskGenerate>();
        public List<PackagingTaskGenerate> PackingTasks { get; set; } = new List<PackagingTaskGenerate>();
   }
    public class HarvestingTaskGenerate
    {
        public int HarvestingTaskId { get; set; }
        public int FarmerId { get; set; }
        public string Avatar { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpiredDate { get; set; }

    }
    public class CaringTaskGenerate
    {
        public int CaringTaskId { get; set; }
        public int FarmerId { get; set; }
        public string Avatar { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpiredDate { get; set; }

    }
    public class PackagingTaskGenerate
    {
        public int PackagingTaskId { get; set; }
        public int FarmerId { get; set; }
        public string Avatar { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
