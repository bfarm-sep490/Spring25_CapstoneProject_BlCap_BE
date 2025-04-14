using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class PackagingProduct
    {
        [Key]
        public int Id { get; set; }
        public int PackagingTaskId { get; set; }
        public int HarvestingTaskId { get; set; }
        public float QuantityPerPack { get; set; }
        public int PackQuantity { get; set; }
        public string QRCode { get; set; }
        public string Status { get; set; }

        public PackagingTask PackagingTask { get; set; }
        public HarvestingTask HarvestingTask { get; set; }
        public ICollection<ProductPickupBatch> ProductPickupBatches { get; set; }
    }
}
