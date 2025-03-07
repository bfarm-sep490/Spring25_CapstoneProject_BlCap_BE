using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Fertilizer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string Unit { get; set; }
        public float AvailableQuantity { get; set; }
        public float TotalQuantity { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        [JsonIgnore]
        public ICollection<CaringFertilizer> CaringFertilizers { get; set; }
        public ICollection<FertilizerRange> FertilizerRanges { get; set; }
    }
}
