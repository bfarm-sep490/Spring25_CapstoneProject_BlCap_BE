using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int RetailerId { get; set; }
        public int PlantId { get; set; }
        public int? PlanId { get; set; }
        public int PackagingTypeId { get; set; }
        public float DepositPrice { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime EstimatedPickupDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public Retailer Retailer { get; set; }
        public Plan Plan { get; set; }
        public PackagingType PackagingType { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<PackagingProduct> PackagingProducts { get; set; }
    }
}
