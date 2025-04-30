using AutoMapper;
using MailKit.Net.Imap;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.APIs.RequestModels.CaringTask;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Care;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/caring-tasks")]
    [ApiController]
    [Authorize]
    public class CaringTaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICaringTaskService _caringTaskService;
        public CaringTaskController(IMapper mapper, ICaringTaskService caringTaskService)
        {
            _mapper = mapper;
            _caringTaskService = caringTaskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FilterTaskResponse request)
        {
            try
            {
                var result = await _caringTaskService.GetAllCaringTask(request.plan_id, request.farmer_id, request.problem_id, request.status, request.page_number, request.page_size, request.start_date, request.end_date);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var result = await _caringTaskService.GetCaringTaskById(id);
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
            var rs = await _caringTaskService.UploadImage(image);
            return Ok(rs);
        }

        [HttpPost]
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

        [HttpDelete("{id}")]
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

        [HttpPut("{id}")]
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

        [HttpPut("{id}/task-report")]
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

        [HttpGet("dashboard")]
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

        [HttpGet("dashboard/plan/{id}")]
        public async Task<IActionResult> GetDashboardByPlanId([FromRoute] int id)
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

        [HttpGet("{id}/assigned-farmers")]
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

        [HttpGet("count/plans/{id}")]
        public async Task<IActionResult> GetCountPlanTypeByPlanId([FromRoute] int id)
        {
            try
            {
                var result = await _caringTaskService.GetTypeCaringTasksStatusByPlanId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("{id}/farmers/{farmer_id}")]
        public async Task<IActionResult> ChangeFarmer(int id, int farmer_id, [FromBody] TaskReasonReplace? model)
        {
            try
            {
                var res = await _caringTaskService.ReplaceFarmer(id, farmer_id, model == null ? null : model.Reason);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
