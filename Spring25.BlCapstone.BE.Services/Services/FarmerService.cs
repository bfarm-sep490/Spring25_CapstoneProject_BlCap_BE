﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
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
    }

    public class FarmerService : IFarmerService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FarmerService(IMapper mapper)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
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
    }
}
