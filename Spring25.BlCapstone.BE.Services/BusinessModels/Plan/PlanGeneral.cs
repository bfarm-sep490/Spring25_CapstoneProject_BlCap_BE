using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Plan
{
    public class PlanGeneral
    {
        public int Id { get; set; }
        public string PlanName { get; set; }
        public PlantInformation PlantInformation { get; set; }
        public YieldInformation YieldInformation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float EstimatedProduct { get; set; }
        public string EstimatedUnit { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class PlantInformation
    {
        public int Id { get; set; }
        public string PlantName { get; set; }
    }

    public class YieldInformation
    {
        public int Id { get; set; }
        public string YieldName { get; set; }
        public string AreaUnit { get; set; }
        public double Area { get; set; }
    }
}
