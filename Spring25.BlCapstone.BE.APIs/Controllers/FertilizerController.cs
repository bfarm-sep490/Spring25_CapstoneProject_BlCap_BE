﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.APIs.RequestModels.Fertilizer;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.BusinessModels.Fertilizer;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api")]
    [ApiController]
    public class FertilizerController : Controller
    {
        private IFertilizerService _fertilizerService;
        private IMapper _mapper;
        public FertilizerController(IFertilizerService fertilizerService, IMapper mapper)
        {
            _fertilizerService = fertilizerService;
            _mapper = mapper;
        }
        [HttpGet("fertilizers")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _fertilizerService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("fertilizers/{id}")]
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
        [HttpPost("fertilizers")]
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
        [HttpPut("fertilizers/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdatedFertilizer model, [FromRoute]int id)
        {
            try
            {
                var obj = _mapper.Map<FertilizerModel>(model);
                var result = await _fertilizerService.Update(id,obj);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("fertilizers/farmowner/{id}")]
        public async Task<IActionResult> GetbyFarmOwnerId([FromRoute] int id)
        {
            try
            {
                var result = await _fertilizerService.GetByFarmOwnerId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
