using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class FarmOwner
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password {  get; set; }
        public virtual ICollection<Pesticide> Pesticides { get; set; }
        public virtual ICollection<Fertilizer> Fertilizers { get; set; }

    }
}
