using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/yields")]
    [ApiController]
    public class YieldController : Controller
    {
        private IMapper _mapper;
        private IYieldService _yieldService;
        public YieldController(IMapper mapper, IYieldService yieldService)
        {
            _mapper = mapper;
            _yieldService = yieldService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _yieldService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            try
            {
                var result = await _yieldService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveById([FromRoute] int id)
        {
            try
            {
                var result = await _yieldService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
