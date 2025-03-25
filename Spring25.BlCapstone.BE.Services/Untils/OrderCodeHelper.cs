using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Untils
{
    public static class OrderCodeHelper
    {
        public static int GenerateOrderCodeHash(int orderId, int planId)
        {
            string input = $"{orderId}-{planId}-{DateTime.Now:yyyyMMddHHmmss}";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                int orderCode = BitConverter.ToInt32(hashBytes, 0);

                return Math.Abs(orderCode);
            }
        }
    }
}
