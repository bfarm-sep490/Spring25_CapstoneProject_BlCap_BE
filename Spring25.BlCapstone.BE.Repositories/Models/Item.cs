using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }

        public ICollection<CaringItem> CaringItems { get; set; }
        public ICollection<HarvestingItem> HarvestingItems { get; set; }
        public ICollection<PackagingItem> PackagingItems { get; set; }
    }
}
