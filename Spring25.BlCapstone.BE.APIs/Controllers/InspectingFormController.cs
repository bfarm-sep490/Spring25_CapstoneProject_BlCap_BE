using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAllInpectingForms()
        {
            try
            {
                var result = await _inspectingFormService.GetAllInspectingForm();
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
        [HttpGet("inspecting-forms/{id}/detail")]
        public async Task<IActionResult> GetDetailInpectingFormById([FromRoute]int id)
        {
            try
            {
                var result = await _inspectingFormService.GetDetailInspectingFormById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
