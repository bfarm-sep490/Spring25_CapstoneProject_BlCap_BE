﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect;
using Spring25.BlCapstone.BE.Services.Untils;
using Spring25.BlCapstone.BE.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IInspectingFormService
    {
        Task<IBusinessResult> GetAllInspectingForm(int? planId, int? inspectorId, List<string>? status);
        Task<IBusinessResult> GetInspectingFormById(int id);
        Task<IBusinessResult> CreateInspectingForm(CreateInspectingPlan model);
        Task<IBusinessResult> UpdateInspectingForm(int id, UpdateInspectingForm model);
        Task<IBusinessResult> DeleteForm(int id);
    }

    public class InspectingFormService : IInspectingFormService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public InspectingFormService(IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IBusinessResult> CreateInspectingForm(CreateInspectingPlan model)
        {
            try
            {
                var form = _mapper.Map<InspectingForm>(model);

                form.Status = model.Status != null ? model.Status : "Draft";
                form.CreatedAt = DateTimeHelper.NowVietnamTime();

                var rs = await _unitOfWork.InspectingFormRepository.CreateAsync(form);
                var result = _mapper.Map<InspectingFormModel>(rs);
                if (rs != null)
                {
                    return new BusinessResult(200, "Create form successfull", result);
                }
                else
                {
                    return new BusinessResult(500, "Create failed!");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAllInspectingForm(int? planId, int? inspectorId, List<string>? status)
        {
            var list = await _unitOfWork.InspectingFormRepository.GetInspectingForms(planId, inspectorId, status: status);
            var result = _mapper.Map<List<InspectingFormModel>>(list);
            return new BusinessResult(200,"Get all Inspecting forms",result);
        }

        public async Task<IBusinessResult> GetInspectingFormById(int id)
        {
            var obj = await _unitOfWork.InspectingFormRepository.GetInspectingForms(formId: id);
            if (obj.Count <= 0) return new BusinessResult(400, "Not Found this object");
            var result = _mapper.Map<List<InspectingFormModel>>(obj);
            return new BusinessResult(200, "Get Inspecting form by id", result);
        }

        public async Task<IBusinessResult> UpdateInspectingForm(int id, UpdateInspectingForm model)
        {
            try
            {
                var form = await _unitOfWork.InspectingFormRepository.GetByIdAsync(id);
                if (form == null)
                {
                    return new BusinessResult(400, "Not found any Inspecting form");
                }

                model.Status = model.Status != null ? model.Status : form.Status;
                _mapper.Map(model, form);
                form.UpdatedAt = DateTimeHelper.NowVietnamTime();

                var rs = await _unitOfWork.InspectingFormRepository.UpdateAsync(form);
                
                if (rs > 0)
                {
                    var result = _mapper.Map<InspectingFormModel>(form);
                    return new BusinessResult(200, "Update successfully!", form);
                }
                else
                {
                    return new BusinessResult(500, "Update failed!");
                }
            } 
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> DeleteForm(int id)
        {
            try
            {
                var inspectingForm = await _unitOfWork.InspectingFormRepository.GetByIdAsync(id);
                if (inspectingForm == null)
                {
                    return new BusinessResult(400, "Not found any Inspecting Form");
                }

                var rs = await _unitOfWork.InspectingFormRepository.RemoveAsync(inspectingForm);
                if (rs)
                {
                    return new BusinessResult(200, "Remove successfully!");
                }
                else
                {
                    return new BusinessResult(500, "Remove failed!");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
