using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Repositories.BlockChain;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/blockchains")]
    [ApiController]
    [Authorize]
    public class BlockChainController : Controller
    {
        private readonly IBlockChainService _blockchain;
        public BlockChainController(IBlockChainService blockchain)
        {
            _blockchain = blockchain;
        }

        [HttpGet("plan/{contractAddress}")]
        public async Task<IActionResult> GetPlanByContractAddress([FromRoute]string contractAddress)
        {
            try
            {
                var result = await _blockchain.GetVechainPlanResponseByContractAddress(contractAddress);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("plan")]
        public async Task<IActionResult> CreateVechainPlan([FromBody]CreatedVeChainPlan model)
        {
            try
            {
                var result = await _blockchain.CreateVechainPlan(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{addressContract}/inspect")]
        public async Task<IActionResult> CreateVechainInspect([FromRoute]string addressContract ,[FromBody] CreateVechainInspect model)
        {
            try
            {
                var result = await _blockchain.CreateVechainInspect(addressContract,model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{addressContract}/task")]
        public async Task<IActionResult> CreateVechainTask([FromRoute] string addressContract, [FromBody] CreateVechainTask model)
        {
            try
            {
                var result = await _blockchain.CreateVechainTask(addressContract, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
