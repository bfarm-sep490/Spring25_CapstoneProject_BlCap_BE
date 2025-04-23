using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Untils
{
    public static class PasswordHelper
    {
        public static string GeneratePassword(string name, DateTime? date, int length = 12)
        {
            string lower = "abcdefghijklmnopqrstuvwxyz";
            string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string digits = "0123456789";
            string special = "!@#$%^&*()-_=+<>?";

            string allCharts = lower + upper + digits + special;
            StringBuilder password = new StringBuilder();
            Random random = new Random();

            if (!string.IsNullOrEmpty(name))
            {
                password.Append(char.ToUpper(name[0]));
            }

            DateTime now = date ?? DateTime.Today;
            password.Append(now.Year % 100);
            password.Append(now.Day);
            password.Append(special[random.Next(special.Length)]);

            while (password.Length < length)
            {
                password.Append(allCharts[random.Next(allCharts.Length)]);
            }

            return new string(password.ToString().OrderBy(x => random.Next()).ToArray());
        }

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                byte[] hash = sha256.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string inputPassword, string hashPassword)
        {
            string hashedInputPassword = HashPassword(inputPassword);

            return hashedInputPassword == hashPassword;
        }
    }
}
