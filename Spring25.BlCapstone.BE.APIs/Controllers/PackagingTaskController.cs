using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAll(int? planId)
        {
            try
            {
                var rs = await _service.GetPackagingTasks(planId);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
