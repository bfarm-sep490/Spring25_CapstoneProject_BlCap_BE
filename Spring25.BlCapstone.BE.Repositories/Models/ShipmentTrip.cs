using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class ShipmentTrip
    {
        [Key]
        public int Id { get; set; }
        public int DriverId { get; set; }
        public int OrderId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Status { get; set; }

        public Driver Driver { get; set; }
        public Order Order { get; set; }
        public ICollection<ShipmentImage> ShipmentImages { get; set; }
    }
}
