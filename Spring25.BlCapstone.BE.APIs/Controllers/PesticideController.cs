using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.APIs.RequestModels.Pesticide;
using Spring25.BlCapstone.BE.Services.BusinessModels.Pesticide;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/pesticides")]
    [ApiController]
    [Authorize]
    public class PesticideController : Controller
    {
        private IPesticideService _pesticideService;
        private IMapper _mapper;
        public PesticideController(IPesticideService pesticideService, IMapper mapper)
        {
            _pesticideService = pesticideService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(string? status)
        {
            try
            {
                var result = await _pesticideService.GetAll(status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatedPesticide model)
        {
            try
            {
                var obj = _mapper.Map<PesticideModel>(model);
                var result = await _pesticideService.Create(obj);
                return Ok(result);
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdatedPesticide model, [FromRoute] int id)
        {
            try
            {
                var obj = _mapper.Map<PesticideModel>(model);
                var result = await _pesticideService.Update(id, obj);
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
              
                var result = await _pesticideService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _pesticideService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("images/upload")]
        public async Task<IActionResult> UploadImage(List<IFormFile> image)
        {
            var rs = await _pesticideService.UploadImage(image);
            return Ok(rs);
        }
    }
}
