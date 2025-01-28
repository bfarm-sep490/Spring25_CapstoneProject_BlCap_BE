using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int PackedProductId { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
        public float Price { get; set; }

        public PackedProduct PackedProduct { get; set; }
        public Order Order { get; set; }
    }
}
