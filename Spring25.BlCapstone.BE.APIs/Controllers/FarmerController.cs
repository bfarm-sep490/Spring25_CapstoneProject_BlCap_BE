using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api")]
    [ApiController]
    public class FarmerController : Controller
    {
        private IFarmerService _service;
        private IMapper _mapper;
        public FarmerController(IFarmerService farmerService, IMapper mapper)
        {
            _service = farmerService;
            _mapper = mapper;
        }
        [HttpGet("farmers")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
