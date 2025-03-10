﻿using Microsoft.AspNetCore.Http;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Item;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IItemService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> CreateItem(CreatedItem item);
        Task<IBusinessResult> UpdateItem(int id, CreatedItem item);
        Task<IBusinessResult> RemoveItem(int id);
        Task<IBusinessResult> ToggleActiveInactive(int id);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
    }

    public class ItemService : IItemService
    {
        private readonly UnitOfWork _unitOfWork;
        public ItemService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var items = await _unitOfWork.ItemRepository.GetAllAsync();
                var res = items.Select(i => new ItemModels
                {
                    Id = i.Id,
                    Description = i.Description,
                    Image = i.Image,
                    Name = i.Name,
                    Status = i.Status,
                    Type = i.Type,
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                }).ToList();
                if (res.Count <= 0)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any Items!",
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
                var item = await _unitOfWork.ItemRepository.GetByIdAsync(id);

                if (item == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any Items!",
                        Data = null
                    };
                }

                var res = new ItemModels
                {
                    Id = item.Id,
                    Description = item.Description,
                    Image = item.Image,
                    Name = item.Name,
                    Status = item.Status,
                    Type = item.Type,
                    Quantity = item.Quantity,
                    Unit = item.Unit,
                };

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

        public async Task<IBusinessResult> CreateItem(CreatedItem item)
        {
            try
            {
                var newItem = new Item
                {
                    Name = item.Name,
                    Description = item.Description,
                    Status = item.Status,
                    Type = item.Type,
                    Image = item.Image, 
                    Quantity = item.Quantity,
                    Unit = item.Unit,
                };

                var rs = await _unitOfWork.ItemRepository.CreateAsync(newItem);

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

        public async Task<IBusinessResult> UpdateItem(int id, CreatedItem item)
        {
            try
            {
                var existedItem = await _unitOfWork.ItemRepository.GetByIdAsync(id);
                if (existedItem == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any Items!",
                        Data = null
                    };
                }

                existedItem.Name = item.Name;
                existedItem.Description = item.Description;
                existedItem.Status = item.Status;
                existedItem.Type = item.Type;
                existedItem.Image = item.Image;
                existedItem.Quantity = item.Quantity;
                existedItem.Unit = item.Unit;

                var rs = await _unitOfWork.ItemRepository.UpdateAsync(existedItem);
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

        public async Task<IBusinessResult> RemoveItem(int id)
        {
            try
            {
                var item = await _unitOfWork.ItemRepository.GetByIdAsync(id);

                if (item == null)
                {
                    return new BusinessResult
                    {
                        Status = 404,
                        Message = "Not found any items!",
                        Data = null
                    };
                }

                var result = await _unitOfWork.ItemRepository.RemoveAsync(item);

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

        public async Task<IBusinessResult> ToggleActiveInactive(int id)
        {
            try
            {
                var item = await _unitOfWork.ItemRepository.GetByIdAsync(id);

                if (item == null)
                {
                    return new BusinessResult { Status = 404, Message = "Not found any Items !", Data = null };
                }

                item.Status = item.Status == "Inactive" ? "Active" : "Inactive";
                var rs = await _unitOfWork.ItemRepository.UpdateAsync(item);

                if (rs > 0)
                {
                    return new BusinessResult { Status = 200, Message = "Deactivate item successful", Data = null };
                }
                else
                {
                    return new BusinessResult { Status = 500, Message = "Deactivate failed !", Data = null };
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }
    }
}