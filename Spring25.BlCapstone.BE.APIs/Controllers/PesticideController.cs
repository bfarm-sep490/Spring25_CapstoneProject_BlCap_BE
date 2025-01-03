using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api")]
    [ApiController]
    public class PesticideController : Controller
    {
        private IPesticideService _pesticideService;
        public PesticideController(IPesticideService pesticideService)
        {
            _pesticideService = pesticideService;
        }
        [HttpGet("pesticides")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _pesticideService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
