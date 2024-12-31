using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api")]
    [ApiController]
    public class FertilizerController : Controller
    {
        private IFertilizerService _fertilizerService;
        public FertilizerController(IFertilizerService fertilizerService)
        {
            _fertilizerService = fertilizerService;
        }
        [HttpGet("fertilizers")]
        public async Task<IActionResult> GetAll()
        {
            try
            {   
                var result = await _fertilizerService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
