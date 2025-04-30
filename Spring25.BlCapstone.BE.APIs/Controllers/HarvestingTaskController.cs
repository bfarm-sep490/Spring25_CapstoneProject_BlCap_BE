using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.APIs.RequestModels.CaringTask;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Harvest;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/harvesting-tasks")]
    [ApiController]
    [Authorize]
    public class HarvestingTaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHarvestingTaskService _harvestingTaskService;
        public HarvestingTaskController(IMapper mapper, IHarvestingTaskService harvestingTaskService)
        {
            _mapper = mapper;
            _harvestingTaskService = harvestingTaskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHarvestingTasks([FromQuery] FilterRequestNotCaring model)
        {
            try
            {
                var result = await _harvestingTaskService.GetHarvestingTasks(model.plan_id, model.farmer_id, model.status, model.page_number, model.page_size, model.start_date, model.end_date);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
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

        [HttpPost("images/upload")]
        public async Task<IActionResult> UploadImage(List<IFormFile> image)
        {
            var rs = await _harvestingTaskService.UploadImage(image);
            return Ok(rs);
        }

        [HttpPut("{id}/task-report")]
        public async Task<IActionResult> UpdateHarvestingTask(int id, HarvestingTaskReport model)
        {
            var rs = await _harvestingTaskService.ReportHarvestingTask(id, model);
            return Ok(rs);
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetdashboardHarvestingTasks()
        {
            try
            {
                var result = await _harvestingTaskService.DashboardHarvest();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateHarvestingTask model)
        {
            try
            {
                var rs = await _harvestingTaskService.UpdateTask(id, model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateHarvestingPlan model)
        {
            try
            {
                var rs = await _harvestingTaskService.CreateHarvestingTask(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var rs = await _harvestingTaskService.DeleteTask(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("dashboard/plan/{id}")]
        public async Task<IActionResult> GetdashboardHarvestingTasksByPlanId([FromRoute]int id)
        {
            try
            {
                var result = await _harvestingTaskService.DashboardHarvestByPlanId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("completed/plan/{id}")]
        public async Task<IActionResult> GetHarvestdTasksDashboardByPlanId([FromRoute] int id)
        {
            try
            {
                var result = await _harvestingTaskService.GetHavestedTasksDashboardByPlanId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/assigned-farmers")]
        public async Task<IActionResult> GetHistoryFarmers(int id)
        {
            try
            {
                var res = await _harvestingTaskService.GetHistoryFarmers(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/farmers/{farmer_id}")]
        public async Task<IActionResult> ChangeFarmer(int id, int farmer_id, [FromBody] TaskReasonReplace? model)
        {
            try
            {
                var res = await _harvestingTaskService.ReplaceFarmer(id, farmer_id, model == null ? null : model.Reason);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
