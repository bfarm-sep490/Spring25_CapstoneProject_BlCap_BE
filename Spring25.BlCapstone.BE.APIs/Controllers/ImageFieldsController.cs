using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageFieldsController : ControllerBase
    {
        public IImageFieldService _imageFieldService;
        public ImageFieldsController(IImageFieldService imageFieldService)
        {
            _imageFieldService = imageFieldService;
        }

        [HttpGet]
        public async Task<ActionResult<IBusinessResult>> GetAll()
        {
            try
            {
                var rs = await _imageFieldService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IBusinessResult>> RemoveImage(int id)
        {
            try
            {
                var rs = await _imageFieldService.DeleteImage(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("status/{id}")]
        public async Task<ActionResult<IBusinessResult>> SwicthStatus(int id)
        {
            try
            {
                var rs = await _imageFieldService.SwitchStatus(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
