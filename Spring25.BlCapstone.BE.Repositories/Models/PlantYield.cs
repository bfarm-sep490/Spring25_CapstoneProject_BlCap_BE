using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class PlantYield
    {
        public int YieldId { get; set; }
        public int PlantId { get; set; }

        public Yield Yield { get; set; }
        public Plant Plant { get; set; }
    }
}
