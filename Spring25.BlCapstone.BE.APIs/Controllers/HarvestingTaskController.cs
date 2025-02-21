using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api")]
    [ApiController]
    public class HarvestingTaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHarvestingTaskService _harvestingTaskService;
        public HarvestingTaskController(IMapper mapper, IHarvestingTaskService harvestingTaskService)
        {
            _mapper = mapper;
            _harvestingTaskService = harvestingTaskService;
        }
        [HttpGet("harvesting-tasks")]
        public async Task<IActionResult> GetAllHarvestingTasks()
        {
            try
            {
                var result = await _harvestingTaskService.GetHarvestingTasks();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("harvesting-tasks/{id}")]
        public async Task<IActionResult> GetHarvestingTaskById([FromRoute]int id)
        {
            try
            {
                var result = await _harvestingTaskService.GetHarvestingTaskById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("harvesting-tasks/{id}/detail")]
        public async Task<IActionResult> GetDetailHarvestingTaskById([FromRoute] int id)
        {
            try
            {
                var result = await _harvestingTaskService.GetHarvestingTaskDetailById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
