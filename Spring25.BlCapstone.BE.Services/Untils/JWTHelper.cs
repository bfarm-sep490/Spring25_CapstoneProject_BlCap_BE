using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Untils
{
    public class JWTHelper
    {
        public static JwtSecurityToken GetToken(string jwtKey, string audience, string issuer, string role, int? id, string name, string email, int day, Guid? guid)
        {

            if (string.IsNullOrEmpty(jwtKey)) throw new ArgumentNullException("not found jwt key");
            if (string.IsNullOrEmpty(audience)) throw new ArgumentNullException("not found audience");
            if (string.IsNullOrEmpty(issuer)) throw new ArgumentNullException("not found issuer");

            List<Claim> authClaims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, name),
                 new Claim(ClaimTypes.Email, email),
                 new Claim(ClaimTypes.Role, role),
            };

            if (id.HasValue) authClaims.Add(new Claim(ClaimTypes.NameIdentifier, id.Value.ToString()));
            else if (guid.HasValue) authClaims.Add(new Claim(ClaimTypes.NameIdentifier, guid.Value.ToString()));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var token = new JwtSecurityToken(

                issuer: issuer,
                audience: audience,
                claims: authClaims,
                expires: DateTime.Now.AddDays(day),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        public static ClaimsPrincipal DecodeToken(string jwtKey, string audience, string issuer, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (string.IsNullOrEmpty(jwtKey)) throw new ArgumentNullException("not found jwt key");
            if (string.IsNullOrEmpty(audience)) throw new ArgumentNullException("not found audience");
            if (string.IsNullOrEmpty(issuer)) throw new ArgumentNullException("not found issuer");

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var claimsIdentity = new ClaimsIdentity(jwtToken.Claims);

                return new ClaimsPrincipal(claimsIdentity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return null;
            }
        }
    }
}
