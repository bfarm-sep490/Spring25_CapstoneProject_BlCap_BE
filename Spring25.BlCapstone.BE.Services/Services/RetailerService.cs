using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Helper;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Redis;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Auth;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Retailer;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IRetailerService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> SwitchStatus(int id);
        Task<IBusinessResult> RemoveRetailer(int id);
        Task<IBusinessResult> CreateRetailer(CreateRetailer model);
        Task<IBusinessResult> UpdateRetailer(int id, CreateRetailer model);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
        Task<IBusinessResult> AddRetailerTokenDevice(int id, string tokenDevice);
        Task<IBusinessResult> GetAllDeviceTokensByRetailerId(int id);
        Task<IBusinessResult> RemoveDeviceTokenByRetailerId(int id);
    }

    public class RetailerService : IRetailerService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RedisManagement _redisManagement;
        private readonly AblyHelper _ablyHelper;

        public RetailerService(IMapper mapper, RedisManagement redisManagement)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
            _redisManagement = redisManagement;
            _ablyHelper = new AblyHelper();
        }

        public async Task<IBusinessResult> GetAll()
        {
            var list = await _unitOfWork.RetailerRepository.GetRetailers();
            var result = _mapper.Map<List<RetailerModels>>(list);

            if (list.Count > 0)
            {
                return new BusinessResult(200, "List Retailers", result);
            }
            else
            {
                return new BusinessResult(404, "Not Found Any Retailers", null);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var users = await _unitOfWork.RetailerRepository.GetRetailers();
                var result = _mapper.Map<List<RetailerModels>>(users);

                if (!(users.Count > 0))
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any Retailers",
                        Data = null
                    };
                }

                return new BusinessResult
                {
                    Status = 200,
                    Message = "Read successfull !",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BusinessResult
                {
                    Status = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<IBusinessResult> SwitchStatus(int id)
        {
            try
            {
                var retailers = await _unitOfWork.RetailerRepository.GetByIdAsync(id);
                if (retailers == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found!",
                        Data = null
                    };
                }

                var updatedRetailer = await _unitOfWork.AccountRepository.GetByIdAsync(retailers.AccountId);
                updatedRetailer.IsActive = !updatedRetailer.IsActive;
                var rs = await _unitOfWork.AccountRepository.UpdateAsync(updatedRetailer);

                if (rs > 0)
                {
                    return new BusinessResult
                    {
                        Status = 200,
                        Message = "Switch success",
                        Data = null
                    };
                }
                else
                {
                    return new BusinessResult
                    {
                        Status = 500,
                        Message = "Switch failed!",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult
                {
                    Status = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<IBusinessResult> RemoveRetailer(int id)
        {
            try
            {
                var retailer = await _unitOfWork.RetailerRepository.GetByIdAsync(id);
                var account = await _unitOfWork.AccountRepository.GetByIdAsync(retailer.AccountId);

                if (retailer == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any retailers!",
                        Data = null
                    };
                }

                var result = await _unitOfWork.RetailerRepository.RemoveAsync(retailer);
                var rs = await _unitOfWork.AccountRepository.RemoveAsync(account);

                if (result && rs)
                {
                    return new BusinessResult
                    {
                        Status = 200,
                        Message = "Remove successfull !",
                        Data = null
                    };
                }
                else
                {
                    return new BusinessResult
                    {
                        Status = 500,
                        Message = "Remove failed !",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult
                {
                    Status = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<IBusinessResult> CreateRetailer(CreateRetailer model)
        {
            try
            {
                string password = PasswordHelper.GeneratePassword(model.Name, model.DOB);
                var newAccount = _mapper.Map<Account>(model);
                newAccount.Password = password;
                newAccount.IsActive = true;
                newAccount.Password = password;
                newAccount.CreatedAt = DateTime.Now;

                var rs = await _unitOfWork.AccountRepository.CreateAsync(newAccount);

                var newRetailer = _mapper.Map<Retailer>(model);
                newRetailer.AccountId = newAccount.Id;
                var rsf = await _unitOfWork.RetailerRepository.CreateAsync(newRetailer);

                var body = EmailHelper.GetEmailBody("RegisterAccount.html", new Dictionary<string, string>
                {
                    { "{{UserName}}", model.Name },
                    { "{{Email}}", model.Email },
                    { "{{ResetPasswordLink}}", "https://bfarmx.space/reset-password" }
                });

                await EmailHelper.SendMail(model.Email, "Chào mừng bạn đến với BFARMX - Blockchain FarmXperience!", model.Name, body);

                if (rsf == null)
                {
                    return new BusinessResult
                    {
                        Status = 500,
                        Message = "Create failed !",
                        Data = null

                    };
                };

                return new BusinessResult
                {
                    Status = 200,
                    Message = "Create retailer success !",
                    Data = rsf
                };

            }
            catch (Exception ex)
            {
                return new BusinessResult
                {
                    Status = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<IBusinessResult> UpdateRetailer(int id, CreateRetailer model)
        {
            try
            {
                var retailer = await _unitOfWork.RetailerRepository.GetByIdAsync(id);
                if (retailer == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Retailer not found !",
                        Data = null
                    };
                }

                var account = await _unitOfWork.AccountRepository.GetByIdAsync(retailer.AccountId);
                account.Name = model.Name;
                account.Email = model.Email;
                account.UpdatedAt = DateTime.Now;
                await _unitOfWork.AccountRepository.UpdateAsync(account);

                _mapper.Map(model, retailer);
                var rs = await _unitOfWork.RetailerRepository.UpdateAsync(retailer);
                if (rs > 0)
                {
                    return new BusinessResult
                    {
                        Status = 200,
                        Message = "Update successfull",
                        Data = null
                    };
                }
                else
                {
                    return new BusinessResult
                    {
                        Status = 500,
                        Message = "Update failed",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult
                {
                    Status = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<IBusinessResult> UploadImage(List<IFormFile> file)
        {
            try
            {
                var image = await CloudinaryHelper.UploadMultipleImages(file);
                var url = image.Select(x => x.Url).ToList();

                return new BusinessResult
                {
                    Status = 200,
                    Message = "Upload success !",
                    Data = url
                };
            }
            catch (Exception ex)
            {
                return new BusinessResult
                {
                    Status = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<IBusinessResult> AddRetailerTokenDevice(int id, string tokenDevice)
        {
            var retailer = await _unitOfWork.RetailerRepository.GetByIdAsync(id);
            if (retailer == null)
            {
                return new BusinessResult(404, "Not found any retailers !");
            }

            var key = $"Retailer-{id}";
            try
            {
                var token = await _ablyHelper.RegisterTokenDevice(tokenDevice, "retailer");

                if (_redisManagement.IsConnected == false) throw new Exception();
                string productListJson = _redisManagement.GetData(key);
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
                _redisManagement.SetData(key, productListJson);
                return new BusinessResult(200, "Set Device Token successfully", result);
            }
            catch
            {
                return new BusinessResult(500, "Redis is fail");
            }
        }

        public async Task<IBusinessResult> GetAllDeviceTokensByRetailerId(int id)
        {
            var retailer = await _unitOfWork.RetailerRepository.GetByIdAsync(id);
            if (retailer == null)
            {
                return new BusinessResult(404, "Not found any retailers !");
            }

            var key = $"Retailer-{id}";
            try
            {
                if (_redisManagement.IsConnected == false) throw new Exception();
                string productListJson = _redisManagement.GetData(key);
                if (productListJson == null || productListJson == "[]")
                {
                    return new BusinessResult(400, "This retailer do not have DeviceToken");
                }
                var result = JsonConvert.DeserializeObject<DeviceTokenModel>(productListJson);
                return new BusinessResult(200, "Retailer device token", result);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, $"Redis is Fail: {ex.Message}");
            }

        }

        public async Task<IBusinessResult> RemoveDeviceTokenByRetailerId(int id)
        {
            var retailer = await _unitOfWork.RetailerRepository.GetByIdAsync(id);
            if (retailer == null)
            {
                return new BusinessResult(404, "Not found any retailers !");
            }

            var key = $"Retailer-{id}";
            try
            {
                if (_redisManagement.IsConnected == false) throw new Exception();
                string productListJson = _redisManagement.GetData(key);
                if (productListJson == null || productListJson == "[]")
                {
                    return new BusinessResult(400, "This retailer do not have DeviceToken");
                }
                var result = JsonConvert.DeserializeObject<DeviceTokenModel>(productListJson);

                foreach (var item in result.Tokens)
                {
                    await _ablyHelper.RemoveTokenDevice(item);
                }

                _redisManagement.DeleteData(key);
                return new BusinessResult(200, "Removed retailer device token successfully !");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, $"Redis is Fail: {ex.Message}");
            }
        }
    }
}
