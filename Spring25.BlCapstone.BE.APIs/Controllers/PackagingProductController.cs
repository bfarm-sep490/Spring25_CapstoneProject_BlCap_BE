using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.Services;
using System.Text.Json.Serialization;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/packaging-products")]
    [ApiController]
    [Authorize]
    public class PackagingProductController : ControllerBase
    {
        private readonly IPackagingProductService _packagingProductService;
        public PackagingProductController(IPackagingProductService packagingProductService)
        {
            _packagingProductService = packagingProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int? plan_id, string? status, int? harvesting_task_id, int? order_id)
        {
            try
            {
                var result = await _packagingProductService.GetAll(plan_id, status, harvesting_task_id, order_id);
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
