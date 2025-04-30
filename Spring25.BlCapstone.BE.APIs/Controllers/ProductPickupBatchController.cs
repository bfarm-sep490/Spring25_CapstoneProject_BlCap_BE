using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Order;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/product-pickup-batches")]
    [ApiController]
    [Authorize]
    public class ProductPickupBatchController : ControllerBase
    {
        private readonly IProductPickupBatchService _batchesService;
        public ProductPickupBatchController(IProductPickupBatchService batchesService)
        {
            _batchesService = batchesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int? order_id)
        {
            try
            {
                var rs = await _batchesService.GetAll(order_id);
                return Ok(rs);
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
                var rs = await _batchesService.GetById(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNew(BatchCreateModel model)
        {
            try
            {
                var rs = await _batchesService.CreateNewBatch(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
