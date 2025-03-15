using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Inspector
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public string? Hotline { get; set; }

        public Account Account { get; set; }
        public ICollection<InspectingForm> InspectingForms { get; set; }
    }
}
