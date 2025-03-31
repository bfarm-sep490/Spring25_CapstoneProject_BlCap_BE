using Newtonsoft.Json;
using Nethereum.Web3;
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Nethereum.Hex.HexConvertors.Extensions;
using Org.VeChain.Thor.Devkit.Transaction;
using System.Numerics;
using Nethereum.Signer;
using Nethereum.Model;
using Org.VeChain.Thor.Devkit.Extension;
using Nethereum.HdWallet;
using Org.VeChain.Thor.Devkit.Cry;
using Org.VeChain.Thor.Devkit.Abi;
using Secp256k1Net;
using CloudinaryDotNet.Core;
using AutoMapper.Internal;
using Org.VeChain.Thor.Devkit.Rlp;

namespace VeChainTransactionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeChainTransactionController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string veChainTestnetUrl = "https://testnet.veblocks.net";
        private static readonly string farmContractAddress = "0x29C4A96225D5AFD91D0FA44973e933eaf1cFcE91"; // Address of the contract
        private static readonly string abi = "{\"constant\": false,\"inputs\": [{\"name\": \"plant_id\",\"type\": \"uint256\"},{\"name\": \"yield_id\",\"type\": \"uint256\"},{\"name\": \"expert_id\",\"type\": \"uint256\"},{\"name\": \"plan_name\",\"type\": \"string\"},{\"name\": \"start_date\",\"type\": \"uint256\"},{\"name\": \"end_date\",\"type\": \"uint256\"},{\"name\": \"estimated_product\",\"type\": \"uint256\"},{\"name\": \"qr_code\",\"type\": \"string\"}],\"name\": \"createPlan\",\"outputs\": [{\"name\": \"\",\"type\": \"uint256\"}],\"payable\": false,\"stateMutability\": \"nonpayable\",\"type\": \"function\"}";

        // Tạo giao dịch
        [HttpPost("createTransaction")]
        public async Task<IActionResult> CreateTransaction()
        {
            try
            {

                /*      // Mnemonic phrase from the image
                      string mnemonic = "leisure charge april subject frown aim weekend start hip alter accident cradle"; // Use your mnemonic phrase here

                      // Create wallet from the mnemonic


                      // Get the private key for the first wallet (index 0)
                 /*     var privateKey = wallet.GetPrivateKey(0);*/
                var privateKey = Org.VeChain.Thor.Devkit.Cry.Secp256k1.GeneratePrivateKey();
                var walletVe = new SimpleWallet(privateKey);
                /*   // Tạo ví ngẫu nhiên
                   var wallet = new Wallet("dadadadadaadad", "12345");
                   var address = wallet.GetAddresses()[0];*/

                // Tạo interface từ ABI của smart contract
                AbiFuncationCoder coder = new AbiFuncationCoder(abi);
                var encodeData = coder.Encode(1, 1, 1, "0", 1, 1, 1, "0");
                // Gọi hàm createPlan


                var txbody = new Body
                {
                    ChainTag = 39,
                    BlockRef = "0x0145a4017714790d",
                    Expiration = 18,
                    GasPriceCoef = 0,
                    Gas = 21000,
                    DependsOn = "",
                    Nonce = "0xd6846cde87878603",
                    Reserved = null
                };
                txbody.Clauses.Add(new Org.VeChain.Thor.Devkit.Transaction.Clause("0x29C4A96225D5AFD91D0FA44973e933eaf1cFcE91", new BigInteger(100000), encodeData.ToHexString()));

                var transaction = new Org.VeChain.Thor.Devkit.Transaction.Transaction(txbody);

                // Gửi yêu cầu để lấy chữ ký từ sponsor
                var sponsorSignature = await GetSponsorSignature(walletVe.address, transaction);

                // Kết hợp chữ ký người dùng và chữ ký sponsor

                var originSignature = Org.VeChain.Thor.Devkit.Cry.Secp256k1.Sign(transaction.SigningHash(), privateKey);

                // Kết hợp chữ ký người dùng và chữ ký sponsor
                byte[] combinedSignature = CombineSignatures(originSignature, sponsorSignature.ToBytes());
                
                // Gửi giao dịch lên testnet
                var rawTransaction =  transaction.Encode().ToHexString();
                var txId = await PostAsync("/transactions", new { raw = rawTransaction });

                return Ok(new { Status = 200, Message = "Transaction submitted", TxId = txId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = 500, Message = "Transaction failed", Error = ex.Message });
            }
        }

        // Gửi yêu cầu lấy chữ ký từ sponsor
        private static async Task<string> GetSponsorSignature(string walletAddress, Org.VeChain.Thor.Devkit.Transaction.Transaction transaction)
        {  

            var raw = transaction.Encode().ToHexString();
            var response = await client.PostAsync("https://sponsor-testnet.vechain.energy/by/819", new StringContent(JsonConvert.SerializeObject(new
            {
                origin = walletAddress,
                raw = raw
            }), Encoding.UTF8, "application/json"));

            var content = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(content);
            return result.signature;
        }

        // Kết hợp chữ ký người dùng và sponsor
        private static byte[] CombineSignatures(byte[] originSignature, byte[] sponsorSignature)
        {
            // Kết hợp chữ ký người dùng và nhà tài trợ
            var combinedSignature = new byte[originSignature.Length + sponsorSignature.Length];
            Buffer.BlockCopy(originSignature, 0, combinedSignature, 0, originSignature.Length);
            Buffer.BlockCopy(sponsorSignature, 0, combinedSignature, originSignature.Length, sponsorSignature.Length);
            return combinedSignature;
        }

        // Gửi POST request
        private static async Task<dynamic> PostAsync(string endpoint, object data)
        {
            var response = await client.PostAsync(veChainTestnetUrl + endpoint, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject(content);
        }
    }

    // Các lớp hỗ trợ
    
}
