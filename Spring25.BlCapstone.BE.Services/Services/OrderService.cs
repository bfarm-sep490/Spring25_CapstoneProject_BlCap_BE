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
        Task<IBusinessResult> CreateOrder(OrderModel order);
        Task<IBusinessResult> GetAllOrders(string? status,int? retailer);
        Task<IBusinessResult> GetOrderById(int id);
        
    }
    public class OrderService:IOrderService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IBusinessResult> CreateOrder(OrderModel order)
        {
            throw new NotImplementedException();
        }

        public async Task<IBusinessResult> GetAllOrders(string? status, int? retailer)
        {
            var list = await _unitOfWork.OrderRepository.GetAllOrder(status, retailer);
            var result = _mapper.Map<List<OrderModel>>(list);
            return new BusinessResult(200,"List Order",result);
        }

        public async Task<IBusinessResult> GetOrderById(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null) return new BusinessResult(400, "Not found Order");
            var result = _mapper.Map<OrderModel>(order);
            return new BusinessResult(200, "Get Order by Id", result);
        }
    }
}
