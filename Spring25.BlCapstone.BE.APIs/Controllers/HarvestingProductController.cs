using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/harvesting-product")]
    [ApiController]
    public class HarvestingProductController : ControllerBase
    {
        private readonly IHarvestingTaskService _harvestingTaskService;
        public HarvestingProductController(IHarvestingTaskService harvestingTaskService)
        {
            _harvestingTaskService = harvestingTaskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHarvestingProductions(int? plan_id, int? packaging_task_id, string? status)
        {
            try
            {
                var result = await _harvestingTaskService.GetListProductionHarvestingTask(plan_id, packaging_task_id, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHarvestingProduction(int id)
        {
            try
            {
                var result = await _harvestingTaskService.GetProductionHarvestingTask(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
