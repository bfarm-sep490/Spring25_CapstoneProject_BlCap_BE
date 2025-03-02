using AutoMapper;
using MailKit.Net.Imap;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api")]
    [ApiController]
    public class CaringTaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICaringTaskService _caringTaskService;
        public CaringTaskController(IMapper mapper, ICaringTaskService caringTaskService)
        {
            _mapper = mapper;
            _caringTaskService = caringTaskService;
        }
        [HttpGet("caring-tasks")]
        public async Task<IActionResult> GetAll(int? planId)
        {
            try
            {
                var result = await _caringTaskService.GetAllCaringTask(planId);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("caring-tasks/{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            try
            {
                var result = await _caringTaskService.GetCaringTaskById(id);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpGet("caring-tasks/{id}/detail")]
        public async Task<IActionResult> GetDetailbyId([FromRoute] int id)
        {
            try
            {
                var result = await _caringTaskService.GetDetailCaringTaskById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
