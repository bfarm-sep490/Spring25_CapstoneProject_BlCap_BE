using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class FarmerPerformance
    {
        [Key]
        [ForeignKey("Farmer")]
        public int Id { get; set; }
        public int CompletedTasks { get; set; }
        public int IncompleteTasks { get; set; }
        public double? PerformanceScore { get; set; } 

        public Farmer Farmer { get; set; }
    }
}
