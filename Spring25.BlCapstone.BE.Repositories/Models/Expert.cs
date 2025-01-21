using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Expert
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime? DOB {  get; set; }
        public string? Phone { get; set; }
        public string? Status { get; set; }
        public string? Avatar { get; set; }

        public Account Account { get; set; }
    }
}
