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
        public async Task<IActionResult> GetHarvestingProduction(int? plan_id)
        {
            try
            {
                var result = await _harvestingTaskService.GetListProductionHarvestingTask(plan_id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
