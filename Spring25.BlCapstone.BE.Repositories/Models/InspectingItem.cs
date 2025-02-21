using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class InspectingItem
    {
        [Key]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int TaskId { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }

        public Item Item { get; set; }
        public InspectingForm InspectingForm { get; set; }
    }
}
