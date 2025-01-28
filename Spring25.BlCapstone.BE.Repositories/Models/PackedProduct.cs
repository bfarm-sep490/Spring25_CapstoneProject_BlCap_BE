using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class PackedProduct
    {
        [Key]
        public int Id { get; set; }
        public int PlanId { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
        public string Status { get; set; }
        public float Price { get; set; }
        public string QRHash { get; set; }

        public Plan Plan { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
