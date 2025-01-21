using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Item;
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
                Password = e.Account.Password,
                Name = e.Account.Name,
                Phone = e.Phone,
                Status = e.Status,
                Avatar = e.Avatar,
                IsActive = e.Account.IsActive
            })
            .ToList();
            return new BusinessResult(200, "List Experts", result);
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

                expert.Account.IsActive = !expert.Account.IsActive;
                var rs = await _unitOfWork.ExpertRepository.UpdateAsync(expert);

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
    }
}
