using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Specialization
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<FarmerSpecialization> FarmerSpecializations { get; set; }
    }
}
