﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Inspector;
using Spring25.BlCapstone.BE.Services.Services;
using System.ComponentModel.DataAnnotations;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/inspectors")]
    [ApiController]
    [Authorize]
    public class InspectorController : ControllerBase
    {
        private IInspectorService _inspectorService;
        public InspectorController(IInspectorService inspectorService)
        {
            _inspectorService = inspectorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _inspectorService.GetAll();
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
            try
            {
                var result = await _inspectorService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> SwitchStatus(int id)
        {
            var rs = await _inspectorService.SwitchStatus(id);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var rs = await _inspectorService.RemoveInspector(id);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInspector model)
        {
            var rs = await _inspectorService.CreateInspector(model);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateInspector model)
        {
            var rs = await _inspectorService.UpdateInspector(id, model);
            return Ok(rs);
        }

        [HttpPost("images/upload")]
        public async Task<IActionResult> UploadImage(List<IFormFile> image)
        {
            var rs = await _inspectorService.UploadImage(image);
            return Ok(rs);
        }

        [HttpGet("{id}/notifications")]
        public async Task<IActionResult> GetListNotifications(int id)
        {
            try
            {
                var rs = await _inspectorService.GetListNotifications(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("notification-read/{notification_id}")]
        public async Task<IActionResult> MarkAsRead(int notification_id)
        {
            try
            {
                var rs = await _inspectorService.MarkAsRead(notification_id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/notifications-read")]
        public async Task<IActionResult> MarkAllAsRead(int id)
        {
            try
            {
                var rs = await _inspectorService.MarkAllAsRead(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
