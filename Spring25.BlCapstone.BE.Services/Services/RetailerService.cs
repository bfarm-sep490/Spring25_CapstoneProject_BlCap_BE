﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Helper;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Redis;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Auth;
using Spring25.BlCapstone.BE.Services.BusinessModels.Notification;
using Spring25.BlCapstone.BE.Services.BusinessModels.Retailer;
using Spring25.BlCapstone.BE.Services.Untils;
using Spring25.BlCapstone.BE.Services.Utils;
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
        Task<IBusinessResult> GetListNotifications(int id);
        Task<IBusinessResult> MarkAsRead(int id);
        Task<IBusinessResult> MarkAllAsRead(int retailerId);
    }

    public class RetailerService : IRetailerService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IConfiguration _configuration;
        private readonly RedisManagement _redisManagement;

        public RetailerService(IMapper mapper, RedisManagement redisManagement, IConfiguration configuration)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
            _redisManagement = redisManagement;
            _configuration = configuration;
        }

        public async Task<IBusinessResult> GetAll()
        {
            var list = await _unitOfWork.RetailerRepository.GetRetailers();
            var result = _mapper.Map<List<RetailerModels>>(list);

            return new BusinessResult(200, "List Retailers", result);
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var users = await _unitOfWork.RetailerRepository.GetRetailerById(id);
                if (users == null)
                {
                    return new BusinessResult
                    {
                        Status = 400,
                        Message = "Not found any Retailers",
                        Data = null
                    };
                }
                var result = _mapper.Map<List<RetailerModels>>(users);

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
                        Status = 400,
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
                        Status = 400,
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
                var account = await _unitOfWork.AccountRepository.GetByEmail(model.Email);
                if (account != null)
                {
                    return new BusinessResult(400, "This email have been used !");
                }

                string password = PasswordHelper.GeneratePassword(model.Name, model.DOB);
                var newAccount = _mapper.Map<Account>(model);
                newAccount.Password = password;
                newAccount.IsActive = true;
                newAccount.Password = password;
                newAccount.Role = "Retailer";
                newAccount.CreatedAt = DateTimeHelper.NowVietnamTime();

                var rs = await _unitOfWork.AccountRepository.CreateAsync(newAccount);

                var newRetailer = _mapper.Map<Retailer>(model);
                newRetailer.AccountId = newAccount.Id;
                var rsf = await _unitOfWork.RetailerRepository.CreateAsync(newRetailer);

                var token = JWTHelper.GenerateResetPasswordToken(model.Email, _configuration["JWT:Secret"], _configuration["JWT:ValidAudience"], _configuration["JWT:ValidIssuer"], password, newAccount.Id);

                var body = EmailHelper.GetEmailBody("RegisterAccount.html", new Dictionary<string, string>
                {
                    { "{{UserName}}", model.Name },
                    { "{{Email}}", model.Email },
                    { "{{ResetPasswordLink}}", $"https://bfarmx.space/auth/reset-password?token={token}" }
                });

                await EmailHelper.SendMail(model.Email, "Chào mừng bạn đến với BFARMX - Blockchain FarmXperience!", model.Name, body);

                var message = "BFarmX - Blockchain FarmXperience rất vui khi được có bạn trong hệ thống của chúng tôi. Mong chúng ta có thể hợp tác lâu dài trong tương lai!";
                var title = $"Xin chào, {newAccount.Name}";
                await _unitOfWork.NotificationRetailerRepository.CreateAsync(new NotificationRetailer
                {
                    RetailerId = rsf.Id,
                    Message = message,
                    Title = title,
                    IsRead = false,
                    CreatedDate = DateTimeHelper.NowVietnamTime(),
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
                    Message = "Create retailer success !",
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

        public async Task<IBusinessResult> UpdateRetailer(int id, CreateRetailer model)
        {
            try
            {
                var retailer = await _unitOfWork.RetailerRepository.GetByIdAsync(id);
                if (retailer == null)
                {
                    return new BusinessResult
                    {
                        Status = 400,
                        Message = "Retailer not found !",
                        Data = null
                    };
                }

                var account = await _unitOfWork.AccountRepository.GetByIdAsync(retailer.AccountId);
                account.Name = model.Name;
                account.Email = model.Email;
                account.UpdatedAt = DateTimeHelper.NowVietnamTime();
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

        public async Task<IBusinessResult> GetListNotifications(int id)
        {
            try
            {
                var notis = await _unitOfWork.NotificationRetailerRepository.GetNotificationsByRetailerId(id);
                if (!notis.Any())
                {
                    return new BusinessResult(400, "There aren't any notifications !");
                }

                var res = _mapper.Map<List<RetailerNotificationsModel>>(notis);
                return new BusinessResult(200, "List notifications :", res.OrderByDescending(c => c.CreatedDate));
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
                var noti = await _unitOfWork.NotificationRetailerRepository.GetByIdAsync(id);
                if (noti == null)
                {
                    return new BusinessResult(400, "Not found this notifications");
                }

                noti.IsRead = true;
                await _unitOfWork.NotificationRetailerRepository.UpdateAsync(noti);
                return new BusinessResult(200, "Mark as read successfully!");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> MarkAllAsRead(int retailerId)
        {
            try
            {
                var expert = await _unitOfWork.RetailerRepository.GetByIdAsync(retailerId);
                if (expert == null)
                {
                    return new BusinessResult(400, "Not found this retailer");
                }

                var notis = await _unitOfWork.NotificationRetailerRepository.GetNotificationsByRetailerId(retailerId);
                notis.ForEach(n =>
                {
                    n.IsRead = true;
                    _unitOfWork.NotificationRetailerRepository.PrepareUpdate(n);
                });

                await _unitOfWork.NotificationRetailerRepository.SaveAsync();

                return new BusinessResult(200, "Mark all as read successfully !");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
