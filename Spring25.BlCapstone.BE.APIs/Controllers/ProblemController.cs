using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Problem;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/problems")]
    [ApiController]
    [Authorize]
    public class ProblemController : ControllerBase
    {
        public IProblemService _problemService;
        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int? plan_id, int? farmer_id, string? name, string? status, int? page_number, int? page_size)
        {
            var rs = await _problemService.GetAll(plan_id, farmer_id, name, status, page_number, page_size);
            return Ok(rs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rs = await _problemService.GetById(id);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProblem model)
        {
            try
            {
                var rs = await _problemService.Create(model);
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
            var rs = await _problemService.UploadImage(image);
            return Ok(rs);
        }

        [HttpPut("{id}/problem-report")]
        public async Task<IActionResult> ReportProblem(int id, ReportProblem model)
        {
            try
            {
                var rs = await _problemService.ReportProblem(id, model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
