using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class ShipmentImage
    {
        [Key]
        public int Id { get; set; }
        public int ShipmentTripId { get; set; }
        public string Url { get; set; }

        public ShipmentTrip ShipmentTrip { get; set; }
    }
}
