﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.APIs.RequestModels.CaringTask;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Harvest;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Package;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/packaging-tasks")]
    [ApiController]
    [Authorize]
    public class PackagingTaskController : ControllerBase
    {
        private IPackagingTaskService _service;

        public PackagingTaskController(IPackagingTaskService packagingService)
        {
            _service = packagingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FilterRequestNotCaring model)
        {
            try
            {
                var rs = await _service.GetPackagingTasks(model.plan_id, model.farmer_id, model.status, model.page_number, model.page_size, model.start_date, model.end_date);
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
                var rs = await _service.GetPackagingTaskById(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePackagingPlan model)
        {
            try
            {
                var rs = await _service.CreatePackagingTask(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/task-report")]
        public async Task<IActionResult> ReportTask(int id, PackagingReport model)
        {
            var rs = await _service.ReportPackagingTask(id, model);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdatePackaging model)
        {
            try
            {
                var rs = await _service.UpdatePackagingTask(id, model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var rs = await _service.DeleteTask(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("images/upload")]
        public async Task<IActionResult> UploadImage(List<IFormFile> image)
        {
            var rs = await _service.UploadImage(image);
            return Ok(rs);
        }

        [HttpGet("{id}/assigned-farmers")]
        public async Task<IActionResult> GetHistoryFarmers(int id)
        {
            try
            {
                var res = await _service.GetHistoryFarmers(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/farmers/{farmer_id}")]
        public async Task<IActionResult> ChangeFarmer(int id, int farmer_id, [FromBody] TaskReasonReplace? model)
        {
            try
            {
                var res = await _service.ReplaceFarmer(id, farmer_id, model == null ? null : model.Reason);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
