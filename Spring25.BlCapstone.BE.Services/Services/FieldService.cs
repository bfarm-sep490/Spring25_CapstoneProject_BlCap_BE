using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Fields;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IFieldService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> CreateField(CreateFieldModels field);
        Task<IBusinessResult> UpdateField(int id, UpdateFieldModels field);
        Task<IBusinessResult> SwitchStatus(int id);
        Task<IBusinessResult> RemoveField(int id);
    }

    public class FieldService : IFieldService
    {
        private readonly UnitOfWork _unitOfWork;
        public FieldService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var fields = await _unitOfWork.FieldRepository.GetFields();
                var results = fields.Select(f => new FieldReturnModel
                {
                    Id = f.Id,
                    Area = f.Area,
                    FarmOwnerId = f.FarmOwnerId,
                    Description = f.Description,
                    Wide = f.Wide,
                    Long = f.Long,
                    Type = f.Type,
                    Status = f.Status,
                    CreatedAt = f.CreatedAt,
                    CreatedBy = f.CreatedBy,
                    UpdatedAt = f.UpdatedAt,
                    UpdatedBy = f.UpdatedBy,
                    ImageOfField = f.ImageFields.Select(i => new ImagesOfFields
                    {
                        Id = i.Id,
                        Url = i.Url,
                        Status = i.Status,
                    }).ToList() ?? new List<ImagesOfFields>(),
                }).ToList();

                if (results.Count > 0)
                {
                    return new BusinessResult
                    {
                        Status = 200,
                        Message = "Read data successfully !!!",
                        Data = results
                    };
                } 
                else
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any Fields!",
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

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var fields = await _unitOfWork.FieldRepository.GetFields();
                var result = fields.Select(f => new FieldReturnModel
                {
                    Id = f.Id,
                    Area = f.Area,
                    FarmOwnerId = f.FarmOwnerId,
                    Description = f.Description,
                    Wide = f.Wide,
                    Long = f.Long,
                    Type = f.Type,
                    Status = f.Status,
                    CreatedAt = f.CreatedAt,
                    CreatedBy = f.CreatedBy,
                    UpdatedAt = f.UpdatedAt,
                    UpdatedBy = f.UpdatedBy,
                    ImageOfField = f.ImageFields.Select(i => new ImagesOfFields
                    {
                        Id = i.Id,
                        Url = i.Url,
                        Status = i.Status,
                    }).ToList() ?? new List<ImagesOfFields>(),
                }).FirstOrDefault(f => f.Id == id);

                if (result != null)
                {
                    return new BusinessResult
                    {
                        Status = 200,
                        Message = "Read data successfully !!!",
                        Data = result
                    };
                }
                else
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any Fields!",
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

        public async Task<IBusinessResult> CreateField(CreateFieldModels field)
        {
            try
            {
                var existedFarm = await _unitOfWork.FarmOwnerRepository.GetByIdAsync(field.FarmOwnerId);
                if (existedFarm == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any Farm Owners!",
                        Data = null
                    };
                }

                var newField = new Field
                {
                    Area = field.Area,
                    FarmOwnerId = field.FarmOwnerId,
                    Description = field.Description,
                    Wide = field.Wide,
                    Long = field.Long,
                    Type = field.Type,
                    Status = "Active",
                    CreatedAt = DateTime.Now,
                    CreatedBy = field.CreatedBy,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = field.CreatedBy
                };
                var rs = await _unitOfWork.FieldRepository.CreateAsync(newField);

                if (rs == null)
                {
                    return new BusinessResult
                    {
                        Status = 500,
                        Message = "Create failed !",
                        Data = null
                    };
                }

                if (field.ImageFields != null && field.ImageFields.Any())
                {
                    var urls = await CloudinaryHelper.UploadMultipleImages(field.ImageFields);

                    foreach (var image in urls)
                    {
                        var newImage = new ImageField
                        {
                            Url = image.Url,
                            Status = "Active",
                            FieldId = newField.Id,
                            PublicId = image.PublicId
                        };

                        _unitOfWork.ImageFieldRepository.PrepareCreate(newImage);
                    }

                    var result = await _unitOfWork.ImageFieldRepository.SaveAsync();
                    if (result <= 0)
                    {
                        return new BusinessResult
                        {
                            Status = 500,
                            Message = "Create image failed !",
                            Data = null
                        };
                    }
                }

                return new BusinessResult
                {
                    Status = 200,
                    Message = "Create successfully!",
                    Data = null
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

        public async Task<IBusinessResult> UpdateField(int id, UpdateFieldModels field)
        {
            try
            {
                var existedFarm = await _unitOfWork.FarmOwnerRepository.GetByIdAsync(field.FarmOwnerId);
                if (existedFarm == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any Farm Owners!",
                        Data = null
                    };
                }

                var updateField = await _unitOfWork.FieldRepository.GetFieldsById(id);
                updateField.Area = field.Area;
                updateField.FarmOwnerId = field.FarmOwnerId;
                updateField.Description = field.Description;
                updateField.Wide = field.Wide;
                updateField.Long = field.Long;
                updateField.Type = field.Type;
                updateField.UpdatedBy = field.UpdatedBy;
                updateField.UpdatedAt = DateTime.Now;

                var rs = await _unitOfWork.FieldRepository.UpdateAsync(updateField);
                if (rs <= 0)
                {
                    return new BusinessResult
                    {
                        Status = 500,
                        Message = "Update field failed!",
                        Data = null
                    };
                }

                var images = _unitOfWork.ImageFieldRepository.FindByCondition(img => img.FieldId == id).ToList();
                if (images != null || images.Any())
                {
                    foreach (var image in images)
                    {
                        await _unitOfWork.ImageFieldRepository.RemoveAsync(image);
                    }
                }

                if (field.ImageFields != null && field.ImageFields.Any())
                {
                    var urls = await CloudinaryHelper.UploadMultipleImages(field.ImageFields);

                    foreach (var image in urls)
                    {
                        var newImage = new ImageField
                        {
                            Url = image.Url,
                            Status = "Active",
                            FieldId = updateField.Id,
                            PublicId = image.PublicId
                        };

                        _unitOfWork.ImageFieldRepository.PrepareCreate(newImage);
                    }

                    var result = await _unitOfWork.ImageFieldRepository.SaveAsync();
                    if (result <= 0)
                    {
                        return new BusinessResult
                        {
                            Status = 500,
                            Message = "Create image failed !",
                            Data = null
                        };
                    }
                }

                return new BusinessResult
                {
                    Status = 200,
                    Message = "Update successfull!",
                    Data = null
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
                var field = await _unitOfWork.FieldRepository.GetByIdAsync(id);
                if (field == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found Field!",
                        Data = null
                    };
                }

                field.Status = field.Status == "Inactive" ? "Active" : "Inactive";
                var rs = await _unitOfWork.FieldRepository.UpdateAsync(field);
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

        public async Task<IBusinessResult> RemoveField(int id)
        {
            try
            {
                var field = await _unitOfWork.FieldRepository.GetByIdAsync(id);
                if (field == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found!",
                        Data = null
                    };
                }

                var images = _unitOfWork.ImageFieldRepository.FindByCondition(img => img.FieldId == id).ToList();
                if (images != null || images.Any())
                {
                    foreach (var image in images)
                    {
                        await _unitOfWork.ImageFieldRepository.RemoveAsync(image);
                    }
                }

                var result = await _unitOfWork.FieldRepository.RemoveAsync(field);
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
