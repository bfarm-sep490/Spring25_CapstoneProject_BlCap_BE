using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.BlockChain
{
    public interface IVechainInteraction
    {
        Task<VechainResponse> GetTransactionByContractAddress(string contractAddress);
        Task<string> CreateNewVechainPlan(CreatedVeChainPlan createdVeChainPlan);
        Task<VechainTxResponse> CreateNewVechainTask(string addressContract,CreateVechainTask createVechainTask);
        Task<VechainTxResponse> CreateNewVechainInspect(string addressContract,CreateVechainInspect createVechainInspect);
    }
    public class VechainInteraction:IVechainInteraction
    {
        private readonly HttpClient _httpClient;

        public VechainInteraction()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:3000/vechain/contracts/")
            };
        }

        public async Task<VechainTxResponse> CreateNewVechainInspect(string addressContract,CreateVechainInspect createVechainInspect)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{addressContract}/inspect", createVechainInspect);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<VechainTxResponse>();
                    if (result.TxId == null) throw new Exception($"Error creating VeChain plan");
                    return result;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error creating VeChain plan: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception during VeChain plan creation", ex);
            }
        }

        public async Task<string> CreateNewVechainPlan(CreatedVeChainPlan createdVeChainPlan)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("plans", createdVeChainPlan);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<VechainTxResponse>();
                    if (result.TxId == null) throw new Exception($"Error creating VeChain plan");             
                    return result.TxId.ContractAddress;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error creating VeChain plan: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception during VeChain plan creation", ex);
            }
        }

        public async Task<VechainTxResponse> CreateNewVechainTask(string addressContract, CreateVechainTask createVechainTask)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{addressContract}/task", createVechainTask);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<VechainTxResponse>();
                    if (result.TxId == null) throw new Exception($"Error creating VeChain plan");
                    return result;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error creating VeChain plan: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception during VeChain plan creation", ex);
            }
        }

        public async Task<VechainResponse> GetTransactionByContractAddress(string contractAddress)
        {
            try
            {
                var response = await _httpClient.GetAsync($"plans/{contractAddress}");

                if (response.IsSuccessStatusCode)
                {
                    var wrapper = await response.Content.ReadFromJsonAsync<VechainResponse>();
                    return wrapper;
                }
                else
                {
                    throw new Exception($"Failed to fetch data: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching transaction from contract address", ex);
            }
        }
    }
}
