using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IFarmerService
    {
        public Task<IBusinessResult> GetAll();
    }

    public class FarmerService : IFarmerService
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        public FarmerService(IMapper mapper)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper=mapper;
        }

        public async Task<IBusinessResult> GetAll()
        {
           var list = await _unitOfWork.FarmerRepository.GetAllAsync();
           var result = _mapper.Map<List<FarmerModel>>(list);
           return new BusinessResult(200, "List Farmer", result);
        }
    }
}
