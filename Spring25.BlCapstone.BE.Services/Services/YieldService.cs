﻿using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plant;
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
        Task<IBusinessResult> GetAll(string? status);
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Create(YieldModel model);
        Task<IBusinessResult> Update(int id, YieldModel model);
        Task<IBusinessResult> Delete(int id);
        Task<IBusinessResult> GetSuggestPlantsbyYieldId(int id);
        Task<IBusinessResult> GetHistoryPlan(int id);
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

        public async Task<IBusinessResult> Create(YieldModel model)
        {
            try
            {
                model.Status= "Available";
                var obj = _mapper.Map<Yield>(model);
                var rs = await _unitOfWork.YieldRepository.CreateAsync(obj);
                
                return new BusinessResult(200, "Create successfully !", _mapper.Map<YieldModel>(rs));
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message };
            }
        }

        public Task<IBusinessResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IBusinessResult> GetAll(string? status)
        {
           var list = await _unitOfWork.YieldRepository.GetYields(status);
           var result= _mapper.Map<List<YieldModel>>(list);
            return new BusinessResult(200,"List Yields",result);
            
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            var obj = await _unitOfWork.YieldRepository.GetByIdAsync(id);
            var result= _mapper.Map<YieldModel>(obj);
            return new BusinessResult(200, "Get Yield by Id", result);
        }

        public async Task<IBusinessResult> GetSuggestPlantsbyYieldId(int id)
        {
            var obj = await _unitOfWork.YieldRepository.GetByIdAsync(id);
            if (obj == null) return new BusinessResult(400, "Not Found Yield");
            var list = await _unitOfWork.YieldRepository.GetSuggestPlantsbyYieldId(id);
            var result = _mapper.Map<List<PlantModel>>(list);
            return new BusinessResult(200, "List suggest plant by yieldid", result);
        }

        public async Task<IBusinessResult> Update(int id, YieldModel model)
        {
            try
            {
                var yield = await _unitOfWork.YieldRepository.GetByIdAsync(id);
                if (yield == null)
                {
                    return new BusinessResult(400, "Not found any yields");
                }

                _mapper.Map(model, yield);
                yield.Id = id;

                var rs = await _unitOfWork.YieldRepository.UpdateAsync(yield);
                return new BusinessResult(200, "Update successfully!", model);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetHistoryPlan(int id)
        {
            try
            {
                var plan = await _unitOfWork.YieldRepository.GetHistoryPlan(id);
                if (!plan.Any())
                {
                    return new BusinessResult(400, "Not found any plans ! This yield is new !!!!");
                }

                var res = _mapper.Map<List<HistoryPlans>>(plan);
                return new BusinessResult(200, "List history in yield: ", res);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
