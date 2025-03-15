using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Retailer
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateTime? DOB { get; set; }
        public string? Avatar { get; set; }

        public Account Account { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<NotificationRetailer> NotificationRetailers { get; set; }
    }
}
