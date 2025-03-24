using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class PlanTransaction
    {
        [Key]
        public int Id { get; set; }
        public int PlanId { get; set; }
        public string Url { get; set; }
        public float? Price { get; set; }
        public string Type { get; set; }

        public Plan Plan { get; set; }
    }
}
