using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Fields;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        public IFieldService _fieldService;
        public FieldsController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpGet]
        public async Task<ActionResult<IBusinessResult>> GetAllFields()
        {
            try
            {
                var rs = await _fieldService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IBusinessResult>> GetById(int id)
        {
            try
            {
                var rs = await _fieldService.GetById(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<IBusinessResult>> Create([FromForm] FieldModels model)
        {
            try
            {
                var rs = await _fieldService.CreateField(model);
                return Ok(rs);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IBusinessResult>> Update(int id, [FromForm] UpdateFieldModels model)
        {
            try
            {
                var rs = await _fieldService.UpdateField(id, model);
                return Ok(rs);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("status/{id}")]
        public async Task<ActionResult<IBusinessResult>> SwitchStatus(int id)
        {
            try
            {
                var rs = await _fieldService.SwitchStatus(id);
                return Ok(rs);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IBusinessResult>> Remove(int id)
        {
            try
            {
                var rs = await _fieldService.RemoveField(id);
                return Ok(rs);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
