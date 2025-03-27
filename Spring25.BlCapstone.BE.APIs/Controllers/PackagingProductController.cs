using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/packaging-products")]
    [ApiController]
    public class PackagingProductController : ControllerBase
    {
        private readonly IPackagingProductService _packagingProductService;
        public PackagingProductController(IPackagingProductService packagingProductService)
        {
            _packagingProductService = packagingProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _packagingProductService.GetAll();
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
                var result = await _packagingProductService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
