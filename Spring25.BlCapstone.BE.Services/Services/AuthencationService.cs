using Microsoft.Extensions.Configuration;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IAuthencationService
    {
        Task<IBusinessResult> SignIn(string email, string password);
    }
    public class AuthencationService :IAuthencationService 
    {
        private UnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public AuthencationService(IConfiguration configuration)
        {
            _unitOfWork ??= new UnitOfWork();
            _configuration = configuration;
        }
        public async Task<IBusinessResult> SignIn(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
                return new BusinessResult()
                {
                    Data = null,
                    Message = "email is empty.",
                    Status = 400
                };

            if (string.IsNullOrEmpty(password))
                return new BusinessResult()
                {
                    Data = null,
                    Message = "password is empty.",
                    Status = 400
                };

            var user = await _unitOfWork.FarmOwnerRepository.SignIn(email,password);



            if (user == null)
                return new BusinessResult()
                {
                    Data = null,
                    Message = "email/password is incorrect.",
                    Status = 404
                };
                
            JwtSecurityToken accessJwtSecurityToken = JWTHelper.GetToken(_configuration["JWT:Secret"], _configuration["JWT:ValidAudience"], _configuration["JWT:ValidIssuer"],"FarmOwner", user.Id, user.Email, user.Email, 1, null);

            object signInModel = new
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessJwtSecurityToken),
            };
            return new BusinessResult()
            {
                Data = signInModel,
                Status = 200,
                Message = "signing in successfully."
            };
        }


    }
}
