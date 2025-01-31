﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/farmers")]
    [ApiController]
    public class FarmerController : ControllerBase
    {
        private IFarmerService _service;

        public FarmerController(IFarmerService farmerService)
        {
            _service = farmerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAll();
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
            var rs = await _service.GetById(id);
            return Ok(rs);
        }

        [HttpPut("status/{id}")]
        public async Task<IActionResult> SwitchStatus(int id)
        {
            var rs = await _service.SwitchStatus(id);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var rs = await _service.RemoveFarmer(id);
            return Ok(rs);
        }
    }
}
