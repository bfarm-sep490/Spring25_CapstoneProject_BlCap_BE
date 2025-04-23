using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface ITransactionService
    {
        Task<IBusinessResult> GetAll(int? orderId, string? status);
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> GetDashBoard(DateTime? start, DateTime? end);
    }

    public class TransactionService : ITransactionService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionService(IMapper mapper)
        {
            _unitOfWork = new UnitOfWork();
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAll(int? orderId, string? status)
        {
            try
            {
                var trans = await _unitOfWork.TransactionRepository.GetTransactions(orderId, status);
                var res = _mapper.Map<List<TransactionModel>>(trans);

                return new BusinessResult(200, "List transactions: ", res);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
        
        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var trans = await _unitOfWork.TransactionRepository.GetByIdAsync(id);
                if (trans == null)
                {
                    return new BusinessResult(400, "Not found any transactions");
                }
                var res = _mapper.Map<TransactionModel>(trans);

                return new BusinessResult(200, "Transaction ne: ", res);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetDashBoard(DateTime? start, DateTime? end)
        {
            try
            {
                var dash = await _unitOfWork.TransactionRepository.GetDashboardTransactionsAsync(start, end);

                return new BusinessResult(200, "Dashboard Transactions: ", dash);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
