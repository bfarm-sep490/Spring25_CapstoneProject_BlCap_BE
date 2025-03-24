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
    public interface IPackagingTypeService
    {
        Task<IBusinessResult> GetAllTypes();
    }

    public class PackagingTypeService : IPackagingTypeService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PackagingTypeService(IMapper mapper)
        {
            _unitOfWork = new UnitOfWork();
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAllTypes()
        {
            try
            {
                var types = await _unitOfWork.PackagingTypeRepository.GetAllAsync();
                if (!types.Any())
                {
                    return new BusinessResult(404, "Not found any Types !");
                }

                var res = _mapper.Map<List<PackagingTypeModel>>(types);
                return new BusinessResult(200, "List of packaging types :", res);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
