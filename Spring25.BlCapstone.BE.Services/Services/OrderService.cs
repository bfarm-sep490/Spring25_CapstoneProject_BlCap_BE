using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Helper;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Order;
using Spring25.BlCapstone.BE.Services.Untils;
using Spring25.BlCapstone.BE.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IOrderService
    {
        Task<IBusinessResult> CreateOrder(CreateOrderModel order);
        Task<IBusinessResult> GetAllOrders(string? status,int? retailer, int? planId);
        Task<IBusinessResult> GetOrderById(int id);
        Task<IBusinessResult> GetOrderWithNoPlan();
        Task<IBusinessResult> UpdateOrderStatus(int id, string status);
    }
    public class OrderService : IOrderService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IBusinessResult> CreateOrder(CreateOrderModel order)
        {
            try
            {
                var newOrder = _mapper.Map<Repositories.Models.Order>(order);
                newOrder.Status = "PendingConfirmation";
                newOrder.CreatedAt = DateTimeHelper.NowVietnamTime();

                var plant = await _unitOfWork.PlantRepository.GetByIdAsync(order.PlantId);
                var account = await _unitOfWork.AccountRepository.GetAccountByUserId(retailerId: order.RetailerId);

                var rs = await _unitOfWork.OrderRepository.CreateAsync(newOrder);
                var estimatedPrice = plant.BasePrice * order.PreOrderQuantity;

                var body = EmailHelper.GetEmailBody("ConfirmOrder.html", new Dictionary<string, string>
                {
                    { "{{orderDate}}", DateTimeHelper.NowVietnamTime().ToString("MMM dd, yy") },
                    { "{{orderConfirmDate}}", DateTimeHelper.NowVietnamTime().AddDays(2).ToString("MMM dd, yy") },
                    { "{{orderTrackingLink}}", "https://bfarmx.space" },
                    { "{{orderCode}}", $"{plant.PlantName}" },
                    { "{{imageOrder}}", $"{plant.ImageUrl}" },
                    { "{{productName}}", $"{plant.PlantName}" },
                    { "{{preOrderQuantity}}", $"{order.PreOrderQuantity.ToString("N0")}" },
                    { "{{pricePerOne}}", $"{plant.BasePrice.ToString("N0")}" },
                    { "{{subtotal}}", $"{estimatedPrice.ToString("N0")}" },
                    { "{{estimatedTotal}}", $"{estimatedPrice.ToString("N0")}" },
                    { "{{deposit}}", $"{order.DepositPrice.ToString("N0")}" }
                });

                await EmailHelper.SendMail(account.Email, "Cảm ơn bạn đã sử dụng dịch vụ của BFARMX - Blockchain FarmXperience!", account.Name, body);

                var retaileraChanel = $"retailer-{order.RetailerId}";
                var message = "Đơn hàng của bạn đã được đưa vào hàng đợi xử lý. Vui lòng kiểm tra email để cập nhật thông tin mới nhất!";
                var title = $"Đơn hàng đã vào hàng đợi - {plant.PlantName}";
                await AblyHelper.SendMessageWithChanel(title, message, retaileraChanel);
                await _unitOfWork.NotificationRetailerRepository.CreateAsync(new NotificationRetailer
                {
                    RetailerId = order.RetailerId,
                    Message = message,
                    Title = title,
                    IsRead = false,
                    CreatedDate = DateTimeHelper.NowVietnamTime(),
                });

                var response = new OrderModel();
                _mapper.Map(rs, response);
                return new BusinessResult(200, "Create order successfull", response);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAllOrders(string? status, int? retailer, int? planId)
        {
            var list = await _unitOfWork.OrderRepository.GetAllOrder(status, retailer, planId);
            var result = _mapper.Map<List<OrderModel>>(list);
            list.ForEach(r =>
            {
                var model = result.FirstOrDefault(m => m.Id == r.Id);
                if (model != null)
                {
                    model.OrderProducts = r.PackagingTasks
                        .SelectMany(task => task.PackagingProducts.Select(ptp => new ProOr
                        {
                            ProductId = ptp.Id,
                            QuantityOfPacks = ptp.PackQuantity,
                            Status = ptp.Status,
                            EvaluatedResult = task.Plan?.InspectingForms
                                .OrderByDescending(f => f.CompleteDate)
                                .FirstOrDefault()?.InspectingResult?.EvaluatedResult ?? ""
                        }))
                        .ToList();
                }
            });
            return new BusinessResult(200, "List Order", result);
        }

        public async Task<IBusinessResult> GetOrderById(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetOrder(id);
            if (order == null) return new BusinessResult(400, "Not found Order");
            var result = _mapper.Map<OrderModel>(order);
            return new BusinessResult(200, "Get Order by Id", result);
        }

        public async Task<IBusinessResult> GetOrderWithNoPlan()
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetOrderWithNoPlan();

                var rs = _mapper.Map<List<OrderModel>>(orders);
                return new BusinessResult(200, "List order with no plan: ", rs);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> UpdateOrderStatus(int id, string status)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetOrderByOrderId(id);
                if (order == null)
                {
                    return new BusinessResult(400, "Not found any orders !");
                }

                if (order.OrderPlans.Any())
                {
                    return new BusinessResult(400, "Can not change status order which already has a plan for it !");
                }

                order.Status = status;
                var retailer = await _unitOfWork.AccountRepository.GetAccountByUserId(retailerId: order.RetailerId);
                var plant = await _unitOfWork.PlantRepository.GetByIdAsync(order.PlantId);

                if (status.ToLower().Trim().Equals("pending"))
                {
                    DateTime date = DateTimeHelper.NowVietnamTime().AddDays(3);
                    int day = date.Day;
                    string suffix = GetDaySuffix(day);
                    string formatted = $"{day}{suffix} {date:MMMM yyyy, HH:mm}";

                    var body = EmailHelper.GetEmailBody("AcceptOrder.html", new Dictionary<string, string>
                    {
                        { "{{customerName}}", retailer.Name },
                        { "{{DeadlineDate}}", formatted },
                        { "{{planLink}}", $"https://bfarmx.space/orders/{id}" },
                        { "{{paynowLink}}", $"https://bfarmx.space/orders/{id}" }
                    });

                    await EmailHelper.SendMail(retailer.Email, "BFARMX - Blockchain FarmXperience xin trân trọng gửi tới bạn!", retailer.Name, body);
                    
                    var retaileraChanel = $"retailer-{order.RetailerId}";
                    var message = "Đơn hàng của bạn đã được duyệt. Vui lòng hoàn tất đặt cọc trong vòng 3 ngày để tiếp tục xử lý đơn hàng. Nếu quá thời hạn, đơn hàng sẽ bị hủy tự động.";
                    var title = $"Đơn hàng đã được cập nhật - {plant.PlantName}";
                    await AblyHelper.SendMessageWithChanel(title, message, retaileraChanel);
                    await _unitOfWork.NotificationRetailerRepository.CreateAsync(new NotificationRetailer
                    {
                        RetailerId = order.RetailerId,
                        Message = message,
                        Title = title,
                        IsRead = false,
                        CreatedDate = DateTimeHelper.NowVietnamTime(),
                    });
                }
                else if (status.ToLower().Trim().Equals("cancel"))
                {
                    var body = EmailHelper.GetEmailBody("CancelOrder.html", new Dictionary<string, string>
                    {
                        { "{{customerName}}", retailer.Name }
                    });

                    await EmailHelper.SendMail(retailer.Email, "BFARMX - Blockchain FarmXperience chân thành xin lỗi bạn!", retailer.Name, body);

                    var retaileraChanel = $"retailer-{order.RetailerId}";
                    var message = "Đơn hàng của bạn đã bị hủy do một số lý do ngoài ý muốn. Chúng tôi thành thật xin lỗi vì sự bất tiện này và rất mong tiếp tục nhận được sự ủng hộ của bạn ở những đơn hàng tiếp theo.";
                    var title = $"Đơn hàng đã bị từ chối - {plant.PlantName}";
                    await AblyHelper.SendMessageWithChanel(title, message, retaileraChanel);
                    await _unitOfWork.NotificationRetailerRepository.CreateAsync(new NotificationRetailer
                    {
                        RetailerId = order.RetailerId,
                        Message = message,
                        Title = title,
                        IsRead = false,
                        CreatedDate = DateTimeHelper.NowVietnamTime(),
                    });
                }

                await _unitOfWork.OrderRepository.UpdateAsync(order);
                var rs = _mapper.Map<OrderModel>(order);
                return new BusinessResult(200, "Change status order success !", rs);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        private string GetDaySuffix(int day)
        {
            if (day >= 11 && day <= 13) return "th"; 
            switch (day % 10)
            {
                case 1: return "st";
                case 2: return "nd";
                case 3: return "rd";
                default: return "th";
            }
        }
    }
}