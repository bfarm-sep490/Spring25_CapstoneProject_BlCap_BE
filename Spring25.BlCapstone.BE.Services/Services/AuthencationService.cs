using AutoMapper;
using IO.Ably;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Redis;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Auth;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plant;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IAuthencationService
    {
        Task<IBusinessResult> SignIn(string email, string password);
        //Task<IBusinessResult> SignInForFarmer(string email, string password);
        Task<IBusinessResult> GetAccountInfoById(int id);
        Task<IBusinessResult> ChangePassword(int id, AccountChangePassword model);
        Task<IBusinessResult> GetAllAccount();
        Task<IBusinessResult> AddFarmerDevice(int id,string token);
        Task<IBusinessResult> GetAllDeviceTokensbyFarmerId(int id);
    }
    public class AuthencationService : IAuthencationService
    {
        private UnitOfWork _unitOfWork;
        private IConfiguration _configuration;
        private IMapper _mapper;
        private readonly RedisManagement _redisManagerment;

        public AuthencationService(IConfiguration configuration,IMapper mapper,RedisManagement redisManagement)
        {
            _unitOfWork ??= new UnitOfWork();
            _configuration = configuration;
            _mapper = mapper;
            _redisManagerment = redisManagement;
        }

        public async Task<IBusinessResult> GetAccountInfoById(int id)
        {
            var obj = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            if (obj == null) { return new BusinessResult(400,"Not found this account"); } 
            var result = _mapper.Map<AccountModel>(obj);
            switch (obj.Role.ToLower()) 
            {
                case "farmer":
                    var farmer = await _unitOfWork.FarmerRepository.GetFarmerbyAccountId(id);
                    result.Infomation = _mapper.Map<InfomationModel>(farmer);
                break;

                case "retailer":
                    var retailer = await _unitOfWork.RetailerRepository.GetRetailerbyAccountId(id);
                    result.Infomation = _mapper.Map<InfomationModel>(retailer);
                break;

                case "inspector":
                    var inspector = await _unitOfWork.InspectorRepository.GetInspectorbyAccountId(id);
                    result.Infomation = _mapper.Map<InfomationModel>(inspector);
                    break;

                case "expert":
                    var expert = await _unitOfWork.ExpertRepository.GetExpertbyAccountId(id);
                    result.Infomation = _mapper.Map<InfomationModel>(expert);
                    break;

                case "farm owner":
                    return new BusinessResult(200, "You are crazy??? U are a farm owner... OKE ????", null);
                   
                default:
                    return new BusinessResult(400,"Do not get your role",obj.Role);
            }
            return new BusinessResult(200, "Get information by account id", result);

        }

        public async Task<IBusinessResult> GetAllAccount()
        {
           var list = await _unitOfWork.AccountRepository.GetAllAsync();
           return new BusinessResult(200, "List Accounts", list);
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
            var user = await _unitOfWork.AccountRepository.SignIn(email,password);
            if (user == null)
                return new BusinessResult()
                {
                    Data = null,
                    Message = "email/password is incorrect.",
                    Status = 404
                };
            else if( user.IsActive == false)
            {
                return new BusinessResult()
                {
                    Data = null,
                    Message = "this account is not Active",
                    Status = 400
                };
            }

            var obj = await _unitOfWork.AccountRepository.GetByIdAsync(user.Id);
            int id;
            string ava;
            switch (obj.Role.ToLower())
            {
                case "farmer":
                    var farmer = await _unitOfWork.FarmerRepository.GetFarmerbyAccountId(user.Id);
                    id = farmer.Id;
                    ava = farmer.Avatar == null ? null : farmer.Avatar;
                    break;

                case "retailer":
                    var retailer = await _unitOfWork.RetailerRepository.GetRetailerbyAccountId(user.Id);
                    id = retailer.Id;
                    ava = retailer.Avatar == null ? null : retailer.Avatar;
                    break;

                case "inspector":
                    var inspector = await _unitOfWork.InspectorRepository.GetInspectorbyAccountId(user.Id);
                    id = inspector.Id;
                    ava = inspector.ImageUrl == null ? null : inspector.ImageUrl;
                    break;

                case "expert":
                    var expert = await _unitOfWork.ExpertRepository.GetExpertbyAccountId(user.Id);
                    id = expert.Id;
                    ava = expert.Avatar == null ? null : expert.Avatar;
                    break;

                case "farm owner":
                    id = user.Id;
                    ava = "https://i.pravatar.cc/150";
                    break;

                default:
                    return new BusinessResult(400, "Do not get your role", obj.Role);
            }

            var signInModel = GenarateToken(id, user.Role, user.Name, user.Email, ava);
            return new BusinessResult()
            {
                Data = signInModel,
                Status = 200,
                Message = "signing in successfully."
            };
        }

        private object GenarateToken(int Id, string Role, string Name, string Email, string avatar)
        {
            JwtSecurityToken accessJwtSecurityToken = JWTHelper.GetToken(_configuration["JWT:Secret"], _configuration["JWT:ValidAudience"], _configuration["JWT:ValidIssuer"], Role, Id, Name, Email, 1, null, avatar);

            object signInModel = new
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessJwtSecurityToken)
            };
            return signInModel;
        }

        public async Task<IBusinessResult> ChangePassword(int id, AccountChangePassword model)
        {
            try
            {
                var user = await _unitOfWork.AccountRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return new BusinessResult { Status = 404, Message = "Not found any user", Data = null };
                }

                if (model.OldPassword != user.Password)
                {
                    return new BusinessResult { Status = 400, Message = "Your old password is incorrect !" };
                }

                user.Password = model.NewPassword;
                var rs = await _unitOfWork.AccountRepository.UpdateAsync(user);
                if (rs > 0)
                {
                    return new BusinessResult { Status = 200, Message = "Change password successfully!", Data = null };
                } 
                else
                {
                    return new BusinessResult { Status = 500, Message = "Change password failed!", Data = null };
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }

        public async Task<IBusinessResult> AddFarmerDevice(int id, string token)
        {
            var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(id);
            if (farmer == null) { return new BusinessResult(400, "Not found this farmer", null); }
            var key = $"DeviceTokens{id}";
            string productListJson = _redisManagerment.GetData(key);
            var result = new DeviceTokenModel();
            if (productListJson == null || productListJson == "[]")
            {     
                result.Id = id;
                result.Tokens = new List<string>();
            }
            else
            {
                result = JsonConvert.DeserializeObject<DeviceTokenModel>(productListJson);
            }  
            result.Tokens.Add(token);              
            productListJson = JsonConvert.SerializeObject(result);
            _redisManagerment.SetData(key, productListJson);
            return new BusinessResult(200, "Set Device Token successfully", result);

        }
        public async Task<IBusinessResult> GetAllDeviceTokensbyFarmerId(int id)
        {
            var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(id);
            if (farmer == null) { return new BusinessResult(400, "Not found this farmer", null); }
            var key = $"DeviceTokens{id}";
            string productListJson = _redisManagerment.GetData(key);
            if (productListJson == null || productListJson == "[]")
            {
                return new BusinessResult(400, "This Farmer do not have DeviceToken");
            }
            var result = JsonConvert.DeserializeObject<DeviceTokenModel>(productListJson);
            return new BusinessResult(200, "Farmer device token", result);
        }
    }
}
