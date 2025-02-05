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
    public interface IDriverService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> SwitchStatus(int id);
        Task<IBusinessResult> RemoveDriver(int id);
        Task<IBusinessResult> CreateDriver(CreateFarmer model);
        Task<IBusinessResult> UpdateDriver(int id, CreateFarmer model);
    }

    public class DriverService : IDriverService
    {
        private readonly UnitOfWork _unitOfWork;

        public DriverService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            var list = await _unitOfWork.DriverRepository.GetDrivers();
            var result = list.Select(e => new FarmerModel
            {
                Id = e.Id,
                Email = e.Account.Email,
                Password = e.Account.Password,
                Name = e.Account.Name,
                Phone = e.Phone,
                Status = e.Status,
                Avatar = e.Avatar,
                IsActive = e.Account.IsActive,
                UpdatedAt = e.Account.UpdatedAt,
                CreatedAt = e.Account.CreatedAt,
            })
            .ToList();

            if (result.Count > 0)
            {
                return new BusinessResult(200, "List Drivers", result);
            }
            else
            {
                return new BusinessResult(404, "Not Found Any Drivers", null);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var users = await _unitOfWork.DriverRepository.GetDrivers();
                var result = users
                    .Where(u => u.Id == id)
                    .Select(f => new FarmerModel
                    {
                        Id = f.Id,
                        Email = f.Account.Email,
                        Password = f.Account.Password,
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
                        Message = "Not found any Drivers",
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
                var drivers = await _unitOfWork.DriverRepository.GetDrivers();
                var updatedDriver = drivers.FirstOrDefault(f => f.Id == id);
                if (updatedDriver == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found!",
                        Data = null
                    };
                }

                updatedDriver.Account.IsActive = !updatedDriver.Account.IsActive;
                var rs = await _unitOfWork.DriverRepository.UpdateAsync(updatedDriver);

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

        public async Task<IBusinessResult> RemoveDriver(int id)
        {
            try
            {
                var driver = await _unitOfWork.DriverRepository.GetByIdAsync(id);
                var account = await _unitOfWork.AccountRepository.GetByIdAsync(driver.AccountId);

                if (driver == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any drivers!",
                        Data = null
                    };
                }

                var result = await _unitOfWork.DriverRepository.RemoveAsync(driver);
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

        public async Task<IBusinessResult> CreateDriver(CreateFarmer model)
        {
            try
            {
                var newAccount = new Account
                {
                    Email = model.Email,
                    Name = model.Name,
                    Role = "Driver",
                    Password = model.Password,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };
                var rs = await _unitOfWork.AccountRepository.CreateAsync(newAccount);

                string url = null;
                if (model.Avatar != null)
                {
                    var i = await CloudinaryHelper.UploadImage(model.Avatar);
                    url = i.Url;
                }

                var newDriver = new Driver
                {
                    AccountId = newAccount.Id,
                    DOB = model.DOB != null ? model.DOB : null,
                    Phone = model.Phone != null ? model.Phone : null,
                    Status = "?",
                    Avatar = url != null ? url : null,
                };
                var rsf = await _unitOfWork.DriverRepository.CreateAsync(newDriver);

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
                    Message = "Create driver success !",
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

        public async Task<IBusinessResult> UpdateDriver(int id, CreateFarmer model)
        {
            try
            {
                var driver = await _unitOfWork.DriverRepository.GetByIdAsync(id);
                if (driver == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Driver not found !",
                        Data = null
                    };
                }

                var account = await _unitOfWork.AccountRepository.GetByIdAsync(driver.AccountId);
                account.Name = model.Name;
                account.Email = model.Email;
                account.Password = model.Password;
                account.UpdatedAt = DateTime.Now;
                await _unitOfWork.AccountRepository.UpdateAsync(account);

                driver.DOB = model.DOB;
                driver.Phone = model.Phone;
                var url = await CloudinaryHelper.UploadImage(model.Avatar);
                driver.Avatar = url.Url;

                var rs = await _unitOfWork.DriverRepository.UpdateAsync(driver);
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
    }
}
