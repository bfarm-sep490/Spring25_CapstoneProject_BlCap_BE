using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Yield
    {
        [Key]
        public int Id { get; set; }
        public string YieldName { get; set; }
        public string AreaUnit {  get; set; }
        public float Area {  get; set; }
        public string Description { get; set; }
        public string Type {  get; set; }
        public string Status { get; set; }

        public ICollection<Device> Devices { get; set; }
        public ICollection<Plan> Plans { get; set; }
        public ICollection<DataEnvironment> DataEnvironments { get; set; }
        public ICollection<PlantYield> PlantYields { get; set; }
    }
}
