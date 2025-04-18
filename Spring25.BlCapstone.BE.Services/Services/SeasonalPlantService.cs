using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Template;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plant;
using Spring25.BlCapstone.BE.Services.BusinessModels.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface ISeasonalPlantService
    {
        Task<IBusinessResult> CreateTemplate(RequestTemplate model);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> GetAllTemplate(int? plantId, string? seasonType, DateTime? start, DateTime? end);
        Task<IBusinessResult> GetTemplateById(int id);
        Task<IBusinessResult> GetTemplatesByPlantsId(int plant_id);
        Task<IBusinessResult> UpdateTemplate(int id, RequestTemplate model);
    }
    public class SeasonalPlantService : ISeasonalPlantService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        public SeasonalPlantService(IMapper mapper) {
            _mapper = mapper;
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> CreateTemplate(RequestTemplate model)
        {
            var plant = await _unitOfWork.PlantRepository.GetByIdAsync(model.PlantId);
            if (plant == null) return new BusinessResult(400, "Not found this Plant");
            var templatePlan = JsonSerializer.Serialize(model.Template, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            var obj = _mapper.Map<SeasonalPlant>(model);
            obj.TemplatePlan = templatePlan;
            var result = await _unitOfWork.SeasonalPlantRepository.CreateAsync(obj);
            var template = _mapper.Map<TemplateModel>(obj);
            template.Template = JsonSerializer.Deserialize<PlanTemplate>(obj.TemplatePlan);
            return new BusinessResult(200,"Create successfully", template);
        }

        public async Task<IBusinessResult> DeleteById(int id)
        {
            var obj = await _unitOfWork.SeasonalPlantRepository.GetByIdAsync(id);
            if (obj == null) return new BusinessResult(200, "Not found this Season Plant");
            var result = await _unitOfWork.SeasonalPlantRepository.RemoveAsync(obj);
            return new BusinessResult(200, "Remove successfully");
        }

        public async Task<IBusinessResult> GetAllTemplate(int? plantId, string? seasonType, DateTime? start, DateTime? end)
        {
            var list = await _unitOfWork.SeasonalPlantRepository.GetAllSeasonalPlants(plantId, seasonType, start, end);
            var result = _mapper.Map<List<TemplateModel>>(list);
            return new BusinessResult(200,"List Seasonal Plants", result);
        }

        public async Task<IBusinessResult> GetTemplateById(int id)
        {
            var obj = await _unitOfWork.SeasonalPlantRepository.GetByIdAsync(id);
            if (obj == null) return new BusinessResult(200, "Not found this Season Plant");
            var result = _mapper.Map<TemplateModel>(obj);
            result.Template = JsonSerializer.Deserialize<PlanTemplate>(obj.TemplatePlan);
            return new BusinessResult(200,"Get Season Template by Id",result);
        }

        public async Task<IBusinessResult> GetTemplatesByPlantsId(int plant_id)
        {
            var list = await _unitOfWork.SeasonalPlantRepository.GetSeasonalPlantsByPlantId(plant_id);
            var result = _mapper.Map<List<TemplateModel>>(list);
            return new BusinessResult(200, "List Seasonal Plants by Plant Id", result);
        }

        public async Task<IBusinessResult> UpdateTemplate(int id, RequestTemplate model)
        {
            var plant = await _unitOfWork.PlantRepository.GetByIdAsync(model.PlantId);
            if (plant == null) return new BusinessResult(400, "Not found this Plant");
            var obj = await _unitOfWork.SeasonalPlantRepository.GetByIdAsync(id);
            if (obj == null) return new BusinessResult(400, "Not found this template");
            var templatePlan = JsonSerializer.Serialize(model.Template, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            _mapper.Map(model,obj);
            obj.TemplatePlan = templatePlan;
            var result = await _unitOfWork.SeasonalPlantRepository.UpdateAsync(obj);
            var template = _mapper.Map<TemplateModel>(obj);
            template.Template = JsonSerializer.Deserialize<PlanTemplate>(obj.TemplatePlan);
            return new BusinessResult(200, "Update successfully", template);
        }
    }
}
