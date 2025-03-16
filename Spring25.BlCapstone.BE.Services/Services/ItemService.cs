using AutoMapper;
using IO.Ably;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Redis;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Fertilizer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Item;
using Spring25.BlCapstone.BE.Services.BusinessModels.Pesticide;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plant;
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
        Task<IBusinessResult> GetAll(string? status);
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
        private readonly IMapper _mapper;
        private readonly RedisManagement _redisManagement;
        private readonly string key = "ListItems";
        public ItemService(IMapper mapper, RedisManagement redisManagement)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
            _redisManagement = redisManagement;
        }

        public async Task<IBusinessResult> GetAll(string? status)
        {
            List<ItemModel> result;
            try
            {
                if (!_redisManagement.IsConnected) throw new Exception();
                string productListJson = _redisManagement.GetData(key);
                if (productListJson == null || productListJson == "[]")
                {
                    var list = await _unitOfWork.ItemRepository.GetAllAsync();
                    result = _mapper.Map<List<ItemModel>>(list);
                    _redisManagement.SetData(key, JsonConvert.SerializeObject(result));
                }
                else
                {
                    result = JsonConvert.DeserializeObject<List<ItemModel>>(productListJson);
                }
            }
            catch (Exception ex)
            {
                var list = await _unitOfWork.ItemRepository.GetAllAsync();
                result = _mapper.Map<List<ItemModel>>(list);
            }
            if (!string.IsNullOrEmpty(status))
            {
                result = result.Where(f => f.Status.ToLower().Trim() == status.ToLower().Trim()).ToList();
            }
            return new BusinessResult(200,"List Item",result);
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            var obj = new ItemModel();
            try
            {
                if (_redisManagement.IsConnected == false) throw new Exception();
                var listJson = _redisManagement.GetData(key);
                if (!string.IsNullOrEmpty(listJson))
                {
                    var list = JsonConvert.DeserializeObject<List<ItemModel>>(listJson);
                    obj = list.FirstOrDefault(x => x.Id == id);
                    if (obj != null) return new BusinessResult(200, "Item (From Cache)", obj);
                }           
                throw new Exception();         
            }
            catch (Exception ex)
            {
                var item = await _unitOfWork.ItemRepository.GetByIdAsync(id);
                if (item == null) return new BusinessResult(400, "Not Found this Item");
                obj = _mapper.Map<ItemModel>(item);
            }
            return new BusinessResult(200, "Item", obj);
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
                    await this.ResetItemsRedis();
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
                    await this.ResetItemsRedis();
                    return new BusinessResult
                    {
                        Status = 200,
                        Message = "Update success!",
                        Data = _mapper.Map<ItemModel>(item)
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
                    await this.ResetItemsRedis();
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
                    await this.ResetItemsRedis();
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
        private async Task ResetItemsRedis()
        {
            try
            {
                if (_redisManagement.IsConnected == false) throw new Exception("Redis is fail");
                _redisManagement.DeleteData(key);
                var plants = await _unitOfWork.ItemRepository.GetAllAsync();
                var list = _mapper.Map<List<ItemModel>>(plants);
                _redisManagement.SetData(key, JsonConvert.SerializeObject(list));
            }
            catch (Exception ex)
            {

            }
        }
    }
}