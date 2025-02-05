using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
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
            var result = list.Select(e => new FarmerModel
            {
                Id = e.Id,
                Email = e.Account.Email,
                Password = e.Account.Password,
                Name = e.Account.Name,
                Phone = e.Phone,
                Status = e.Status,
                Avatar = e.Avatar,
                IsActive = e.Account.IsActive
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
                    .Select(f => new FarmerModel
                    {
                        Id = f.Id,
                        Email = f.Account.Email,
                        Password = f.Account.Password,
                        Name = f.Account.Name,
                        Phone = f.Phone,
                        Status = f.Status,
                        Avatar = f.Avatar,
                        IsActive = f.Account.IsActive
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
    }
}
