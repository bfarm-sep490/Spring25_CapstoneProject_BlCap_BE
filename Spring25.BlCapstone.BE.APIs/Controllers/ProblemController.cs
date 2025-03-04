using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Problem;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/problems")]
    [ApiController]
    public class ProblemController : ControllerBase
    {
        public IProblemService _problemService;
        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int? planId)
        {
            var rs = await _problemService.GetAll(planId);
            return Ok(rs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rs = await _problemService.GetById(id);
            return Ok(rs);
        }

        [HttpPut("{id}/result-content")]
        public async Task<IActionResult> UpdateRC(int id, UpdateResult model)
        {
            try
            {
                var rs = await _problemService.UpdateResult(id, model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
