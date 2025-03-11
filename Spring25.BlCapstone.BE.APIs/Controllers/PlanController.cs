using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plan;
using Spring25.BlCapstone.BE.Services.Services;
using System.ComponentModel.DataAnnotations;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/plans")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private IPlanService _planService;
        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _planService.GetAll();
                return Ok(res);
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
                var res = await _planService.GetById(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/general")]
        public async Task<IActionResult> GetGeneralPlan(int id)
        {
            try
            {
                var res = await _planService.GetGeneralPlan(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/problems")]
        public async Task<IActionResult> GetAllProbs(int id)
        {
            try
            {
                var res = await _planService.GetAllProblems(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/farmers")]
        public async Task<IActionResult> GetAllFarmers(int id)
        {
            try
            {
                var res = await _planService.GetAllFarmers(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetAllItems(int id)
        {
            try
            {
                var res = await _planService.GetAllItems(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/tasks-assign")]
        public async Task<IActionResult> AssignTask(int id, AssigningPlan model)
        {
            try
            {
                var res = await _planService.AssignTasks(id, model);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/plan-approval")]
        public async Task<IActionResult> ApprovePlan(int id)
        {
            try
            {
                var rs = await _planService.ApprovePlan(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePlan model)
        {
            try
            {
                var rs = await _planService.Create(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdatePlan(int id, [Required] string status)
        {
            try
            {
                var rs = await _planService.UpdateStatus(id, status);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdatePlan model)
        {
            try
            {
                var rs = await _planService.UpdatePlan(id, model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            try
            {
                var rs = await _planService.DeletePlan(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}/dashboard")]
        public async Task<IActionResult> GetStatusTasksDashboardByPlanId([FromRoute]int id)
        {
            try
            {
                var res = await _planService.GetStatusTasksDashboardByPlanId(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
