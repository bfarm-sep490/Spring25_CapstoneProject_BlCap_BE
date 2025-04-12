using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class OrderPlan
    {
        public int OrderId {  get; set; }
        public int PlanId { get; set; }
        public float Quantity { get; set; }

        public Order Order { get; set; }
        public Plan Plan { get; set; }
    }
}
