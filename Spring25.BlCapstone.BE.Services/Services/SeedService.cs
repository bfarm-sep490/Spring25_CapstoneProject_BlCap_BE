using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface ISeedService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Create(SeedModel model);
        Task<IBusinessResult> Update(SeedModel model);
        Task<IBusinessResult> Delete(int id);
        
    }
    public class SeedService:ISeedService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SeedService(UnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<IBusinessResult> Create(SeedModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IBusinessResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IBusinessResult> GetAll()
        {
           var list = await _unitOfWork.SeedRepository.GetAllAsync();
            return new BusinessResult(200, "Get all seeds", list);
        }

        public Task<IBusinessResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IBusinessResult> Update(SeedModel model)
        {
            throw new NotImplementedException();
        }
    }
}
