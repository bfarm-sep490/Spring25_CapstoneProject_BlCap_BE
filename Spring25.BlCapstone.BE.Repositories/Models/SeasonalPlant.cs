using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class SeasonalPlant
    {
        [Key]
        public int Id { get; set; }
        public int PlantId { get; set; }
        public string Description { get; set; }
        public string SeasonType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float EstimatedPerOne { get; set; }
        public int DurationDays { get; set; }
        public string TemplatePlan { get; set; }


        public Plant Plant { get; set; }
    }
}
