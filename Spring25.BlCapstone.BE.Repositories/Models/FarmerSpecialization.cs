using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class FarmerSpecialization
    {
        public int FarmerId { get; set; }
        public int SpecializationId { get; set; }

        public Farmer Farmer { get; set; }
        public Specialization Specialization { get; set; }
    }
}
