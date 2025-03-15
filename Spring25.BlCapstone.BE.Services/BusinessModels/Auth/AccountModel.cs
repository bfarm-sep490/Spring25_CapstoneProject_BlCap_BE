using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Auth
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public InfomationModel Infomation { get; set; }
    }
    public class InfomationModel
    {
        public int Id { get; set; }
        public DateTime? DOB { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
    }
}
