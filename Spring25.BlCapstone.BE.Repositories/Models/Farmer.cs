using Azure.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Farmer
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string AvatarImage { get; set; }
        public string CINumber { get; set; }
        public string CIBeforeImage { get; set; }
        public string CIAfterImage { get; set; }
        public string DeviceAddress { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
    }
}
