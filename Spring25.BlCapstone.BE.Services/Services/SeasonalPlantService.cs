using AutoMapper;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Template;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plan;
using Spring25.BlCapstone.BE.Services.BusinessModels.Template;
using Spring25.BlCapstone.BE.Services.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface ISeasonalPlantService
    {
        Task<IBusinessResult> CreateTemplate(RequestTemplate model);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> GetAllTemplate(int? plantId, string? seasonType, DateTime? start, DateTime? end);
        Task<IBusinessResult> GetTemplateById(int id);
        Task<IBusinessResult> GetTemplatesByPlantsId(int plant_id);
        Task<IBusinessResult> ImportByExcel(IFormFile file);
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

        public async Task<IBusinessResult> ImportByExcel(IFormFile file)
        {
            var result = new RequestTemplate();
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            if (file.FileName != "TemplateForm.xlsx") { return new BusinessResult(400, "Upload fail, please reupload file"); }
            ExcelPackage.License.SetNonCommercialPersonal("BFarmX");
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == "Template");
                if (worksheet == null) { return new BusinessResult(400, "Template do not have Sheet Template"); }
                result.Description = ExcelHelper.ReadCellValue(worksheet, 8, 6);
                result.PlantId = int.Parse(ExcelHelper.ReadCellValue(worksheet, 3, 1));
                result.SeasonType = ExcelHelper.ReadCellValue(worksheet, 6, 6);
                result.StartDate = DateTime.Parse(ExcelHelper.ReadCellValue(worksheet, 7, 8));
                result.EndDate = DateTime.Parse(ExcelHelper.ReadCellValue(worksheet, 7, 13));
                result.EstimatedPerOne = float.Parse(ExcelHelper.ReadCellValue(worksheet, 11, 6));
                result.DurationDays = int.Parse(ExcelHelper.ReadCellValue(worksheet,10 , 6));
                result.Template = new PlanTemplate();
                result.Template.SampleQuantity = int.Parse(ExcelHelper.ReadCellValue(worksheet, 10, 6));
                result.Template.CaringTasks = new List<CaringTaskTemplate>();
                result.Template.HarvestingTaskTemplates = new List<HarvestingTaskTemplate>();
                result.Template.InspectingTasks = new List<InspectingTaskTemplate>();
                result.Template.SeasonType = result.SeasonType;
                var x = 17;
                do
                {
                    var count = ExcelHelper.GetMergedCellSize(worksheet, x, 1);
                    var caringTask = new CaringTaskTemplate();
                    caringTask.TaskName = ExcelHelper.ReadCellValue(worksheet, x, 2);
                    caringTask.TaskType = ExcelHelper.ReadCellValue(worksheet, x, 4);
                    caringTask.StartIn = int.Parse(ExcelHelper.ReadCellValue(worksheet, x, 9));
                    caringTask.EndIn = int.Parse(ExcelHelper.ReadCellValue(worksheet, x, 13));
                    caringTask.Description = ExcelHelper.ReadCellValue(worksheet, x, 14);
                    x = x + 3;
                    for (int i = 0; i < count.Rows - 3; i++)
                    {
                        var fertilizerId = int.Parse(ExcelHelper.ReadCellValue(worksheet, x, 2));
                        if (fertilizerId != 0 && !ExcelHelper.CheckedCellIsNull(worksheet, x, 2))
                        {
                            var caringFertilizer = new FertilizerTemplate();
                            caringFertilizer.FertilizerId = fertilizerId;
                            caringFertilizer.Quantity = float.Parse(ExcelHelper.ReadCellValue(worksheet, x, 4));
                            caringFertilizer.Unit = ExcelHelper.ReadCellValue(worksheet, x, 5);
                            caringTask.Fertilizers.Add(caringFertilizer);
                        }
                        var pesticideId = int.Parse(ExcelHelper.ReadCellValue(worksheet, x, 6));
                        if (pesticideId != 0 && !ExcelHelper.CheckedCellIsNull(worksheet, x, 6))
                        {
                            var caringPesticide = new PesticideTemplate();
                            caringPesticide.PesticideId = pesticideId;
                            caringPesticide.Quantity = float.Parse(ExcelHelper.ReadCellValue(worksheet, x, 8));
                            caringPesticide.Unit = ExcelHelper.ReadCellValue(worksheet, x, 9);
                            caringTask.Pesticides.Add(caringPesticide);
                        }
                        var itemid = int.Parse(ExcelHelper.ReadCellValue(worksheet, x, 10));
                        if (itemid != 0 && !ExcelHelper.CheckedCellIsNull(worksheet, x, 10))
                        {
                            var caringItem = new ItemTemplate();
                            caringItem.ItemId = itemid;
                            caringItem.Quantity = int.Parse(ExcelHelper.ReadCellValue(worksheet, x, 12));
                            caringItem.Unit = ExcelHelper.ReadCellValue(worksheet, x, 13);
                            caringTask.Items.Add(caringItem);
                        }
                        x = x + 1;
                    }
                    result.Template.CaringTasks.Add(caringTask);

                }
                while (
                 !ExcelHelper.CheckedCellIsNull(worksheet, x, 1)
                );
                x = x + 3;
                do
                {
                    var inspectingForm = new InspectingTaskTemplate();
                    inspectingForm.FormName = ExcelHelper.ReadCellValue(worksheet, x, 2);
                    inspectingForm.StartIn = int.Parse(ExcelHelper.ReadCellValue(worksheet, x, 9));
                    inspectingForm.EndIn = int.Parse(ExcelHelper.ReadCellValue(worksheet, x, 13));
                    inspectingForm.Description = ExcelHelper.ReadCellValue(worksheet, x, 14);
                    result.Template.InspectingTasks.Add(inspectingForm);
                    x = x + 1;
                }
                while (
                 !ExcelHelper.CheckedCellIsNull(worksheet, x, 1)
                );
              
                x = x + 3;
                if (ExcelHelper.CheckedCellIsNull(worksheet, x, 1)) return new BusinessResult(400, "Template do not have harvesting task");
                do
                {
                    var harvestingForm = new HarvestingTaskTemplate();
                    var harvestCount = ExcelHelper.GetMergedCellSize(worksheet, x, 1);
                    harvestingForm.TaskName = ExcelHelper.ReadCellValue(worksheet, x, 2);
                    harvestingForm.StartIn =int.Parse(ExcelHelper.ReadCellValue(worksheet, x, 9));
                    harvestingForm.EndIn = int.Parse(ExcelHelper.ReadCellValue(worksheet, x, 13));
                    harvestingForm.Description = ExcelHelper.ReadCellValue(worksheet,x, 14);
                    x = x + 3;
                    for (int i = 0; i < harvestCount.Rows - 3; i++)
                    {
                        var harvestingItemid = int.Parse(ExcelHelper.ReadCellValue(worksheet, x, 10));
                        if (harvestingItemid != 0 && !ExcelHelper.CheckedCellIsNull(worksheet, x, 10))
                        {
                            var harvestingItem = new ItemTemplate();
                            harvestingItem.ItemId = harvestingItemid;
                            harvestingItem.Quantity = int.Parse(ExcelHelper.ReadCellValue(worksheet, x, 12));
                            harvestingItem.Unit = ExcelHelper.ReadCellValue(worksheet, x, 13);
                            harvestingForm.Items.Add(harvestingItem);
                        }
                        x = x + 1;
                    }
                    result.Template.HarvestingTaskTemplates.Add(harvestingForm);
                }
                while (
                !ExcelHelper.CheckedCellIsNull(worksheet, x, 1)
                );           
            }
            return new BusinessResult(200, "data", result);
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
