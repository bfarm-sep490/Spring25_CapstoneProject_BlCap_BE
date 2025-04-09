using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.BlockChain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IBlockChainService
    {
        Task<VechainResponse> GetVechainPlanResponseByContractAddress(string contractAddress);
        Task<string> CreateVechainPlan(CreatedVeChainPlan model);
        Task<VechainTxResponse> CreateVechainTask(string  contractAddress,CreateVechainTask model);
        Task<VechainTxResponse> CreateVechainInspect(string contractAddress, CreateVechainInspect model);
    }
    public class BlockChainService : IBlockChainService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IVechainInteraction _vechainInteraction;
        public BlockChainService(IVechainInteraction vechainInteraction)
        {
            _unitOfWork ??= new UnitOfWork();
            _vechainInteraction = vechainInteraction;
        }

        public async Task<VechainTxResponse> CreateVechainInspect(string contractAddress, CreateVechainInspect model)
        {
            var result = await _vechainInteraction.CreateNewVechainInspect(contractAddress,model);
            return result;
        }

        public async Task<string> CreateVechainPlan(CreatedVeChainPlan model)
        {
            var result = await _vechainInteraction.CreateNewVechainPlan(model);
            return result;
        }

        public async Task<VechainTxResponse> CreateVechainTask(string contractAddress, CreateVechainTask model)
        {
            var result = await _vechainInteraction.CreateNewVechainTask(contractAddress, model);
            return result;
        }

        public async Task<VechainResponse> GetVechainPlanResponseByContractAddress(string contractAddress)
        {
           var result = await _vechainInteraction.GetTransactionByContractAddress(contractAddress);
            if (result == null) throw new Exception("Not have any Address");
            if (result.Status == 500) throw new Exception("Fail");
           return result;
        }
    }
}
