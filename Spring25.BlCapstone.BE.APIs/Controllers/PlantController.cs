﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.APIs.RequestModels.Plant;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plant;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/plants")]
    [ApiController]
    public class PlantController : Controller
    {
        private IMapper _mapper;
        private IPlantService _seedService;
        public PlantController(IMapper mapper, IPlantService seedService)
        {
            _mapper = mapper;
            _seedService = seedService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(string? status, string? name)
        {
            try
            {
                var result = await _seedService.GetAll(status, name);
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
        public async Task<IActionResult> Create([FromBody] CreatedPlant model)
        {
            try
            {
                var obj = _mapper.Map<PlantModel>(model);
                var result = await _seedService.Create(obj);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdatedPlant model, [FromRoute]int id)
        {
            try
            {
                var obj = _mapper.Map<PlantModel>(model);
                var result = await _seedService.Update(id,obj);
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
            var rs = await _seedService.UploadImage(image);
            return Ok(rs);
        }

        [HttpGet("{id}/suggest-yields")]
        public async Task<IActionResult> GetSuggestYieldsbyPlantId([FromRoute] int id)
        {
            try
            {
                var result = await _seedService.GetSuggestYieldsbyPlantId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("suggest-yields")]
        public async Task<IActionResult> DeleteSuggestYields([FromBody] PlantYieldModel model)
        {
            try
            {
                var result = await _seedService.DeleteSuggestYields(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
        [HttpPost("suggest-yields")]
        public async Task<IActionResult> CreateSuggestYields([FromBody]PlantYieldModel model)
        {
            try
            {
                var result = await _seedService.CreateSuggestYields(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
