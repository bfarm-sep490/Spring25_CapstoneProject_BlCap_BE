﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Package;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IPackagingTaskService
    {
        Task<IBusinessResult> CreatePackagingTask(CreatePackagingPlan model);
        Task<IBusinessResult> GetPackagingTasks(int? planId, int? farmerId);
        Task<IBusinessResult> GetPackagingTaskById(int id);
        Task<IBusinessResult> UploadImage(List<IFormFile> file);
        Task<IBusinessResult> ReportPackagingTask(int id, PackagingReport model);
        Task<IBusinessResult> UpdatePackagingTask(int id, UpdatePackaging model);
        Task<IBusinessResult> DeleteTask(int id);
        Task<IBusinessResult> GetHistoryFarmers(int id);
    }
    public class PackagingTaskService : IPackagingTaskService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public PackagingTaskService(IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetPackagingTasks(int? planId, int? farmerId)
        {
            try
            { 
                var packs = await _unitOfWork.PackagingTaskRepository.GetPackagingTasks(planId, farmerId);
                var rs = _mapper.Map<List<PackagingTaskModel>>(packs);

                return new BusinessResult { Status = 200, Message = "List of packaging tasks:", Data = rs };
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }

        public async Task<IBusinessResult> GetPackagingTaskById(int id)
        {
            try
            {
                var task = await _unitOfWork.PackagingTaskRepository.GetPackagingTasks(taskId: id);
                if (task.Count <= 0)
                {
                    return new BusinessResult(404, "Not found any Packaging Task");
                }
                var rs = _mapper.Map<List<PackagingTaskModel>>(task);

                return new BusinessResult { Status = 200, Message = "List of packaging tasks:", Data = rs };
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
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

        public async Task<IBusinessResult> ReportPackagingTask(int id, PackagingReport model)
        {
            try
            {
                var task = await _unitOfWork.PackagingTaskRepository.GetByIdAsync(id);
                if (task == null)
                {
                    return new BusinessResult(404, "Not found any Packaging tasks");
                }

                var harvestingTask = await _unitOfWork.HarvestingTaskRepository.GetByIdAsync(model.HarvestingTaskId);
                if (harvestingTask == null)
                {
                    return new BusinessResult(404, "Not found any Harvesting Task !");
                }

                _mapper.Map(model, task);
                task.CompleteDate = DateTime.Now;
                task.UpdatedAt = DateTime.Now;

                var rs = await _unitOfWork.PackagingTaskRepository.UpdateAsync(task);

                var images = await _unitOfWork.PackagingImageRepository.GetPackagingImagesByTaskId(id);
                if (!images.Any() || images.Count > 0)
                {
                    foreach (var image in images)
                    {
                        await _unitOfWork.PackagingImageRepository.RemoveAsync(image);
                    }
                }

                var type = await _unitOfWork.PackagingTypeRepository.GetByIdAsync(task.PackagingTypeId.Value);
                float isSpare = model.TotalPackagedWeight - (model.PackagedItemCount * type.QuantityPerPack);

                if (isSpare == 0)
                {
                    _unitOfWork.PackagingProductRepository.PrepareCreate(new PackagingProduct
                    {
                        PackagingTaskId = task.Id,
                        HarvestingTaskId = model.HarvestingTaskId,
                        QuantityPerPack = type.QuantityPerPack,
                        PackQuantity = model.PackagedItemCount,
                        //QR code blockchain ne
                        QRCode = "?",
                        Status = "Active"
                    });
                }
                else if (isSpare < 0)
                {
                    _unitOfWork.PackagingProductRepository.PrepareCreate(new PackagingProduct
                    {
                        PackagingTaskId = task.Id,
                        HarvestingTaskId = model.HarvestingTaskId,
                        QuantityPerPack = type.QuantityPerPack,
                        PackQuantity = model.PackagedItemCount - 1,
                        //QR code blockchain ne
                        QRCode = "?",
                        Status = "Active"
                    });

                    _unitOfWork.PackagingProductRepository.PrepareCreate(new PackagingProduct
                    {
                        PackagingTaskId = task.Id,
                        HarvestingTaskId = model.HarvestingTaskId,
                        QuantityPerPack = Math.Abs(isSpare),
                        PackQuantity = 1,
                        //QR code blockchain ne
                        QRCode = "?",
                        Status = "Active"
                    });
                }

                await _unitOfWork.PackagingProductRepository.SaveAsync();

                if (model.Images != null && model.Images.Any())
                {
                    foreach (var image in model.Images)
                    {
                        await _unitOfWork.PackagingImageRepository.CreateAsync(new Repositories.Models.PackagingImage
                        {
                            TaskId = id,
                            Url = image
                        });
                    }
                }

                if (rs > 0)
                {
                    return new BusinessResult(200, "Update successfully!", task);
                }
                else
                {
                    return new BusinessResult(500, "Update failed!");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> UpdatePackagingTask(int id, UpdatePackaging model)
        {
            try
            {
                var task = await _unitOfWork.PackagingTaskRepository.GetByIdAsync(id);
                if (task == null)
                {
                    return new BusinessResult(404, "Not found any Packaging tasks");
                }

                model.Status = model.Status != null ? model.Status : task.Status;
                _mapper.Map(model, task);
                task.UpdatedAt = DateTime.Now;

                var rs = await _unitOfWork.PackagingTaskRepository.UpdateAsync(task);

                var items = await _unitOfWork.PackagingItemRepository.GetPackagingItemsByTaskId(id);
                foreach (var item in items)
                {
                    await _unitOfWork.PackagingItemRepository.RemoveAsync(item);
                }

                if (model.Items != null)
                {
                    foreach (var i in model.Items)
                    {
                        await _unitOfWork.PackagingItemRepository.CreateAsync(new PackagingItem
                        {
                            TaskId = task.Id,
                            ItemId = i.ItemId,
                            Quantity = i.Quantity,
                            Unit = i.Unit,
                        });
                    }
                }

                if (rs > 0)
                {
                    return new BusinessResult(200, "Update successfully!", task);
                }
                else
                {
                    return new BusinessResult(500, "Update failed!");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> CreatePackagingTask(CreatePackagingPlan model)
        {
            try
            {
                var task = _mapper.Map<PackagingTask>(model);
                task.Status = model.Status != null ? model.Status : "Draft";
                task.CreatedAt = DateTime.Now;

                var rs = await _unitOfWork.PackagingTaskRepository.CreateAsync(task);
                if (model.Items != null)
                {
                    foreach (var i in model.Items)
                    {
                        await _unitOfWork.PackagingItemRepository.CreateAsync(new PackagingItem
                        {
                            TaskId = task.Id,
                            ItemId = i.ItemId,
                            Quantity = i.Quantity,
                            Unit = i.Unit,
                        });
                    }
                }

                if (rs != null)
                {
                    return new BusinessResult(200, "Create task successfull", rs);
                }
                else
                {
                    return new BusinessResult(500, "Create failed!");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> DeleteTask(int id)
        {
            try
            {
                var packagingTask = await _unitOfWork.PackagingTaskRepository.GetByIdAsync(id);
                if (packagingTask == null)
                {
                    return new BusinessResult(404, "Not found any packaging task");
                }

                var packagingItems = await _unitOfWork.PackagingItemRepository.GetPackagingItemsByTaskId(id);
                foreach (var packagingItem in packagingItems)
                {
                    await _unitOfWork.PackagingItemRepository.RemoveAsync(packagingItem);
                }

                var rs = await _unitOfWork.PackagingTaskRepository.RemoveAsync(packagingTask);
                if (rs)
                {
                    return new BusinessResult(200, "Delete successfully");
                }
                else
                {
                    return new BusinessResult(500, "Remove failed");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetHistoryFarmers(int id)
        {
            try
            {
                var task = await _unitOfWork.PackagingTaskRepository.GetByIdAsync(id);
                if (task == null)
                {
                    return new BusinessResult(404, "Not found any Packaging Task");
                }

                var history = await _unitOfWork.FarmerPackagingTaskRepository.GetFarmerPackagingTasks(id);
                if (history.Count <= 0)
                {
                    return new BusinessResult(404, "There are not any farmers in task !");
                }

                var res = _mapper.Map<List<HistoryFarmersTask>>(history);
                return new BusinessResult(200, "List of history: ", res);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
