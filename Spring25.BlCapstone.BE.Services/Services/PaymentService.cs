using Net.payOS;
using Net.payOS.Types;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Payment;
using Spring25.BlCapstone.BE.Services.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IPaymentService
    {
        Task<IBusinessResult> CreatePayment(CreatePaymentRequest model);
        Task<IBusinessResult> GetPaymentDetailsPayOS(int transactionID);
        Task<IBusinessResult> CancelPayment(int transactionID, string? cancelReason);
        Task ProcessWebhook(WebhookType webhookData);
    }

    public class PaymentService : IPaymentService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly PayOS _payOS;

        private readonly string _cancelURL;
        private readonly string _returnURL;

        public PaymentService()
        {
            var clientId = Environment.GetEnvironmentVariable("CLIENTID");
            var apiKey = Environment.GetEnvironmentVariable("APIKEY");
            var checkSum = Environment.GetEnvironmentVariable("CHECKSUM");

            if (string.IsNullOrEmpty(clientId))
            {
                throw new Exception("ClientID PayOS configuration is missing...");
            }
            else if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("ApiKey PayOS configuration is missing...");
            }
            else if (string.IsNullOrEmpty(checkSum))
            {
                throw new Exception("Checksum PayOS configuration is missing...");
            }

            _unitOfWork = new UnitOfWork();
            _payOS = new PayOS(clientId, apiKey, checkSum);
            _cancelURL = "https://www.google.com/";
            _returnURL = "https://www.google.com/";
        }

        public async Task<IBusinessResult> CreatePayment(CreatePaymentRequest model)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetOrderById(model.OrderId);
                if (order == null)
                {
                    return new BusinessResult(404, "Not found any order !");
                }

                if (model.Type.ToLower().Trim().Equals("deposit"))
                {
                    if (!order.Status.ToLower().Trim().Equals("pending"))
                    {
                        return new BusinessResult(400, $"Can not deposit order with {order.Status} status");
                    }

                    order.Status = "WaitingForPayment";
                    _unitOfWork.OrderRepository.PrepareUpdate(order);

                    var orderCode = OrderCodeHelper.GenerateOrderCodeHash(order.Id, order.PlantId);
                    List<ItemData> items = new List<ItemData>
                    {
                        new ItemData(order.Plant.PlantName + $" {DateTime.Now}", (int)order.EstimatedQuantity, model.Amount)
                    };

                    DateTime expirationDate = DateTime.UtcNow.AddHours(1);
                    int expiredAt = (int)((DateTimeOffset)expirationDate).ToUnixTimeSeconds();

                    var description = $"#{orderCode} D@{DateTime.Now:yyMMdd}";
                    PaymentData paymentData = new PaymentData(orderCode, model.Amount, description, items, _cancelURL, _returnURL, expiredAt: expiredAt, buyerPhone: order.Phone, buyerAddress: order.Address, buyerName: order.Retailer.Account.Name, buyerEmail: order.Retailer.Account.Email);
                    CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

                    if (createPayment == null)
                    {
                        return new BusinessResult(500, "Create Payment link fail !");
                    }
                    await _unitOfWork.OrderRepository.SaveAsync();

                    var trans = new Repositories.Models.Transaction
                    {
                        OrderId = order.Id,
                        Content = description + $" {model.Description}",
                        Price = model.Amount,
                        Type = model.Type,
                        Status = "Pending",
                        PaymentDate = DateTime.Now,
                        OrderCode = orderCode
                    };
                    
                    await _unitOfWork.TransactionRepository.CreateAsync(trans);
                    return new BusinessResult(200, "Create Deposit Payment Link successfully: ", createPayment);
                }
                else if (model.Type.ToLower().Trim().Equals("pay"))
                {
                    if (!order.Status.ToLower().Trim().Equals("pending") && !order.Status.ToLower().Trim().Equals("deposit"))
                    {
                        return new BusinessResult(400, $"Can not pay order with {order.Status} status");
                    }

                    order.Status = "WaitingForPayment";
                    _unitOfWork.OrderRepository.PrepareUpdate(order);

                    var orderCode = OrderCodeHelper.GenerateOrderCodeHash(order.Id, order.PlantId);
                    List<ItemData> items = new List<ItemData>
                    {
                        new ItemData(order.Plant.PlantName + $" {DateTime.Now}", (int)order.EstimatedQuantity, model.Amount)
                    };

                    DateTime expirationDate = DateTime.UtcNow.AddHours(1);
                    int expiredAt = (int)((DateTimeOffset)expirationDate).ToUnixTimeSeconds();

                    var description = $"#{orderCode} P@{DateTime.Now:yyMMdd}";
                    PaymentData paymentData = new PaymentData(orderCode, model.Amount, description, items, _cancelURL, _returnURL, expiredAt: expiredAt, buyerPhone: order.Phone, buyerAddress: order.Address, buyerName: order.Retailer.Account.Name, buyerEmail: order.Retailer.Account.Email);
                    CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);
                    if (createPayment == null)
                    {
                        return new BusinessResult(500, "Create Payment link fail !");
                    }
                    await _unitOfWork.OrderRepository.SaveAsync();

                    var trans = new Repositories.Models.Transaction
                    {
                        OrderId = order.Id,
                        Content = description + $" {model.Description}",
                        Price = model.Amount,
                        Type = model.Type,
                        Status = "Pending",
                        PaymentDate = DateTime.Now,
                        OrderCode = orderCode
                    };

                    await _unitOfWork.TransactionRepository.CreateAsync(trans);
                    return new BusinessResult(200, "Create Pay Payment Link successfully: ", createPayment);
                }
                else
                {
                    return new BusinessResult(400, $"Invalid type of payment {model.Type}");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetPaymentDetailsPayOS(int transactionID)
        {
            try
            {
                var trans = await _unitOfWork.TransactionRepository.GetByIdAsync(transactionID);
                if (trans == null)
                {
                    return new BusinessResult(404, "Not found any transactions !");
                }

                PaymentLinkInformation paymentLinkInformation = await _payOS.getPaymentLinkInformation(trans.OrderCode);
                if (paymentLinkInformation == null)
                {
                    return new BusinessResult(500, "Not found link information or have not created payOS link yet !");
                }
                else
                {
                    return new BusinessResult(200, "Payment Link Information: ", paymentLinkInformation);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> CancelPayment(int transactionID, string? cancelReason)
        {
            try
            {
                var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(transactionID);
                if (transaction == null)
                {
                    return new BusinessResult(404, "Transaction does not existed !");
                }

                PaymentLinkInformation cancelledPaymentLinkInfo = await _payOS.getPaymentLinkInformation(transaction.OrderCode);

                if (string.IsNullOrEmpty(cancelReason))
                {
                    await _payOS.cancelPaymentLink(transaction.OrderCode);
                }
                else
                {
                    await _payOS.cancelPaymentLink(transaction.OrderCode, cancelReason);
                }

                transaction.Status = "Cancel";
                var order = await _unitOfWork.OrderRepository.GetOrderByTransactionId(transactionID);
                order.Status = "Pending";
                await _unitOfWork.OrderRepository.UpdateAsync(order);

                var result = await _unitOfWork.TransactionRepository.UpdateAsync(transaction);
                if (result <= 0)
                {
                    return new BusinessResult(500, "Fail at update transactions");
                }

                return new BusinessResult(200, "Cancel Payment Success !");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task ProcessWebhook(WebhookType webhookData)
        {
            try
            {
                _payOS.verifyPaymentWebhookData(webhookData);
                var orderCode = webhookData.data.orderCode;

                if (webhookData.code == "00")
                {
                    var trans = await _unitOfWork.TransactionRepository.GetTransactionByOrderCode(orderCode);
                    trans.Status = "Paid";
                    trans.PaymentDate = DateTime.Now;

                    var order = await _unitOfWork.OrderRepository.GetByIdAsync(trans.OrderId);
                    order.Status = trans.Type.ToLower().Trim().Equals("deposit") ? "Deposit" : "Paid";

                    await _unitOfWork.TransactionRepository.UpdateAsync(trans);
                    await _unitOfWork.OrderRepository.UpdateAsync(order);
                }
                else
                {
                    throw new Exception(webhookData.desc);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
