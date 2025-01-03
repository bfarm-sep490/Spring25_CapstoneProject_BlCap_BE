using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Field
    {
        [Key]
        public int Id { get; set; }
        public double Area { get; set; }
        public int FarmOwnerId { get; set; }
        public string Description { get; set; }
        public double Wide { get; set; }
        public double Long { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public FarmOwner FarmOwner { get; set; }
        public ICollection<ImageField> ImageFields { get; set; }
    }
}
