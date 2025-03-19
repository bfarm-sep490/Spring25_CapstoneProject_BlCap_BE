using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.APIs.RequestModels.Yield;
using Spring25.BlCapstone.BE.Services.BusinessModels.Yield;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/yields")]
    [ApiController]
    public class YieldController : Controller
    {
        private IMapper _mapper;
        private IYieldService _yieldService;
        public YieldController(IMapper mapper, IYieldService yieldService)
        {
            _mapper = mapper;
            _yieldService = yieldService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? status)
        {
            try
            {
                var result = await _yieldService.GetAll(status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            try
            {
                var result = await _yieldService.GetById(id);
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
                var result = await _yieldService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatedYield model)
        {
            try
            {
                var obj = _mapper.Map<YieldModel>(model);
                var rs = await _yieldService.Create(obj);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Create(int id, UpdatedYield model)
        {
            try
            {
                var obj = _mapper.Map<YieldModel>(model);
                var rs = await _yieldService.Update(id, obj);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}/suggest-plants")]
        public async Task<IActionResult> GetSuggestPlantsbyYieldId([FromRoute] int id)
        {
            try
            {
                var result = await _yieldService.GetSuggestPlantsbyYieldId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
