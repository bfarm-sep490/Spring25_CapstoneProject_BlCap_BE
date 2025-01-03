using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IImageFieldService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> DeleteImage(int id);
        Task<IBusinessResult> SwitchStatus(int id);
    }

    public class ImageFieldService : IImageFieldService
    {
        private readonly UnitOfWork _unitOfWork;
        public ImageFieldService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var rs = await _unitOfWork.ImageFieldRepository.GetAllAsync();
                if (rs.Count > 0)
                {
                    return new BusinessResult
                    {
                        Status = 200,
                        Message = "Read success",
                        Data = rs
                    };
                } 
                else
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "No data",
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

        public async Task<IBusinessResult> DeleteImage(int id)
        {
            try
            {
                var image = await _unitOfWork.ImageFieldRepository.GetByIdAsync(id);
                if (image == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found!",
                        Data = null
                    };
                }

                var rs = await CloudinaryHelper.DeleteImage(image.PublicId);
                if (rs)
                {
                    var result = await _unitOfWork.ImageFieldRepository.RemoveAsync(image);

                    if (result)
                    {
                        return new BusinessResult
                        {
                            Status = 200,
                            Message = "Delete success",
                            Data = null
                        };
                    }
                    else
                    {
                        return new BusinessResult
                        {
                            Status = 500,
                            Message = "Delete failed !",
                            Data = null
                        };
                    }
                } 
                else
                {
                    return new BusinessResult
                    {
                        Status = 500,
                        Message = "Fail to remove in Cloudinary",
                        Data = null
                    };
                }
            } 
            catch(Exception ex)
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
                var image = await _unitOfWork.ImageFieldRepository.GetByIdAsync(id);
                if (image == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found!",
                        Data = null
                    };
                }

                image.Status = image.Status == "Inactive" ? "Active" : "Inactive";
                var rs = await _unitOfWork.ImageFieldRepository.UpdateAsync(image);
                if(rs > 0)
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
            catch(Exception ex)
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
