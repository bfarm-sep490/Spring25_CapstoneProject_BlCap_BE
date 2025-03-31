using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Payment
{
    public class CreatePaymentRemainingRequest
    {
        public List<Product> Product { get; set; }
        public int OrderId { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public int QuantityOfPacks { get; set; }
    }
}
