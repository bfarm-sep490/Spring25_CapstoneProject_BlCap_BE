using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Spring25.BlCapstone.BE.Services.BusinessModels.Payment;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("deposit-payment/payos")]
        [Authorize]
        public async Task<IActionResult> CreatePaymentDepositLink(CreatePaymentDepositRequest model)
        {
            try
            {
                var rs = await _paymentService.CreatePaymentDeposit(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("remaining-payment/payos")]
        [Authorize]
        public async Task<IActionResult> CreatePaymentRemainingLinkPayOS(CreatePaymentRemainingRequest model)
        {
            try
            {
                var rs = await _paymentService.CreatePaymentRemaining(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("remaining-payment/cash")]
        [Authorize]
        public async Task<IActionResult> CreatePaymentRemainingLinkCash(CreatePaymentRemainingRequest model)
        {
            try
            {
                var rs = await _paymentService.CashPayment(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{transaction_id}")]
        [Authorize]
        public async Task<IActionResult> GetPaymentLinkInfo(int transaction_id)
        {
            try
            {
                var rs = await _paymentService.GetPaymentDetailsPayOS(transaction_id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{transaction_id}/cancel")]
        [Authorize]
        public async Task<IActionResult> CancelPayment(int transaction_id, string? reason)
        {
            try
            {
                var rs = await _paymentService.CancelPayment(transaction_id, reason);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("webhook")]
        public async Task PayOSWebhook([FromBody] WebhookType webhookData)
        {
            await _paymentService.ProcessWebhook(webhookData);
        }
    }
}
