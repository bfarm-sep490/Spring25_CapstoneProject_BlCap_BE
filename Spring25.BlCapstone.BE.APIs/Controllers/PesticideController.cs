//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Spring25.BlCapstone.BE.APIs.RequestModels.Pesticide;
//using Spring25.BlCapstone.BE.Services.BusinessModels.Pesticide;
//using Spring25.BlCapstone.BE.Services.Services;

//namespace Spring25.BlCapstone.BE.APIs.Controllers
//{
//    [Route("api")]
//    [ApiController]
//    public class PesticideController : Controller
//    {
//        private IPesticideService _pesticideService;
//        private IMapper _mapper;
//        public PesticideController(IPesticideService pesticideService, IMapper mapper)
//        {
//            _pesticideService = pesticideService;
//            _mapper = mapper;
//        }
//        [HttpGet("pesticides")]
//        public async Task<IActionResult> GetAll()
//        {
//            try
//            {
//                var result = await _pesticideService.GetAll();
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//        [HttpPost("pesticides")]
//        public async Task<IActionResult> Create([FromBody] CreatedPesticide model)
//        {
//            try
//            {
//                var obj = _mapper.Map<PesticideModel>(model);
//                var result = await _pesticideService.Create(obj);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                {
//                    return BadRequest(ex.Message);
//                }
//            }
//        }
//        [HttpPut("pesticides/{id}")]
//        public async Task<IActionResult> Update([FromBody] UpdatedPesticide model, [FromRoute] int id)
//        {
//            try
//            {
//                var obj = _mapper.Map<PesticideModel>(model);
//                var result = await _pesticideService.Update(id, obj);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//        [HttpDelete("pesticides/{id}")]
//        public async Task<IActionResult> Delete([FromRoute] int id)
//        {
//            try
//            {
//                var result = await _pesticideService.Delete(id);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//        [HttpGet("farmowner/{id}/pesticides")]
        
//        public async Task<IActionResult> GetbyFarmOwnerId([FromRoute] int id)
//        {
//            try
//            {
//                var obj = await _pesticideService.GetByFarmOwnerId(id);
//                return Ok(obj);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//    }
//}
