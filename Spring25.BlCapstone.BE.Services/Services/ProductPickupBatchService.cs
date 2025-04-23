using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IProductPickupBatchService
    {
        Task<IBusinessResult> GetAll(int? orderId);
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> CreateNewBatch(BatchCreateModel model);
    }

    public class ProductPickupBatchService : IProductPickupBatchService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductPickupBatchService(IMapper mapper)
        {
            _unitOfWork = new UnitOfWork();
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAll(int? orderId)
        {
            try
            {
                var batches = await _unitOfWork.ProductPickupBatchRepository.GetAllBatches(orderId);
                var rs = _mapper.Map<List<BatchModel>>(batches);
                return new BusinessResult(200, "List batches: ", rs);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var batch = await _unitOfWork.ProductPickupBatchRepository.GetByIdAsync(id);
                if (batch == null)
                {
                    return new BusinessResult(400, "Not found any batches !");
                }

                var rs = _mapper.Map<BatchModel>(batch);
                return new BusinessResult(200, "Batch By Id: ", rs);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> CreateNewBatch(BatchCreateModel model)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.ProductHasOrderOrNo(model.ProductId);
                if (!order)
                {
                    return new BusinessResult(400, "This product does not have any orders !");
                }

                var canCreate = await _unitOfWork.PackagingProductRepository.CanCreateNewPickupBatch(model.ProductId, model.Quantity);
                if (!canCreate)
                {
                    return new BusinessResult(400, "Can not create batch because the received quantity exceed the pre-order quantity");
                }

                var rs = _mapper.Map<ProductPickupBatch>(model);
                rs.CreatedDate = DateTime.Now;
                await _unitOfWork.ProductPickupBatchRepository.CreateAsync(rs);
                return new BusinessResult(200, "Create success!", model);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
