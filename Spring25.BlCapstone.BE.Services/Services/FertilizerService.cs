using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Fertilizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IFertilizerService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Update(int id, FertilizerModel model);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> Create(FertilizerModel model);
    }
    public class FertilizerService : IFertilizerService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FertilizerService(IMapper mapper)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
        }

        public async Task<IBusinessResult> Create(FertilizerModel model)
        {
            var obj = _mapper.Map<Fertilizer>(model);
            obj.Status = "true";
            var result = await _unitOfWork.FertilizerRepository.CreateAsync(obj);
            return new BusinessResult(200, "Create Fertilizer successfully", result);
        }
        public async Task<IBusinessResult> DeleteById(int id)
        {
            var obj = await _unitOfWork.FertilizerRepository.GetByIdAsync(id);
            if (obj == null) { return new BusinessResult(404, "Do not have this Fertilizer"); }
            var result = await _unitOfWork.FertilizerRepository.RemoveAsync(obj);
            if (result) { return new BusinessResult(1, "Remove Fertilizer successfully", result); }
            return new BusinessResult(200, "Remove Fertilizer fail!", result);
        }

        public async Task<IBusinessResult> GetAll()
        {
            var list = await _unitOfWork.FertilizerRepository.GetAllAsync();
            var result = _mapper.Map<List<FertilizerModel>>(list);
            return new BusinessResult(200, "List Fertilizer", result);
        }
        public async Task<IBusinessResult> GetById(int id)
        {
            var obj = await _unitOfWork.FertilizerRepository.GetByIdAsync(id);
            if (obj == null) { return new BusinessResult(404, "Not found this Fertilizer"); }
            var result = _mapper.Map<FertilizerModel>(obj);
            return new BusinessResult(200, "Fertilizer", result);
        }
        public async Task<IBusinessResult> Update(int id, FertilizerModel model)
        {
            var obj = await _unitOfWork.FertilizerRepository.GetByIdAsync(id);
            if (obj == null) { return new BusinessResult(404, "Not found this Fertilizer"); }
            _mapper.Map(model, obj);
            obj.Id = id;
            var result = await _unitOfWork.FertilizerRepository.UpdateAsync(obj);
            if (result != 0) { return new BusinessResult(200, "Update Fertilizer successfully", obj); }
            else return new BusinessResult(500, "Update Fertilizer Fail");
        }
    }
}
