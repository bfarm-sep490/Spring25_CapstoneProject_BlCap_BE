using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Harvest;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Package;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/packaging-tasks")]
    [ApiController]
    public class PackagingTaskController : ControllerBase
    {
        private IPackagingTaskService _service;

        public PackagingTaskController(IPackagingTaskService packagingService)
        {
            _service = packagingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int? plan_id, int? farmer_id)
        {
            try
            {
                var rs = await _service.GetPackagingTasks(plan_id, farmer_id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var rs = await _service.GetPackagingTaskById(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePackagingPlan model)
        {
            try
            {
                var rs = await _service.CreatePackagingTask(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut("{id}/task-report")]
        public async Task<IActionResult> ReportTask(int id, PackagingReport model)
        {
            var rs = await _service.ReportPackagingTask(id, model);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdatePackaging model)
        {
            try
            {
                var rs = await _service.UpdatePackagingTask(id, model);
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
                var rs = await _service.DeleteTask(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("images/upload")]
        public async Task<IActionResult> UploadImage(List<IFormFile> image)
        {
            var rs = await _service.UploadImage(image);
            return Ok(rs);
        }

        [HttpGet("{id}/assigned-farmers")]
        public async Task<IActionResult> GetHistoryFarmers(int id)
        {
            try
            {
                var res = await _service.GetHistoryFarmers(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/farmers/{farmer_id}")]
        public async Task<IActionResult> ChangeFarmer(int id, int farmer_id, [FromBody] string? reason)
        {
            try
            {
                var res = await _service.ReplaceFarmer(id, farmer_id, reason);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
