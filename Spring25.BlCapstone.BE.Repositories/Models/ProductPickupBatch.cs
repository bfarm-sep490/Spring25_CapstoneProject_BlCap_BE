using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class ProductPickupBatch
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public float Quantity { get; set; }
        public float Price { get; set; }
        public DateTime CreatedDate { get; set; }

        public PackagingProduct PackagingProduct { get; set; }
    }
}
