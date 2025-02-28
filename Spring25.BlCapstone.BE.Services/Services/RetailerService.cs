using Microsoft.AspNetCore.Http;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
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
    }

    public class RetailerService : IRetailerService
    {
        private readonly UnitOfWork _unitOfWork;

        public RetailerService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            var list = await _unitOfWork.RetailerRepository.GetRetailers();
            var result = list.Select(e => new RetailerModels
            {
                Id = e.Id,
                Email = e.Account.Email,
                Name = e.Account.Name,
                Phone = e.Phone,
                Status = e.Status,
                Avatar = e.Avatar,
                CreatedAt = e.Account.CreatedAt,
                UpdatedAt = e.Account.UpdatedAt,
                IsActive = e.Account.IsActive,
                LongxLat = e.LongxLat,
                Address = e.Address,
            })
            .ToList();

            if (result.Count > 0)
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
                var result = users
                    .Where(u => u.Id == id)
                    .Select(f => new RetailerModels
                    {
                        Id = f.Id,
                        Email = f.Account.Email,
                        Name = f.Account.Name,
                        Phone = f.Phone,
                        Status = f.Status,
                        Avatar = f.Avatar,
                        IsActive = f.Account.IsActive,
                        CreatedAt = f.Account.CreatedAt,
                        UpdatedAt = f.Account.UpdatedAt,
                        LongxLat = f.LongxLat,
                        Address = f.Address,
                    })
                .ToList();

                if (result == null || !result.Any())
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
                var retailers = await _unitOfWork.RetailerRepository.GetRetailers();
                var updatedRetailer = retailers.FirstOrDefault(f => f.Id == id);
                if (updatedRetailer == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found!",
                        Data = null
                    };
                }

                updatedRetailer.Account.IsActive = !updatedRetailer.Account.IsActive;
                var rs = await _unitOfWork.RetailerRepository.UpdateAsync(updatedRetailer);

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
                var newAccount = new Account
                {
                    Email = model.Email,
                    Name = model.Name,
                    Role = "Retailer",
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    Password = password
                };
                var rs = await _unitOfWork.AccountRepository.CreateAsync(newAccount);

                var newRetailer = new Retailer
                {
                    AccountId = newAccount.Id,
                    DOB = model.DOB != null ? model.DOB : null,
                    Phone = model.Phone != null ? model.Phone : "",
                    Status = "?",
                    Avatar = model.Avatar != null ? model.Avatar : null,
                    LongxLat = model.LongxLat != null ? model.LongxLat : "0",
                    Address = model.Address != null ? model.Address : "Somewhere..."
                };
                var rsf = await _unitOfWork.RetailerRepository.CreateAsync(newRetailer);

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

                retailer.DOB = model.DOB;
                retailer.Phone = model.Phone != null ? model.Phone : null;
                retailer.Avatar = model.Avatar != null ? model.Avatar : null;
                retailer.LongxLat = model.LongxLat != null ? model.LongxLat : "0";
                retailer.Address = model.Address != null ? model.Address : "Somewhere...";

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
    }
}
