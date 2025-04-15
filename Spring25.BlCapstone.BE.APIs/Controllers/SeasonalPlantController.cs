using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Template;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/template")]
    [ApiController]
    public class SeasonalPlantController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISeasonalPlantService _seasonalPlantService;
        public SeasonalPlantController(IMapper mapper,ISeasonalPlantService seasonalPlantService)
        {
            _mapper = mapper;
            _seasonalPlantService = seasonalPlantService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _seasonalPlantService.GetAllTemplate();
                return Ok(result);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);  
            }
        }
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] RequestTemplate model)
        {
            try
            {
                var result = await _seasonalPlantService.CreateTemplate(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            try
            {
                var result = await _seasonalPlantService.GetTemplateById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTemplate([FromRoute] int id, [FromBody] RequestTemplate model)
        {
            try
            {
                var result = await _seasonalPlantService.UpdateTemplate(id,model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveById([FromRoute] int id)
        {
            try
            {
                var result = await _seasonalPlantService.DeleteById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("plants/{plant_id}")]
        public async Task<IActionResult> GetSeasonalPlantsByplantId([FromRoute] int plant_id)
        {
            try
            {
                var result = await _seasonalPlantService.GetTemplatesByPlantsId(plant_id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
