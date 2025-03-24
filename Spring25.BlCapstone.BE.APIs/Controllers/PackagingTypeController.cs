using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/packaging-types")]
    [ApiController]
    public class PackagingTypeController : ControllerBase
    {
        private readonly IPackagingTypeService _packagingTypeService;
        public PackagingTypeController(IPackagingTypeService packagingTypeService)
        {
            _packagingTypeService = packagingTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var rs = await _packagingTypeService.GetAllTypes();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
