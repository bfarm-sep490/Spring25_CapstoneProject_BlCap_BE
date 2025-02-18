using Net.payOS;
using Net.payOS.Types;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IPaymentService
    {
        Task<IBusinessResult> CreatePayment();
        Task<IBusinessResult> GetPaymentDetails(long paymentCode);
        Task<IBusinessResult> CancelPayment(long paymentCode, string? cancelReason);
        Task ProcessWebhook(WebhookType webhookType);
    }

    public class PaymentService : IPaymentService
    {
        private readonly PayOS _payOS;
        private readonly UnitOfWork _unitOfWork;

        public PaymentService()
        {
            var clientId = Environment.GetEnvironmentVariable("CLIENTID");
            var apiKey = Environment.GetEnvironmentVariable("APIKEY");
            var checkSum = Environment.GetEnvironmentVariable("CHECKSUM");

            if (string.IsNullOrEmpty(clientId))
            {
                throw new Exception("ClientID PayOS configuration is missing...");
            } else if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("ApiKey PayOS configuration is missing...");
            } else if (string.IsNullOrEmpty(checkSum))
            {
                throw new Exception("Checksum PayOS configuration is missing...");
            }

            _payOS = new PayOS(clientId, apiKey, checkSum);
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> CreatePayment()
        {
            try
            {
                //hehe
                return null;
            }
            catch (Exception ex)
            {
                return new BusinessResult
                {
                    Status = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<IBusinessResult> GetPaymentDetails(long paymentCode)
        {
            try
            {
                PaymentLinkInformation paymentLinkInformation = await _payOS.getPaymentLinkInformation(paymentCode);

                if (paymentLinkInformation == null)
                {
                    return new BusinessResult
                    {
                        Status = 500,
                        Message = "Not found link information or haven't created payOS link yet !",
                        Data = null
                    };
                }
                else
                {
                    return new BusinessResult
                    {
                        Status = 200,
                        Message = "Payment Information",
                        Data = paymentLinkInformation
                    };
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult
                {
                    Status = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<IBusinessResult> CancelPayment(long paymentCode, string? cancelReason)
        {
            try
            {
                PaymentLinkInformation cancelPaymentLinkInfor = await _payOS.getPaymentLinkInformation(paymentCode);

                if (string.IsNullOrEmpty(cancelReason))
                {
                    await _payOS.cancelPaymentLink(paymentCode);
                }
                else
                {
                    await _payOS.cancelPaymentLink(paymentCode, cancelReason);
                }

                //hihi
                return new BusinessResult
                {
                    Status = 200,
                    Message = "Cancel Payment Success !",
                    Data = cancelPaymentLinkInfor
                };
            }
            catch (Exception ex)
            {
                return new BusinessResult
                {
                    Status = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task ProcessWebhook(WebhookType webhookType)
        {
            try
            {
                _payOS.verifyPaymentWebhookData(webhookType);
                
                if(webhookType.code == "00")
                {
                    //hoho
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
