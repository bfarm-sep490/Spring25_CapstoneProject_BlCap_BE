using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class YieldPlan
    {
        public int YieldId { get; set; }
        public int PlanId { get; set; }
        public bool IsCompleted { get; set; }
        public string DataEnvironmentUrl { get; set; }
        public DateTime CompletedDate { get; set; }

        public Yield Yield { get; set; }
        public Plan Plan { get; set; }
    }
}
