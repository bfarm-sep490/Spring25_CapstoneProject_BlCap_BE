using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.Services;
using System.ComponentModel.DataAnnotations;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/experts")]
    [ApiController]
    public class ExpertController : ControllerBase
    {
        private IExpertService _expertService;
        public ExpertController(IExpertService expertService)
        {
            _expertService = expertService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _expertService.GetAll();
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
            var rs = await _expertService.GetById(id);
            return Ok(rs);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> SwitchStatus(int id)
        {
            var rs = await _expertService.SwitchStatus(id);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var rs = await _expertService.RemoveExpert(id);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFarmer model)
        {
            var rs = await _expertService.CreateExpert(model);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateFarmer model)
        {
            var rs = await _expertService.UpdateExpert(id, model);
            return Ok(rs);
        }

        [HttpPost("images/upload")]
        public async Task<IActionResult> UploadImage(List<IFormFile> image)
        {
            var rs = await _expertService.UploadImage(image);
            return Ok(rs);
        }
    }
}
