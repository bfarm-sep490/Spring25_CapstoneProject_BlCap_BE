﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Helper;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Redis;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Auth;
using Spring25.BlCapstone.BE.Services.BusinessModels.Inspector;
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
    public interface IInspectorService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> SwitchStatus(int id);
        Task<IBusinessResult> RemoveInspector(int id);
        Task<IBusinessResult> CreateInspector(CreateInspector model);
        Task<IBusinessResult> UpdateInspector(int id, UpdateInspector model);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
        Task<IBusinessResult> GetListNotifications(int id);
        Task<IBusinessResult> MarkAsRead(int id);
        Task<IBusinessResult> MarkAllAsRead(int inspectorId);
    }

    public class InspectorService : IInspectorService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RedisManagement _redisManagement;

        public InspectorService(IMapper mapper)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var list = await _unitOfWork.InspectorRepository.GetInspectors();
                var result = _mapper.Map<List<InspectorModel>>(list);

                    return new BusinessResult(200, "List Inspectors", result);
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

                return new BusinessResult(400, "Not found any Inspectors", null);
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
                        Status = 400,
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
                        Status = 400,
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
                newAccount.CreatedAt = DateTimeHelper.NowVietnamTime();

                var rs = await _unitOfWork.AccountRepository.CreateAsync(newAccount);

                var newIns = _mapper.Map<Inspector>(model);
                newIns.AccountId = newAccount.Id;
                var rsf = await _unitOfWork.InspectorRepository.CreateAsync(newIns);

                var message = "BFarmX - Blockchain FarmXperience rất vui khi được có bạn trong hệ thống của chúng tôi. Mong chúng ta có thể hợp tác lâu dài trong tương lai!";
                var title = $"Xin chào, {newAccount.Name}";
                await _unitOfWork.NotificationInspectorRepository.CreateAsync(new NotificationInspector
                {
                    InspectorId = rsf.Id,
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
                        Status = 400,
                        Message = "Inspector not found !",
                        Data = null
                    };
                }

                var account = await _unitOfWork.AccountRepository.GetByIdAsync(ins.AccountId);
                account.Name = model.Name;
                account.Email = model.Email;
                account.UpdatedAt = DateTimeHelper.NowVietnamTime();
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

        public async Task<IBusinessResult> GetListNotifications(int id)
        {
            try
            {
                var notis = await _unitOfWork.NotificationInspectorRepository.GetNotificationsByInspectorId(id);
                if (!notis.Any())
                {
                    return new BusinessResult(400, "There aren't any notifications !");
                }

                var res = _mapper.Map<List<InspectorNotificationsModel>>(notis);
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
                var noti = await _unitOfWork.NotificationInspectorRepository.GetByIdAsync(id);
                if (noti == null)
                {
                    return new BusinessResult(400, "Not found this notifications");
                }

                noti.IsRead = true;
                await _unitOfWork.NotificationInspectorRepository.UpdateAsync(noti);
                return new BusinessResult(200, "Mark as read successfully!");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> MarkAllAsRead(int inspectorId)
        {
            try
            {
                var expert = await _unitOfWork.InspectorRepository.GetByIdAsync(inspectorId);
                if (expert == null)
                {
                    return new BusinessResult(400, "Not found this inspector");
                }

                var notis = await _unitOfWork.NotificationInspectorRepository.GetNotificationsByInspectorId(inspectorId);
                notis.ForEach(n =>
                {
                    n.IsRead = true;
                    _unitOfWork.NotificationInspectorRepository.PrepareUpdate(n);
                });

                await _unitOfWork.NotificationInspectorRepository.SaveAsync();

                return new BusinessResult(200, "Mark all as read successfully !");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
