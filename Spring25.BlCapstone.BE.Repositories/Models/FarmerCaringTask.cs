using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class FarmerCaringTask
    {
        public int FarmerId { get; set; }
        public int TaskId { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
        public DateTime ExpiredDate { get; set; }
      
        public Farmer Farmer { get; set; }
        public CaringTask CaringTask { get; set; }
    }
}
