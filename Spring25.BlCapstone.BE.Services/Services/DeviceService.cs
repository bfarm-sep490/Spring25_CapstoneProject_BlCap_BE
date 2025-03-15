using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Device;
using Spring25.BlCapstone.BE.Services.BusinessModels.Item;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IDeviceService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> CreateDevice(CreateDevice model);
        Task<IBusinessResult> UpdateDevice(int id, UpdateDevice model);
        Task<IBusinessResult> RemoveDevice(int id);
    }

    public class DeviceService : IDeviceService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeviceService(IMapper mapper)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var devices = await _unitOfWork.DeviceRepository.GetAllAsync();
                var res = _mapper.Map<List<DeviceModels>>(devices);

                if (devices.Count <= 0)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any Devices!",
                        Data = null
                    };
                }
                else
                {
                    return new BusinessResult
                    {
                        Status = 200,
                        Message = "Read data successfully !!!",
                        Data = res
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

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var device = await _unitOfWork.DeviceRepository.GetByIdAsync(id);

                if (device == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any Devices!",
                        Data = null
                    };
                }

                var res = _mapper.Map<DeviceModels>(device);

                return new BusinessResult
                {
                    Status = 200,
                    Message = "Read data successfully !!!",
                    Data = res
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

        public async Task<IBusinessResult> RemoveDevice(int id)
        {
            try
            {
                var device = await _unitOfWork.DeviceRepository.GetByIdAsync(id);

                if (device == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any devices!",
                        Data = null
                    };
                }

                var result = await _unitOfWork.DeviceRepository.RemoveAsync(device);

                if (result)
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

        public async Task<IBusinessResult> CreateDevice(CreateDevice model)
        {
            try
            {
                if (model.YieldId.HasValue)
                {
                    var yield = await _unitOfWork.YieldRepository.GetByIdAsync(model.YieldId.Value);
                    if (yield == null)
                    {
                        return new BusinessResult
                        {
                            Status = 404,
                            Message = "Yield not found !",
                            Data = null
                        };
                    }
                }
                
                var newDevice = _mapper.Map<Device>(model);
                newDevice.CreatedAt = DateTime.Now;

                var rs = await _unitOfWork.DeviceRepository.CreateAsync(newDevice);

                if (rs == null)
                {
                    return new BusinessResult
                    {
                        Status = 500,
                        Message = "Create failed !",
                        Data = null
                    };
                }
                else
                {
                    return new BusinessResult
                    {
                        Status = 200,
                        Message = "Create successfully!",
                        Data = rs
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

        public async Task<IBusinessResult> UpdateDevice(int id, UpdateDevice model)
        {
            try
            {
                var existedDevice = await _unitOfWork.DeviceRepository.GetByIdAsync(id);
                if (existedDevice == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any Devices!",
                        Data = null
                    };
                }

                if (model.YieldId.HasValue)
                {
                    var yield = await _unitOfWork.YieldRepository.GetByIdAsync(model.YieldId.Value);
                    if (yield == null)
                    {
                        return new BusinessResult
                        {
                            Status = 404,
                            Message = "Yield not found !",
                            Data = null
                        };
                    }
                }

                _mapper.Map(model, existedDevice);
                existedDevice.UpdatedAt = DateTime.Now;
                existedDevice.UpdatedBy = model.UpdatedBy;

                var rs = await _unitOfWork.DeviceRepository.UpdateAsync(existedDevice);
                if (rs <= 0)
                {
                    return new BusinessResult
                    {
                        Status = 500,
                        Message = "Update failed!",
                        Data = null
                    };
                }
                else
                {
                    return new BusinessResult
                    {
                        Status = 200,
                        Message = "Update success!",
                        Data = existedDevice
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
