using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
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
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> CreateNewType(CreateTypeModel model);
        Task<IBusinessResult> SwitchStatus(int id);
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

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var type = await _unitOfWork.PackagingTypeRepository.GetByIdAsync(id);
                if (type == null)
                {
                    return new BusinessResult(404, "Not found any Types !");
                }

                var res = _mapper.Map<PackagingTypeModel>(type);
                return new BusinessResult(200, $"Packaging type {id} :", res);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> CreateNewType(CreateTypeModel model)
        {
            try
            {
                var type = _mapper.Map<PackagingType>(model);
                type.Status = "Active";

                await _unitOfWork.PackagingTypeRepository.CreateAsync(type);
                var rs = _mapper.Map<PackagingTypeModel>(type);

                return new BusinessResult(200, "Create successfull !", rs);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> SwitchStatus(int id)
        {
            try
            {
                var type = await _unitOfWork.PackagingTypeRepository.GetByIdAsync(id);
                if (type == null)
                {
                    return new BusinessResult(404, "Not found any Types !");
                }

                type.Status = type.Status.ToLower().Trim().Equals("active") ? "Inactive" : "Active";
                await _unitOfWork.PackagingTypeRepository.UpdateAsync(type);
                var rs = _mapper.Map<PackagingTypeModel>(type);

                return new BusinessResult(200, "Switch success !", rs);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
