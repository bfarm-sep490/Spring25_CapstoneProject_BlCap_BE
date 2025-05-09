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
using Spring25.BlCapstone.BE.Services.BusinessModels.Expert;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Notification;
using Spring25.BlCapstone.BE.Services.Untils;
using Spring25.BlCapstone.BE.Services.Utils;
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
        Task<IBusinessResult> GetListNotifications(int id);
        Task<IBusinessResult> MarkAsRead(int id);
        Task<IBusinessResult> MarkAllAsRead(int expertId);
    }

    public class ExpertService : IExpertService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RedisManagement _redisManagement;
        private IConfiguration _configuration;

        public ExpertService(IMapper mapper, RedisManagement redisManagement, IConfiguration configuration)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
            _redisManagement = redisManagement;
            _configuration = configuration;
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var list = await _unitOfWork.ExpertRepository.GetExperts();
                var result = _mapper.Map<List<ExpertModel>>(list);

                return new BusinessResult(200, "List Experts", result);
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
                var users = await _unitOfWork.ExpertRepository.GetExpert(id);
                var result = _mapper.Map<ExpertModel>(users);

                if (users == null)
                {
                    return new BusinessResult
                    {
                        Status = 400,
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
                        Status = 400,
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
                        Status = 400,
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
                newAccount.CreatedAt = DateTimeHelper.NowVietnamTime();

                var rs = await _unitOfWork.AccountRepository.CreateAsync(newAccount);

                var newExpert = _mapper.Map<Expert>(model);
                newExpert.AccountId = newAccount.Id;
                var rsf = await _unitOfWork.ExpertRepository.CreateAsync(newExpert);

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
                await _unitOfWork.NotificationExpertRepository.CreateAsync(new NotificationExpert
                {
                    ExpertId = rsf.Id,
                    Message = message,
                    Title = title,
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow,
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
                        Status = 400,
                        Message = "3xpert not found !",
                        Data = null
                    };
                }

                var account = await _unitOfWork.AccountRepository.GetByIdAsync(expert.AccountId);
                account.Name = model.Name;
                account.Email = model.Email;
                account.UpdatedAt = DateTimeHelper.NowVietnamTime();
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

        public async Task<IBusinessResult> GetListNotifications(int id)
        {
            try
            {
                var notis = await _unitOfWork.NotificationExpertRepository.GetNotificationsByExpertId(id);
                if (!notis.Any())
                {
                    return new BusinessResult(400, "There aren't any notifications !");
                }

                var res = _mapper.Map<List<ExpertNotificationsModel>>(notis);
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
                var noti = await _unitOfWork.NotificationExpertRepository.GetByIdAsync(id);
                if (noti == null)
                {
                    return new BusinessResult(400, "Not found this notifications");
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
                    return new BusinessResult(400, "Not found this expert");
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
