using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Item;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/items")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {
        public IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? status, string? type)
        {
            var rs = await _itemService.GetAll(status, type);
            return Ok(rs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rs = await _itemService.GetById(id);   
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatedItem item)
        {
            var rs = await _itemService.CreateItem(item);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreatedItem item)
        {
            var rs = await _itemService.UpdateItem(id, item);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var rs = await _itemService.RemoveItem(id);
            return Ok(rs);
        }

        [HttpPost("images/upload")]
        public async Task<IActionResult> UploadImage(List<IFormFile> image)
        {
            var rs = await _itemService.UploadImage(image);
            return Ok(rs);
        }

        [HttpPut("{id}/switch-active")]
        public async Task<IActionResult> ToggleActiveInactiveItem(int id)
        {
            try
            {
                var rs = await _itemService.ToggleActiveInactive(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
