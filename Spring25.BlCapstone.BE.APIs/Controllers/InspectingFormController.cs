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
        public async Task<IActionResult> GetAllInpectingForms(int? planId, int? inspectorId)
        {
            try
            {
                var result = await _inspectingFormService.GetAllInspectingForm(planId, inspectorId);
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

        [HttpPost("inspecting-forms/images/upload")]
        public async Task<IActionResult> UploadImage(List<IFormFile> image)
        {
            var rs = await _inspectingFormService.UploadImage(image);
            return Ok(rs);
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
    }
}
