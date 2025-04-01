using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Order;
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
        Task<IBusinessResult> CancelOrder(int orderId);
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
                var newOrder = _mapper.Map<Order>(order);
                newOrder.Status = "Pending";
                newOrder.CreatedAt = DateTime.Now;

                var rs = await _unitOfWork.OrderRepository.CreateAsync(newOrder);
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
                if (!orders.Any())
                {
                    return new BusinessResult(404, "Theren't any order with no plan");
                }

                var rs = _mapper.Map<List<OrderModel>>(orders);
                return new BusinessResult(200, "List order with no plan: ", rs);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> CancelOrder(int orderId)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
                if (order == null) 
                {
                    return new BusinessResult(404, "Not found any orders !");
                }

                if (order.PlanId.HasValue)
                {
                    return new BusinessResult(400, "Can not cancel order which already has a plan for it !");
                }

                order.Status = "Cancel";
                await _unitOfWork.OrderRepository.UpdateAsync(order);
                var rs = _mapper.Map<OrderModel>(order);
                return new BusinessResult(200, "Cancel order success !", rs);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
