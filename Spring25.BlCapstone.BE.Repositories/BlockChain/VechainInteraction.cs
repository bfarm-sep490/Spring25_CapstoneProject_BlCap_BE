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
        Task<VechainPlan> GetTransactionByContractAddress(string contractAddress);
        Task<VechainTxResponse> CreateVechainPlan(CreatedVeChainPlan createdVeChainPlan);
        
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

        public async Task<VechainTxResponse> CreateVechainPlan(CreatedVeChainPlan createdVeChainPlan)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("plans", createdVeChainPlan);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<VechainTxResponse>();
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

        public async Task<VechainPlan> GetTransactionByContractAddress(string contractAddress)
        {
            try
            {
                var response = await _httpClient.GetAsync($"plans/{contractAddress}");

                if (response.IsSuccessStatusCode)
                {
                    var wrapper = await response.Content.ReadFromJsonAsync<VechainResponse>();
                    return wrapper?.Data;
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
