using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class DataEnvironment
    {
        [Key]
        public int Id { get; set; }
        public int YieldId { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float YieldHumidity { get; set; }
        public DateTime Date { get; set; }

        public Yield Yield { get; set; }
    }
}
