using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.APIs.RequestModels.Fertilizer;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.BusinessModels.Fertilizer;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/fertilizers")]
    [ApiController]
    [Authorize]
    public class FertilizerController : Controller
    {
        private IFertilizerService _fertilizerService;
        private IMapper _mapper;
        public FertilizerController(IFertilizerService fertilizerService, IMapper mapper)
        {
            _fertilizerService = fertilizerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? status)
        {
            try
            {
                var result = await _fertilizerService.GetAll(status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            try
            {
                var result = await _fertilizerService.DeleteById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatedFertilizer model)
        {
            try
            {
                var obj = _mapper.Map<FertilizerModel>(model);
                var result = await _fertilizerService.Create(obj);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdatedFertilizer model, [FromRoute] int id)
        {
            try
            {
                var obj = _mapper.Map<FertilizerModel>(model);
                var result = await _fertilizerService.Update(id, obj);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            { 
                var result = await _fertilizerService.GetById(id);
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
            var rs = await _fertilizerService.UploadImage(image);
            return Ok(rs);
        }
    }
}
