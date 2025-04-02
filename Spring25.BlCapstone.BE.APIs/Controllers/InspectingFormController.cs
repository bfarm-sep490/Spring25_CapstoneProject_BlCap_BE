using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api")]
    [ApiController]
    public class InspectingFormController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IInspectingFormService _inspectingFormService;
        public InspectingFormController(IMapper mapper, IInspectingFormService inspectingFormService)
        {
            _mapper = mapper;
            _inspectingFormService = inspectingFormService;
        }

        [HttpGet("inspecting-forms")]
        public async Task<IActionResult> GetAllInpectingForms(int? plan_id, int? inspector_id)
        {
            try
            {
                var result = await _inspectingFormService.GetAllInspectingForm(plan_id, inspector_id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("inspecting-forms/{id}")]
        public async Task<IActionResult> GetInpectingFormById([FromRoute]int id)
        {
            try
            {
                var result = await _inspectingFormService.GetInspectingFormById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("inspecting-forms")]
        public async Task<IActionResult> Create(CreateInspectingPlan model)
        {
            try
            {
                var rs = await _inspectingFormService.CreateInspectingForm(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("inspecting-forms/{id}")]
        public async Task<IActionResult> Update(int id, UpdateInspectingForm model)
        {
            try
            {
                var rs = await _inspectingFormService.UpdateInspectingForm(id, model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("inspecting-forms/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var rs = await _inspectingFormService.DeleteForm(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
