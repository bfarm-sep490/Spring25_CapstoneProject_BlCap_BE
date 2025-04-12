using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class ConfigurationSystem
    {
        [Key]
        public int Id { get; set; }
        public float DepositPercent { get; set; }
        public string Status { get; set; }
    }
}
