using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.APIs.RequestModels.Seed;
using Spring25.BlCapstone.BE.Services.BusinessModels.Seed;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/seeds")]
    [ApiController]
    public class SeedController : Controller
    {
        private IMapper _mapper;
        private ISeedService _seedService;
        public SeedController(IMapper mapper, ISeedService seedService)
        {
            _mapper = mapper;
            _seedService = seedService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _seedService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId([FromRoute] int id)
        {
            try
            {
                var result = await _seedService.GetById(id);
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
                var result = await _seedService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatedSeed model)
        {
            try
            {
                var obj = _mapper.Map<SeedModel>(model);
                var result = await _seedService.Create(obj);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdatedSeed model, [FromRoute]int id)
        {
            try
            {
                var obj = _mapper.Map<SeedModel>(model);
                var result = await _seedService.Update(id,obj);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
