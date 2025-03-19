using AutoMapper;
using MailKit.Net.Imap;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Care;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api")]
    [ApiController]
    public class CaringTaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICaringTaskService _caringTaskService;
        public CaringTaskController(IMapper mapper, ICaringTaskService caringTaskService)
        {
            _mapper = mapper;
            _caringTaskService = caringTaskService;
        }

        [HttpGet("caring-tasks")]
        public async Task<IActionResult> GetAll(int? plan_id, int? farmer_id)
        {
            try
            {
                var result = await _caringTaskService.GetAllCaringTask(plan_id, farmer_id);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("caring-tasks/{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            try
            {
                var result = await _caringTaskService.GetCaringTaskById(id);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("caring-tasks/images/upload")]
        public async Task<IActionResult> UploadImage(List<IFormFile> image)
        {
            var rs = await _caringTaskService.UploadImage(image);
            return Ok(rs);
        }

        [HttpPost("caring-tasks")]
        public async Task<IActionResult> Create(CreateCaringPlan model)
        {
            try
            {
                var rs = await _caringTaskService.CreateCaringTask(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("caring-tasks/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var rs = await _caringTaskService.DeleteCaringTask(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("caring-tasks/{id}")]
        public async Task<IActionResult> Update(int id, UpdateCaringTask model)
        {
            try
            {
                var rs = await _caringTaskService.UpdateDetailCaringTask(id, model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("caring-tasks/{id}/task-report")]
        public async Task<IActionResult> Report(int id, CaringTaskReport model)
        {
            try
            {
                var rs = await _caringTaskService.TaskReport(id, model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("caring-tasks/dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            try
            {
                var result = await _caringTaskService.DashboardCaringTasks();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("caring-tasks/dashboard/plan/{id}")]
        public async Task<IActionResult> GetDashboardByPlanId([FromRoute]int id)
        {
            try
            {
                var result = await _caringTaskService.DashboardCaringTasksByPlanId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("caring-tasks/{id}/assigned-farmers")]
        public async Task<IActionResult> GetHistoryFarmers(int id)
        {
            try
            {
                var res = await _caringTaskService.GetHistoryFarmers(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
