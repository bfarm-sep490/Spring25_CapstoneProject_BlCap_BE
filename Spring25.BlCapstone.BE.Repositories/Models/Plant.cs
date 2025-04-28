using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Plant
    {
        [Key]
        public int Id { get; set; }
        public string PlantName { get; set; }
        public string Description { get; set; }
        public float Quantity { get; set; }
        public string Status {  get; set; }
        public float BasePrice { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
        public float AverageEstimatedPerOne { get; set; }
        public int AverageDurationDate { get; set; }
        public float DeltaOne { get; set; }
        public float DeltaTwo { get; set; }
        public float DeltaThree { get; set; }
        public int PreservationDay { get; set; }

        public ICollection<Plan> Plans { get; set; }
        public ICollection<PlantYield> PlantYields { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<SeasonalPlant> SeasonalPlants { get; set; }
    }
}