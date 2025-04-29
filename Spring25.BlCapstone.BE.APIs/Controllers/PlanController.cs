using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.APIs.RequestModels.Plan;
using Spring25.BlCapstone.BE.Services.BusinessModels.Notification;
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
        public async Task<IActionResult> GetAll(int? expert_id, string? status, int? order_id)
        {
            try
            {
                var res = await _planService.GetAll(expert_id, status, order_id);
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
        
        [HttpPut("{id}/plan-rejection")]
        public async Task<IActionResult> RejectPlan(int id, string? reason)
        {
            try
            {
                var rs = await _planService.RejectPlan(id, reason);
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
        public async Task<IActionResult> UpdatePlan(int id, [Required] string status, string report_by)
        {
            try
            {
                var rs = await _planService.UpdateStatus(id, status, report_by);
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

        [HttpGet("farmer/{farmer_id}")]
        public async Task<IActionResult> GetPlansFarmerAssignedTo(int farmer_id, bool? is_active_in_plan,[FromQuery] List<string>? status)
        {
            try
            {
                var res = await _planService.GetAllPlanFarmerAssigned(farmer_id, is_active_in_plan, status);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/fertilizers")]
        public async Task<IActionResult> GetFerByPlanId(int id)
        {
            try
            {
                var res = await _planService.GetInfomationOfFertilizerTasksByPlanId(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/pesticides")]
        public async Task<IActionResult> GetPesByPlanId(int id)
        {
            try
            {
                var res = await _planService.GetInfomationOfPesticideTasksByPlanId(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/farmers/{farmer_id}")]
        public async Task<IActionResult> DeleteFarmerFromPlan(int id, int farmer_id)
        {
            try
            {
                var rs = await _planService.RemoveFarmerFromPlan(id, farmer_id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("{id}/farmers")]
        public async Task<IActionResult> AddFarmerToPlan(int id, AddFarmerToPlan model)
        {
            try
            {
                var rs = await _planService.AddFarmerToPlan(id, model.FarmerId);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/tasks/count")]
        public async Task<IActionResult> GetCountTasks([FromRoute] int id)
        {
            try
            {
                var result = await _planService.GetCountTasksByPlanId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/orders/{order_id}")]
        public async Task<IActionResult> DeleteOrderFromPlan(int id, int order_id)
        {
            try
            {
                var rs = await _planService.RemoveOrderFromPlan(id, order_id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/orders/{order_id}")]
        public async Task<IActionResult> AddOrderToPlan(int id, int order_id)
        {
            try
            {
                var rs = await _planService.AddOrderToPlan(id, order_id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/busy-farmers")]
        public async Task<IActionResult> GetAllFarmers(int id, DateTime? start, DateTime? end)
        {
            try
            {
                var res = await _planService.GetBusyFarmerInPlanAssigned(id, start, end);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/genarated-tasks")]
        public async Task<IActionResult> GenarateTasks([FromRoute]int id, [FromQuery] List<int> farmer_ids)
        {
            try
            {
                var res = await _planService.GenarateTasksForFarmer(id, farmer_ids);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/status/complete")]
        public async Task<IActionResult> ChangeCompleteStatus(int id)
        {
            try
            {
                var rs = await _planService.ChangeCompleteStatus(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{id}/status/cancel")]
        public async Task<IActionResult> ChangeCancelStatus(int id)
        {
            try
            {
                var rs = await _planService.ChangeCancelStatus(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{id}/plan-public")]
        public async Task<IActionResult> PublicPlan(int id)
        {
            try
            {
                var rs = await _planService.PublicPlan(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("with-details")]
        public async Task<IActionResult> CreateTemplatePlan(CreatePlanTemplate model)
        {
            try
            {
                var res = await _planService.CreateBigPlan(model);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetOrdersPlan(int id)
        {
            try
            {
                var res = await _planService.GetPlanOrderById(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("template-plan")]
        public async Task<IActionResult> GetTemplatesPlan([FromBody]RequestTemplatePlan model)
        {
            try
            {
                var res = await _planService.GetTemplatePlan(model);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("qrcode-to-retailer")]
        public async Task<IActionResult>NotificationforExperts([FromBody] NotificationExpertsRequest model)
        {
            try
            {
                var res = await _planService.NotificationforExperts(model);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
