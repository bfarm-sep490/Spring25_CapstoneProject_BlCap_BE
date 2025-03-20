using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Helper;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Redis;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Auth;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IFarmerService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> SwitchStatus(int id);
        Task<IBusinessResult> RemoveFarmer(int id);
        Task<IBusinessResult> CreateFarmer(CreateFarmer model);
        Task<IBusinessResult> UpdateFarmer(int id, CreateFarmer model);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
        Task<IBusinessResult> AddFarmerTokenDevice(int id, string tokenDevice);
        Task<IBusinessResult> GetAllDeviceTokensByFarmerId(int id);
        Task<IBusinessResult> RemoveDeviceTokenByFarmerId(int id);
        Task<IBusinessResult> GetFarmerCalendar(int id, DateTime? startDate, DateTime? endDate);
    }

    public class FarmerService : IFarmerService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RedisManagement _redisManagement;
        private readonly AblyHelper _ablyHelper;

        public FarmerService(IMapper mapper, RedisManagement redisManagement)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
            _redisManagement = redisManagement;
            _ablyHelper = new AblyHelper();
        }

        public async Task<IBusinessResult> GetAll()
        {
            var list = await _unitOfWork.FarmerRepository.GetFarmers();
            var result = _mapper.Map<List<FarmerModel>>(list);

            if (list.Count > 0)
            {
                return new BusinessResult(200, "List Farmers", result);
            }
            else
            {
                return new BusinessResult(404, "Not Found Any Farmers", null);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var users = await _unitOfWork.FarmerRepository.GetFarmers();
                var result = _mapper.Map<List<FarmerModel>>(users);

                if (users.Count <= 0)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any Farmers",
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
                var farmers = await _unitOfWork.FarmerRepository.GetByIdAsync(id);

                if (farmers == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found!",
                        Data = null
                    };
                }

                var updatedFarmer = await _unitOfWork.AccountRepository.GetByIdAsync(farmers.AccountId);
                updatedFarmer.IsActive = !updatedFarmer.IsActive;
                var rs = await _unitOfWork.AccountRepository.UpdateAsync(updatedFarmer);

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

        public async Task<IBusinessResult> RemoveFarmer(int id)
        {
            try
            {
                var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(id);
                var account = await _unitOfWork.AccountRepository.GetByIdAsync(farmer.AccountId);

                if (farmer == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any farmers!",
                        Data = null
                    };
                }

                var result = await _unitOfWork.FarmerRepository.RemoveAsync(farmer);
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

        public async Task<IBusinessResult> CreateFarmer(CreateFarmer model)
        {
            try
            {
                string password = PasswordHelper.GeneratePassword(model.Name, model.DOB);
                var newAccount = _mapper.Map<Account>(model);
                newAccount.Role = "Farmer";
                newAccount.IsActive = true;
                newAccount.Password = password;
                newAccount.CreatedAt = DateTime.Now;

                var rs = await _unitOfWork.AccountRepository.CreateAsync(newAccount);

                var newFarmer = _mapper.Map<Farmer>(model);
                newFarmer.AccountId = newAccount.Id;
                
                var rsf = await _unitOfWork.FarmerRepository.CreateAsync(newFarmer);

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
                    Message = "Create farmer success !",
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

        public async Task<IBusinessResult> UpdateFarmer(int id, CreateFarmer model)
        {
            try
            {
                var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(id);
                if (farmer == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Farmer not found !",
                        Data = null
                    };
                }

                var account = await _unitOfWork.AccountRepository.GetByIdAsync(farmer.AccountId);
                account.Name = model.Name;
                account.Email = model.Email;
                account.UpdatedAt = DateTime.Now;
                await _unitOfWork.AccountRepository.UpdateAsync(account);

                farmer.DOB = model.DOB;
                farmer.Phone = model.Phone;
                farmer.Avatar = model.Avatar;

                var rs = await _unitOfWork.FarmerRepository.UpdateAsync(farmer);
                if (rs > 0)
                {
                    return new BusinessResult
                    {
                        Status = 200,
                        Message = "Update successfull",
                        Data = null
                    };
                } else
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

        public async Task<IBusinessResult> AddFarmerTokenDevice(int id, string tokenDevice)
        {
            var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(id);
            if (farmer == null)
            {
                return new BusinessResult(404, "Not found any farmers !");
            }

            var key = $"Farmer-{id}";
            try
            {
                var token = await _ablyHelper.RegisterTokenDevice(tokenDevice, "farmer");

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

        public async Task<IBusinessResult> GetAllDeviceTokensByFarmerId(int id)
        {
            var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(id);
            if (farmer == null)
            {
                return new BusinessResult(404, "Not found any farmers !");
            }

            var key = $"Farmer-{id}";
            try
            {
                if (_redisManagement.IsConnected == false) throw new Exception();
                string productListJson = _redisManagement.GetData(key);
                if (productListJson == null || productListJson == "[]")
                {
                    return new BusinessResult(400, "This Farmer do not have DeviceToken");
                }
                var result = JsonConvert.DeserializeObject<DeviceTokenModel>(productListJson);
                return new BusinessResult(200, "Farmer device token", result);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, $"Redis is Fail: {ex.Message}");
            }

        }

        public async Task<IBusinessResult> RemoveDeviceTokenByFarmerId(int id)
        {
            var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(id);
            if (farmer == null)
            {
                return new BusinessResult(404, "Not found any farmers !");
            }

            var key = $"Farmer-{id}";
            try
            {
                if (_redisManagement.IsConnected == false) throw new Exception();
                string productListJson = _redisManagement.GetData(key);
                if (productListJson == null || productListJson == "[]")
                {
                    return new BusinessResult(400, "This Farmer do not have DeviceToken");
                }
                var result = JsonConvert.DeserializeObject<DeviceTokenModel>(productListJson);

                foreach (var item in result.Tokens)
                {
                    await _ablyHelper.RemoveTokenDevice(item);
                }

                _redisManagement.DeleteData(key);
                return new BusinessResult(200, "Removed Farmer device token successfully !");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, $"Redis is Fail: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> GetFarmerCalendar(int id, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(id);
                if (farmer == null)
                {
                    return new BusinessResult(404, "Not found any Farmer !");
                }

                var caringCalendar = await _unitOfWork.CaringTaskRepository.GetCaringCalander(id, startDate, endDate);
                var caringTaskList = caringCalendar.Select(cc => new FarmerInTaskCalendar
                {
                    TaskId = cc.Id,
                    TaskType = "Caring Task",
                    StartDate = cc.StartDate,
                    EndDate = cc.EndDate,
                }).ToList();

                var harvestingCalendar = await _unitOfWork.HarvestingTaskRepository.GetHarvestingCalander(id, startDate, endDate);
                var harvestingTaskList = harvestingCalendar.Select(hc => new FarmerInTaskCalendar
                {
                    TaskId = hc.Id,
                    TaskType = "Harvesting Task",
                    StartDate = hc.StartDate,
                    EndDate = hc.EndDate,
                }).ToList();

                var packagingCalendar = await _unitOfWork.PackagingTaskRepository.GetPackagingCalander(id, startDate, endDate);
                var packagingTaskList = packagingCalendar.Select(pc => new FarmerInTaskCalendar
                {
                    TaskId = pc.Id,
                    TaskType = "Packaging Task",
                    StartDate = pc.StartDate,
                    EndDate = pc.EndDate,
                }).ToList();

                var allTasks = new List<FarmerInTaskCalendar>();
                allTasks.AddRange(caringTaskList);
                allTasks.AddRange(harvestingTaskList);
                allTasks.AddRange(packagingTaskList);

                return new BusinessResult(200, "Farmer Calander: ", allTasks.OrderBy(c => c.StartDate));
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
