using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Payment
{
    public class CreatePaymentRemainingRequest
    {
        public int OrderId { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
    }
}
