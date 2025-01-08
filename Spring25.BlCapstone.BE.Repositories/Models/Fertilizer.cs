using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Fertilizer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string NutrientContent { get; set; }
        public string StorageConditions { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public int FarmOwnerId { get; set; }

        public FarmOwner Owner { get; set; }
        public ICollection<TaskFertilizer> TaskFertilizers { get; set; }
    }
}
