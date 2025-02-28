using AutoMapper;
using Microsoft.AspNetCore.Http;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Item;
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
    }

    public class ExpertService : IExpertService
    {
        private readonly UnitOfWork _unitOfWork;

        public ExpertService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            var list = await _unitOfWork.ExpertRepository.GetExperts();
            var result = list.Select(e => new FarmerModel
            {
                Id = e.Id,
                Email = e.Account.Email,
                Name = e.Account.Name,
                Phone = e.Phone,
                Status = e.Status,
                Avatar = e.Avatar,
                IsActive = e.Account.IsActive,
                UpdatedAt = e.Account.UpdatedAt,
                CreatedAt = e.Account.CreatedAt,
            })
            .ToList();

            if(result.Count > 0)
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
                var users = await _unitOfWork.ExpertRepository.GetExperts();
                var result = users
                    .Where(u => u.Id == id)
                    .Select(f => new FarmerModel
                    {
                        Id = f.Id,
                        Email = f.Account.Email,
                        Name = f.Account.Name,
                        Phone = f.Phone,
                        Status = f.Status,
                        Avatar = f.Avatar,
                        IsActive = f.Account.IsActive,
                        UpdatedAt = f.Account.UpdatedAt,
                        CreatedAt = f.Account.CreatedAt,
                    })
                .ToList();

                if (result == null || !result.Any())
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
                var experts = await _unitOfWork.ExpertRepository.GetExperts();
                var updatedExpert = experts.FirstOrDefault(f => f.Id == id);
                if (updatedExpert == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found!",
                        Data = null
                    };
                }

                updatedExpert.Account.IsActive = !updatedExpert.Account.IsActive;
                var rs = await _unitOfWork.ExpertRepository.UpdateAsync(updatedExpert);

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
                var newAccount = new Account
                {
                    Email = model.Email,
                    Name = model.Name,
                    Role = "Expert",
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    Password = password
                };
                var rs = await _unitOfWork.AccountRepository.CreateAsync(newAccount);

                var newExpert = new Expert
                {
                    AccountId = newAccount.Id,
                    DOB = model.DOB != null ? model.DOB : null,
                    Phone = model.Phone != null ? model.Phone : null,
                    Status = "?",
                    Avatar = model.Avatar != null ? model.Avatar : null,
                };
                var rsf = await _unitOfWork.ExpertRepository.CreateAsync(newExpert);

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
    }
}
