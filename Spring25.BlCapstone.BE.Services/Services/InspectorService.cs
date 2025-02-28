using AutoMapper;
using CloudinaryDotNet.Core;
using IO.Ably;
using Microsoft.AspNetCore.Http;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Inspector;
using Spring25.BlCapstone.BE.Services.Untils;
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
    }

    public class InspectorService : IInspectorService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

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

                if (result.Any())
                {
                    return new BusinessResult(200, "List Inspectors", result);
                }

                return new BusinessResult(404, "Not found any Inspectors", null);
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

                return new BusinessResult(404, "Not found any Inspectors", null);
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
                var ins = await _unitOfWork.InspectorRepository.GetInspector(id);
                if (ins == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any inspectors!",
                        Data = null
                    };
                }

                ins.Account.IsActive = !ins.Account.IsActive;
                var rs = await _unitOfWork.InspectorRepository.UpdateAsync(ins);

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
                        Status = 404,
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
                string password = PasswordHelper.GeneratePassword(model.Name, null);
                var newAccount = new Account
                {
                    Email = model.Email,
                    Name = model.Name,
                    Role = "Expert",
                    IsActive = true,
                    Password = password,
                    CreatedAt = DateTime.Now
                };
                var rs = await _unitOfWork.AccountRepository.CreateAsync(newAccount);

                var newIns = new Inspector
                {
                    AccountId = newAccount.Id,
                    Description = model.Description,
                    Address = model.Address,
                    Status = "?",
                    ImageUrl = model.ImageUrl != null ? model.ImageUrl : null,
                    IsAvailable = true,
                };
                var rsf = await _unitOfWork.InspectorRepository.CreateAsync(newIns);

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
                        Status = 404,
                        Message = "Inspector not found !",
                        Data = null
                    };
                }

                var account = await _unitOfWork.AccountRepository.GetByIdAsync(ins.AccountId);
                account.Name = model.Name;
                account.Email = model.Email;
                account.UpdatedAt = DateTime.Now;
                await _unitOfWork.AccountRepository.UpdateAsync(account);

                ins.Description = model.Description;
                ins.Address = ins.Address;
                ins.ImageUrl = model.ImageUrl;
                ins.Status = model.Status;

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
    }
}
