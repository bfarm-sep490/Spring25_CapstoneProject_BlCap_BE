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
        Task<IBusinessResult> GetAll();
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

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var products = await _unitOfWork.PackagingProductRepository.GetPackagingProducts();
                if (!products.Any())
                {
                    return new BusinessResult(404, "There are not any products !");
                }

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
                    return new BusinessResult(404, "There are not any products !");
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
