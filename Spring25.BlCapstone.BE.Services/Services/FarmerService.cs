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
using Spring25.BlCapstone.BE.Services.BusinessModels.Notification;
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
        Task<IBusinessResult> GetListNotifications(int id);
        Task<IBusinessResult> MarkAsRead(int id);
        Task<IBusinessResult> MarkAllAsRead(int farmerId);
    }

    public class FarmerService : IFarmerService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RedisManagement _redisManagement;

        public FarmerService(IMapper mapper, RedisManagement redisManagement)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
            _redisManagement = redisManagement;
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var list = await _unitOfWork.FarmerRepository.GetFarmers();
                var result = _mapper.Map<List<FarmerModel>>(list);

                return new BusinessResult(200, "List Farmers", result);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var users = await _unitOfWork.FarmerRepository.GetFarmerById(id);
                if (users == null)
                {
                    return new BusinessResult
                    {
                        Status = 400,
                        Message = "Not found any Farmers",
                        Data = null
                    };
                }

                var result = _mapper.Map<FarmerModel>(users);

                return new BusinessResult
                {
                    Status = 200,
                    Message = "Farmer ne ma !",
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
                        Status = 400,
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
                        Status = 400,
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
                var account = await _unitOfWork.AccountRepository.GetByEmail(model.Email);
                if (account != null)
                {
                    return new BusinessResult(400, "This email have been used !");
                }

                string password = PasswordHelper.GeneratePassword(model.Name, model.DOB);
                var newAccount = _mapper.Map<Account>(model);
                newAccount.Role = "Farmer";
                newAccount.IsActive = true;
                newAccount.Password = password;
                newAccount.CreatedAt = DateTime.Now;

                await _unitOfWork.AccountRepository.CreateAsync(newAccount);

                var newFarmer = _mapper.Map<Farmer>(model);
                newFarmer.AccountId = newAccount.Id;

                var farmer = await _unitOfWork.FarmerRepository.CreateAsync(newFarmer);

                await _unitOfWork.FarmerPerformanceRepository.CreateAsync(new FarmerPerformance
                {
                    Id = newFarmer.Id,
                    CompletedTasks = 0,
                    IncompleteTasks = 0,
                    PerformanceScore = null
                });

                var body = EmailHelper.GetEmailBody("RegisterAccount.html", new Dictionary<string, string>
                {
                    { "{{UserName}}", model.Name },
                    { "{{Email}}", model.Email },
                    { "{{ResetPasswordLink}}", "https://bfarmx.space/reset-password" }
                });

                await EmailHelper.SendMail(model.Email, "Chào mừng bạn đến với BFARMX - Blockchain FarmXperience!", model.Name, body);

                var farmerChanel = $"farmer-{farmer.Id}";
                var message = "BFarmX - Blockchain FarmXperience rất vui khi được có bạn trong hệ thống của chúng tôi. Mong chúng ta có thể hợp tác lâu dài trong tương lai!";
                var title = $"Xin chào, {newAccount.Name}";
                await AblyHelper.SendMessageWithChanel(title, message, farmerChanel);
                await _unitOfWork.NotificationFarmerRepository.CreateAsync(new NotificationFarmer
                {
                    FarmerId = farmer.Id,
                    Message = message,
                    Title = title,
                    IsRead = false,
                    CreatedDate = DateTime.Now,
                });

                var rs = _mapper.Map<FarmerModel>(newFarmer);

                return new BusinessResult
                {
                    Status = 200,
                    Message = "Create farmer success !",
                    Data = rs
                };

            }
            catch (Exception ex)
            {
                return new BusinessResult
                {
                    Status = 500,
                    Message = $"{ex.Message}",
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
                        Status = 400,
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

        public async Task<IBusinessResult> AddFarmerTokenDevice(int id, string tokenDevice)
        {
            var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(id);
            if (farmer == null)
            {
                return new BusinessResult(400, "Not found any farmers !");
            }

            var key = $"Farmer-{id}";
            try
            {
                var token = await AblyHelper.RegisterTokenDevice(tokenDevice, "farmer");

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
                return new BusinessResult(400, "Not found any farmers !");
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
                return new BusinessResult(400, "Not found any farmers !");
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
                    await AblyHelper.RemoveTokenDevice(item);
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
                    return new BusinessResult(400, "Not found any Farmer !");
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

        public async Task<IBusinessResult> GetListNotifications(int id)
        {
            try
            {
                var notis = await _unitOfWork.NotificationFarmerRepository.GetNotificationsByFarmerId(id);
                if (!notis.Any())
                {
                    return new BusinessResult(400, "There aren't any notifications !");
                }

                var res = _mapper.Map<List<FarmerNotificationsModel>>(notis);
                return new BusinessResult(200, "List notifications :", res);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> MarkAsRead(int id)
        {
            try
            {
                var noti = await _unitOfWork.NotificationFarmerRepository.GetByIdAsync(id);
                if (noti == null)
                {
                    return new BusinessResult(400, "Not found this notifications");
                }

                noti.IsRead = true;
                await _unitOfWork.NotificationFarmerRepository.UpdateAsync(noti);
                return new BusinessResult(200, "Mark as read successfully!");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> MarkAllAsRead(int farmerId)
        {
            try
            {
                var farmer = await _unitOfWork.FarmerRepository.GetByIdAsync(farmerId);
                if (farmer == null)
                {
                    return new BusinessResult(400, "Not found this farmer");
                }

                var notis = await _unitOfWork.NotificationFarmerRepository.GetNotificationsByFarmerId(farmerId);
                notis.ForEach(n =>
                {
                    n.IsRead = true;
                    _unitOfWork.NotificationFarmerRepository.PrepareUpdate(n);
                });

                await _unitOfWork.NotificationFarmerRepository.SaveAsync();

                return new BusinessResult(200, "Mark all as read successfully !");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
