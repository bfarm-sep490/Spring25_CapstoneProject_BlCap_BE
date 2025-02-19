using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Inspector;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/inspectors")]
    [ApiController]
    public class InspectorController : ControllerBase
    {
        private IInspectorService _inspectorService;
        public InspectorController(IInspectorService inspectorService)
        {
            _inspectorService = inspectorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _inspectorService.GetAll();
                return Ok(result);
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
                var result = await _inspectorService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("status/{id}")]
        public async Task<IActionResult> SwitchStatus(int id)
        {
            var rs = await _inspectorService.SwitchStatus(id);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var rs = await _inspectorService.RemoveInspector(id);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateInspector model)
        {
            var rs = await _inspectorService.CreateInspector(model);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateInspector model)
        {
            var rs = await _inspectorService.UpdateInspector(id, model);
            return Ok(rs);
        }
    }
}
