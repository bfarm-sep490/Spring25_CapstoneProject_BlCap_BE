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

        public Task<IBusinessResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IBusinessResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IBusinessResult> Update(YieldModel model)
        {
            throw new NotImplementedException();
        }
    }
}
