using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class TaskPesticide
    {
        public int TaskId { get; set; }
        public int PesticideId { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public string Status { get; set; }

        public Pesticide Pesticide { get; set; }
        public Task Task { get; set; }
    }
}
