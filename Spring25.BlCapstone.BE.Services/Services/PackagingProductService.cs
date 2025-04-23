using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IPackagingProductService
    {
        Task<IBusinessResult> GetAll(int? planId, string? status, int? harvestingTaskId, int? orderId);
        Task<IBusinessResult> GetById(int id);
    }

    public class PackagingProductService : IPackagingProductService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PackagingProductService(IMapper mapper)
        {
            _unitOfWork = new UnitOfWork();
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAll(int? planId, string? status, int? harvestingTaskId, int? orderId)
        {
            try
            {
                var products = await _unitOfWork.PackagingProductRepository.GetPackagingProducts(planId, status, harvestingTaskId, orderId);
                var rs = _mapper.Map<List<PackagingProductionModel>>(products);
                return new BusinessResult(200, "List of productions:", rs);
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
                var product = await _unitOfWork.PackagingProductRepository.GetProductById(id);
                if (product == null)
                {
                    return new BusinessResult(400, "There are not any products !");
                }

                var rs = _mapper.Map<PackagingProductionModel>(product);
                return new BusinessResult(200, "Product :", rs);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
