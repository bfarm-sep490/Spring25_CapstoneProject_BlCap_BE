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
        public float Price { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPaid { get; set; }

        public Retailer Retailer { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<OrderPlant> OrderPlants { get; set; }
        public ICollection<OrderPlan> OrderPlans { get; set; }
    }
}
