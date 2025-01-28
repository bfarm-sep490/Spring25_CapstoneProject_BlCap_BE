using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class ProductionFertilizer
    {
        public int FertilizerId { get; set; }
        public int TaskId { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }

        public Fertilizer Fertilizer { get; set; }
        public ProductionTask ProductionTask { get; set; }
    }
}
