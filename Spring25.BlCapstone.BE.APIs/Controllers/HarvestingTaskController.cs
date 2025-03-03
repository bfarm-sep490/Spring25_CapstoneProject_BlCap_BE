﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Harvest;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api")]
    [ApiController]
    public class HarvestingTaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHarvestingTaskService _harvestingTaskService;
        public HarvestingTaskController(IMapper mapper, IHarvestingTaskService harvestingTaskService)
        {
            _mapper = mapper;
            _harvestingTaskService = harvestingTaskService;
        }

        [HttpGet("harvesting-tasks")]
        public async Task<IActionResult> GetAllHarvestingTasks(int? planId, int? farmerId)
        {
            try
            {
                var result = await _harvestingTaskService.GetHarvestingTasks(planId, farmerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("harvesting-tasks/{id}")]
        public async Task<IActionResult> GetHarvestingTaskById([FromRoute]int id)
        {
            try
            {
                var result = await _harvestingTaskService.GetHarvestingTaskById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("harvesting-tasks/{id}/detail")]
        public async Task<IActionResult> GetDetailHarvestingTaskById([FromRoute] int id)
        {
            try
            {
                var result = await _harvestingTaskService.GetHarvestingTaskDetailById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("harvesting-tasks/images/upload")]
        public async Task<IActionResult> UploadImage(List<IFormFile> image)
        {
            var rs = await _harvestingTaskService.UploadImage(image);
            return Ok(rs);
        }

        [HttpPut("harvesting-tasks/{id}/task-report")]
        public async Task<IActionResult> UpdateHarvestingTask(int id, HarvestingTaskReport model)
        {
            var rs = await _harvestingTaskService.ReportHarvestingTask(id, model);
            return Ok(rs);
        }

        [HttpPut("harvesting-tasks/{id}")]
        public async Task<IActionResult> Update(int id, UpdateHarvestingTask model)
        {
            try
            {
                var rs = await _harvestingTaskService.UpdateTask(id, model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("harvesting-tasks")]
        public async Task<IActionResult> Create(CreateHarvestingPlan model)
        {
            try
            {
                var rs = await _harvestingTaskService.CreateHarvestingTask(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
