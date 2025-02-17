using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class FertilizerRange
    {
        public int FertilizerId { get; set; }
        public int PlantId { get; set; }
        public string Unit { get; set; }
        public float Maximum { get; set; }
        public float Minimum { get; set; }

        public Fertilizer Fertilizer { get; set; }
        public Plant Plant { get; set; }
    }
}
