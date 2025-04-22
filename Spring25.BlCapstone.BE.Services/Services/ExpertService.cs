using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Helper;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Redis;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Auth;
using Spring25.BlCapstone.BE.Services.BusinessModels.Expert;
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
    public interface IExpertService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> SwitchStatus(int id);
        Task<IBusinessResult> RemoveExpert(int id);
        Task<IBusinessResult> CreateExpert(CreateFarmer model);
        Task<IBusinessResult> UpdateExpert(int id, CreateFarmer model);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
        Task<IBusinessResult> AddExpertTokenDevice(int id, string tokenDevice);
        Task<IBusinessResult> GetAllDeviceTokensByExpertId(int id);
        Task<IBusinessResult> RemoveDeviceTokenByExpertId(int id);
        Task<IBusinessResult> GetListNotifications(int id);
        Task<IBusinessResult> MarkAsRead(int id);
        Task<IBusinessResult> MarkAllAsRead(int expertId);
    }

    public class ExpertService : IExpertService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RedisManagement _redisManagement;

        public ExpertService(IMapper mapper, RedisManagement redisManagement)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
            _redisManagement = redisManagement;
        }

        public async Task<IBusinessResult> GetAll()
        {
            var list = await _unitOfWork.ExpertRepository.GetExperts();
            var result = _mapper.Map<List<ExpertModel>>(list);

            if(list.Count > 0)
            {
                return new BusinessResult(200, "List Experts", result);
            }
            else
            {
                return new BusinessResult(404, "Not Found Any Experts", null);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var users = await _unitOfWork.ExpertRepository.GetExperts(id);
                var result = _mapper.Map<List<ExpertModel>>(users);

                if (users.Count <= 0)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any Experts",
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
                var expert = await _unitOfWork.ExpertRepository.GetByIdAsync(id);
                if (expert == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found!",
                        Data = null
                    };
                }

                var updatedExpert = await _unitOfWork.AccountRepository.GetByIdAsync(expert.AccountId);
                updatedExpert.IsActive = !updatedExpert.IsActive;
                var rs = await _unitOfWork.AccountRepository.UpdateAsync(updatedExpert);

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

        public async Task<IBusinessResult> RemoveExpert(int id)
        {
            try
            {
                var expert = await _unitOfWork.ExpertRepository.GetByIdAsync(id);
                var account = await _unitOfWork.AccountRepository.GetByIdAsync(expert.AccountId);

                if (expert == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any experts!",
                        Data = null
                    };
                }

                var result = await _unitOfWork.ExpertRepository.RemoveAsync(expert);
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

        public async Task<IBusinessResult> CreateExpert(CreateFarmer model)
        {
            try
            {
                string password = PasswordHelper.GeneratePassword(model.Name, model.DOB);
                var newAccount = _mapper.Map<Account>(model);
                newAccount.Role = "Expert";
                newAccount.IsActive = true;
                newAccount.Password = password;
                newAccount.CreatedAt = DateTime.Now;

                var rs = await _unitOfWork.AccountRepository.CreateAsync(newAccount);

                var newExpert = _mapper.Map<Expert>(model);
                newExpert.AccountId = newAccount.Id;
                var rsf = await _unitOfWork.ExpertRepository.CreateAsync(newExpert);

                var body = EmailHelper.GetEmailBody("RegisterAccount.html", new Dictionary<string, string>
                {
                    { "{{UserName}}", model.Name },
                    { "{{Email}}", model.Email },
                    { "{{ResetPasswordLink}}", "https://bfarmx.space/reset-password" }
                });

                await EmailHelper.SendMail(model.Email, "Chào mừng bạn đến với BFARMX - Blockchain FarmXperience!", model.Name, body);

                var expertChanel = $"expert-{rsf.Id}";
                var message = "BFarmX - Blockchain FarmXperience rất vui khi được có bạn trong hệ thống của chúng tôi. Mong chúng ta có thể hợp tác lâu dài trong tương lai!";
                var title = $"Xin chào, {newAccount.Name}";
                await AblyHelper.SendMessageWithChanel(title, message, expertChanel);
                await _unitOfWork.NotificationExpertRepository.CreateAsync(new NotificationExpert
                {
                    ExpertId = rsf.Id,
                    Message = message,
                    Title = title,
                    IsRead = false,
                    CreatedDate = DateTime.Now,
                });

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
                    Message = "Create 3xpert success !",
                    Data = model
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

        public async Task<IBusinessResult> UpdateExpert(int id, CreateFarmer model)
        {
            try
            {
                var expert = await _unitOfWork.ExpertRepository.GetByIdAsync(id);
                if (expert == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "3xpert not found !",
                        Data = null
                    };
                }

                var account = await _unitOfWork.AccountRepository.GetByIdAsync(expert.AccountId);
                account.Name = model.Name;
                account.Email = model.Email;
                account.UpdatedAt = DateTime.Now;
                await _unitOfWork.AccountRepository.UpdateAsync(account);

                expert.DOB = model.DOB;
                expert.Phone = model.Phone;
                expert.Avatar = model.Avatar;

                var rs = await _unitOfWork.ExpertRepository.UpdateAsync(expert);
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

        public async Task<IBusinessResult> AddExpertTokenDevice(int id, string tokenDevice)
        {
            var expert = await _unitOfWork.ExpertRepository.GetByIdAsync(id);
            if (expert == null)
            {
                return new BusinessResult(404, "Not found any experts !");
            }

            var key = $"Expert-{id}";
            try
            {
                var token = await AblyHelper.RegisterTokenDevice(tokenDevice, "expert");

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

        public async Task<IBusinessResult> GetAllDeviceTokensByExpertId(int id)
        {
            var expert = await _unitOfWork.ExpertRepository.GetByIdAsync(id);
            if (expert == null)
            {
                return new BusinessResult(404, "Not found any experts !");
            }

            var key = $"Expert-{id}";
            try
            {
                if (_redisManagement.IsConnected == false) throw new Exception();
                string productListJson = _redisManagement.GetData(key);
                if (productListJson == null || productListJson == "[]")
                {
                    return new BusinessResult(400, "This expert do not have DeviceToken");
                }
                var result = JsonConvert.DeserializeObject<DeviceTokenModel>(productListJson);
                return new BusinessResult(200, "Expert device token", result);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, $"Redis is Fail: {ex.Message}");
            }

        }

        public async Task<IBusinessResult> RemoveDeviceTokenByExpertId(int id)
        {
            var expert = await _unitOfWork.ExpertRepository.GetByIdAsync(id);
            if (expert == null)
            {
                return new BusinessResult(404, "Not found any experts !");
            }

            var key = $"Expert-{id}";
            try
            {
                if (_redisManagement.IsConnected == false) throw new Exception();
                string productListJson = _redisManagement.GetData(key);
                if (productListJson == null || productListJson == "[]")
                {
                    return new BusinessResult(400, "This expert do not have DeviceToken");
                }
                var result = JsonConvert.DeserializeObject<DeviceTokenModel>(productListJson);

                foreach (var item in result.Tokens)
                {
                    await AblyHelper.RemoveTokenDevice(item);
                }

                _redisManagement.DeleteData(key);
                return new BusinessResult(200, "Removed expert device token successfully !");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, $"Redis is Fail: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> GetListNotifications(int id)
        {
            try
            {
                var notis = await _unitOfWork.NotificationExpertRepository.GetNotificationsByExpertId(id);
                if (!notis.Any())
                {
                    return new BusinessResult(404, "There aren't any notifications !");
                }

                var res = _mapper.Map<List<ExpertNotificationsModel>>(notis);
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
                var noti = await _unitOfWork.NotificationExpertRepository.GetByIdAsync(id);
                if (noti == null)
                {
                    return new BusinessResult(404, "Not found this notifications");
                }

                noti.IsRead = true;
                await _unitOfWork.NotificationExpertRepository.UpdateAsync(noti);
                return new BusinessResult(200, "Mark as read successfully!");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> MarkAllAsRead(int expertId)
        {
            try
            {
                var expert = await _unitOfWork.ExpertRepository.GetByIdAsync(expertId);
                if (expert == null)
                {
                    return new BusinessResult(404, "Not found this expert");
                }

                var notis = await _unitOfWork.NotificationExpertRepository.GetNotificationsByExpertId(expertId);
                notis.ForEach(n =>
                {
                    n.IsRead = true;
                    _unitOfWork.NotificationExpertRepository.PrepareUpdate(n);
                });

                await _unitOfWork.NotificationExpertRepository.SaveAsync();

                return new BusinessResult(200, "Mark all as read successfully !");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
