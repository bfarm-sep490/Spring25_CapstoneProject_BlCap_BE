using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Seed;
using Spring25.BlCapstone.BE.Services.BusinessModels.Yield;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IYieldService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Create(YieldModel model);
        Task<IBusinessResult> Update(YieldModel model);
        Task<IBusinessResult> Delete(int id);
    }
    public class YieldService : IYieldService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public YieldService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<IBusinessResult> Create(YieldModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IBusinessResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IBusinessResult> GetAll()
        {
           var list = await _unitOfWork.YieldRepository.GetAllAsync();
           var result= _mapper.Map<List<YieldModel>>(list);
            return new BusinessResult(200,"List Yields",result);
            
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            var obj = await _unitOfWork.YieldRepository.GetByIdAsync(id);
            var result= _mapper.Map<YieldModel>(obj);
            return new BusinessResult(200, "Get Yield by Id", result);
        }

        public Task<IBusinessResult> Update(YieldModel model)
        {
            throw new NotImplementedException();
        }
    }
}
