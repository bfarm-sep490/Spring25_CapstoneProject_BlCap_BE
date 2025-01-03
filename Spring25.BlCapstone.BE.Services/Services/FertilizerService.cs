using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IFertilizerService
    {
        Task<IBusinessResult> GetAll();
    }
    public class FertilizerService : IFertilizerService
    {
        private readonly UnitOfWork _unitOfWork;
        public FertilizerService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            var result = await _unitOfWork.FertilizerRepository.GetAllAsync();
            return new BusinessResult(1, "List", result);
        }
    }
}
