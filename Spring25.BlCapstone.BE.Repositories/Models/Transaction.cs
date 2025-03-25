using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public long OrderCode { get; set; }
        public string Content { get; set; }
        public float Price { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime? PaymentDate { get; set; }

        public Order Order { get; set; }
    }
}
