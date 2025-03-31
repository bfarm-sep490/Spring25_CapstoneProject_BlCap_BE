using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int QuantityOfPacks { get; set; }
        public string Status { get; set; }

        public Order Order { get; set; }
        public PackagingProduct PackagingProduct { get; set; }
    }
}
