using AutoMapper;
using CloudinaryDotNet.Core;
using IO.Ably;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Helper;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Redis;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Auth;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Inspector;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IInspectorService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> SwitchStatus(int id);
        Task<IBusinessResult> RemoveInspector(int id);
        Task<IBusinessResult> CreateInspector(CreateInspector model);
        Task<IBusinessResult> UpdateInspector(int id, UpdateInspector model);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
        Task<IBusinessResult> AddInspectorTokenDevice(int id, string tokenDevice);
        Task<IBusinessResult> GetAllDeviceTokensByInspectorId(int id);
        Task<IBusinessResult> RemoveDeviceTokenByInspectorId(int id);
    }

    public class InspectorService : IInspectorService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RedisManagement _redisManagement;
        private readonly AblyHelper _ablyHelper;

        public InspectorService(IMapper mapper, RedisManagement redisManagement)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
            _redisManagement = redisManagement;
            _ablyHelper = new AblyHelper();
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var list = await _unitOfWork.InspectorRepository.GetInspectors();
                var result = _mapper.Map<List<InspectorModel>>(list);

                if (result.Any())
                {
                    return new BusinessResult(200, "List Inspectors", result);
                }

                return new BusinessResult(404, "Not found any Inspectors", null);
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

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var inss = await _unitOfWork.InspectorRepository.GetInspector(id);
                var res = _mapper.Map<InspectorModel>(inss);
                if (inss != null)
                {
                    return new BusinessResult(200, "Inspector ne", res);
                }

                return new BusinessResult(404, "Not found any Inspectors", null);
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
                var ins = await _unitOfWork.InspectorRepository.GetByIdAsync(id);
                if (ins == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any inspectors!",
                        Data = null
                    };
                }

                var account = await _unitOfWork.AccountRepository.GetByIdAsync(ins.AccountId);

                account.IsActive = !account.IsActive;
                var rs = await _unitOfWork.AccountRepository.UpdateAsync(account);

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

        public async Task<IBusinessResult> RemoveInspector(int id)
        {
            try
            {
                var ins = await _unitOfWork.InspectorRepository.GetByIdAsync(id);
                var account = await _unitOfWork.AccountRepository.GetByIdAsync(ins.AccountId);

                if (ins == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any inspectors!",
                        Data = null
                    };
                }

                var result = await _unitOfWork.InspectorRepository.RemoveAsync(ins);
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

        public async Task<IBusinessResult> CreateInspector(CreateInspector model)
        {
            try
            {
                var account = await _unitOfWork.AccountRepository.GetByEmail(model.Email);
                if (account != null)
                {
                    return new BusinessResult(400, "This email have been used !");
                }

                string password = PasswordHelper.GeneratePassword(model.Name, null);
                var newAccount = _mapper.Map<Account>(CreateInspector);
                newAccount.Role = "Expert";
                newAccount.IsActive = true;
                newAccount.Password = password;
                newAccount.CreatedAt = DateTime.Now;

                var rs = await _unitOfWork.AccountRepository.CreateAsync(newAccount);

                var newIns = _mapper.Map<Inspector>(model);
                newIns.AccountId = newAccount.Id;
                var rsf = await _unitOfWork.InspectorRepository.CreateAsync(newIns);

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
                    Message = "Create Inspector success !",
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

        public async Task<IBusinessResult> UpdateInspector(int id, UpdateInspector model)
        {
            try
            {
                var ins = await _unitOfWork.InspectorRepository.GetByIdAsync(id);
                if (ins == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Inspector not found !",
                        Data = null
                    };
                }

                var account = await _unitOfWork.AccountRepository.GetByIdAsync(ins.AccountId);
                account.Name = model.Name;
                account.Email = model.Email;
                account.UpdatedAt = DateTime.Now;
                await _unitOfWork.AccountRepository.UpdateAsync(account);

                ins.Description = model.Description;
                ins.Address = ins.Address;
                ins.ImageUrl = model.ImageUrl;
                ins.Hotline = model.Hotline;

                var rs = await _unitOfWork.InspectorRepository.UpdateAsync(ins);
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

        public async Task<IBusinessResult> AddInspectorTokenDevice(int id, string tokenDevice)
        {
            var inspector = await _unitOfWork.InspectorRepository.GetByIdAsync(id);
            if (inspector == null)
            {
                return new BusinessResult(404, "Not found any inspectors !");
            }

            var key = $"Inspector-{id}";
            try
            {
                var token = await _ablyHelper.RegisterTokenDevice(tokenDevice, "inspector");

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

        public async Task<IBusinessResult> GetAllDeviceTokensByInspectorId(int id)
        {
            var inspector = await _unitOfWork.InspectorRepository.GetByIdAsync(id);
            if (inspector == null)
            {
                return new BusinessResult(404, "Not found any inspectors !");
            }

            var key = $"Inspector-{id}";
            try
            {
                if (_redisManagement.IsConnected == false) throw new Exception();
                string productListJson = _redisManagement.GetData(key);
                if (productListJson == null || productListJson == "[]")
                {
                    return new BusinessResult(400, "This inspector do not have DeviceToken");
                }
                var result = JsonConvert.DeserializeObject<DeviceTokenModel>(productListJson);
                return new BusinessResult(200, "Inspector device token", result);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, $"Redis is Fail: {ex.Message}");
            }

        }

        public async Task<IBusinessResult> RemoveDeviceTokenByInspectorId(int id)
        {
            var inspector = await _unitOfWork.InspectorRepository.GetByIdAsync(id);
            if (inspector == null)
            {
                return new BusinessResult(404, "Not found any inspectors !");
            }

            var key = $"Inspector-{id}";
            try
            {
                if (_redisManagement.IsConnected == false) throw new Exception();
                string productListJson = _redisManagement.GetData(key);
                if (productListJson == null || productListJson == "[]")
                {
                    return new BusinessResult(400, "This inspector do not have DeviceToken");
                }
                var result = JsonConvert.DeserializeObject<DeviceTokenModel>(productListJson);

                foreach (var item in result.Tokens)
                {
                    await _ablyHelper.RemoveTokenDevice(item);
                }

                _redisManagement.DeleteData(key);
                return new BusinessResult(200, "Removed inspector device token successfully !");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, $"Redis is Fail: {ex.Message}");
            }
        }
    }
}
