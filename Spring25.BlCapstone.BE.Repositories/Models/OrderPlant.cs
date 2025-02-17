using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class OrderPlant
    {
        public int OrderId { get; set; }
        public int PlantId { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }

        public Order Order { get; set; }
        public Plant Plant { get; set; }
    }
}
