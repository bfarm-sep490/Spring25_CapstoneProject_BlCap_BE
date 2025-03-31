using Google.Apis.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nethereum.HdWallet;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.Signer;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Utilities.Encoders;
using System.Net.Http;
using System.Text;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VechainController : ControllerBase
    {
        const string address = "0x29C4A96225D5AFD91D0FA44973e933eaf1cFcE91";
        const string abi = @"[
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": true,
          ""internalType"": ""uint256"",
          ""name"": ""id"",
          ""type"": ""uint256""
        },
        {
          ""indexed"": false,
          ""internalType"": ""string"",
          ""name"": ""plan_name"",
          ""type"": ""string""
        }
      ],
      ""name"": ""PlanCreated"",
      ""type"": ""event""
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": true,
          ""internalType"": ""uint256"",
          ""name"": ""id"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""PlanUpdated"",
      ""type"": ""event""
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": true,
          ""internalType"": ""uint256"",
          ""name"": ""plan_id"",
          ""type"": ""uint256""
        },
        {
          ""indexed"": false,
          ""internalType"": ""string"",
          ""name"": ""task_type"",
          ""type"": ""string""
        }
      ],
      ""name"": ""TaskSummaryUpdated"",
      ""type"": ""event""
    },
    {
      ""inputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": ""plan_id"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""approvePlan"",
      ""outputs"": [],
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    },
    {
      ""inputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""caring_task_summaries"",
      ""outputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": ""plan_id"",
          ""type"": ""uint256""
        },
        {
          ""internalType"": ""uint256"",
          ""name"": ""total_tasks"",
          ""type"": ""uint256""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""overall_status"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""uint256"",
          ""name"": ""last_updated"",
          ""type"": ""uint256""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""inputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": ""plant_id"",
          ""type"": ""uint256""
        },
        {
          ""internalType"": ""uint256"",
          ""name"": ""yield_id"",
          ""type"": ""uint256""
        },
        {
          ""internalType"": ""uint256"",
          ""name"": ""expert_id"",
          ""type"": ""uint256""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""plan_name"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""uint256"",
          ""name"": ""start_date"",
          ""type"": ""uint256""
        },
        {
          ""internalType"": ""uint256"",
          ""name"": ""end_date"",
          ""type"": ""uint256""
        },
        {
          ""internalType"": ""uint256"",
          ""name"": ""estimated_product"",
          ""type"": ""uint256""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""qr_code"",
          ""type"": ""string""
        }
      ],
      ""name"": ""createPlan"",
      ""outputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    },
    {
      ""inputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": ""plan_id"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""getPlan"",
      ""outputs"": [
        {
          ""components"": [
            {
              ""internalType"": ""uint256"",
              ""name"": ""id"",
              ""type"": ""uint256""
            },
            {
              ""internalType"": ""uint256"",
              ""name"": ""plant_id"",
              ""type"": ""uint256""
            },
            {
              ""internalType"": ""uint256"",
              ""name"": ""yield_id"",
              ""type"": ""uint256""
            },
            {
              ""internalType"": ""uint256"",
              ""name"": ""expert_id"",
              ""type"": ""uint256""
            },
            {
              ""internalType"": ""string"",
              ""name"": ""plan_name"",
              ""type"": ""string""
            },
            {
              ""internalType"": ""uint256"",
              ""name"": ""start_date"",
              ""type"": ""uint256""
            },
            {
              ""internalType"": ""uint256"",
              ""name"": ""end_date"",
              ""type"": ""uint256""
            },
            {
              ""internalType"": ""string"",
              ""name"": ""status"",
              ""type"": ""string""
            },
            {
              ""internalType"": ""uint256"",
              ""name"": ""estimated_product"",
              ""type"": ""uint256""
            },
            {
              ""internalType"": ""string"",
              ""name"": ""qr_code"",
              ""type"": ""string""
            },
            {
              ""internalType"": ""bool"",
              ""name"": ""is_approved"",
              ""type"": ""bool""
            },
            {
              ""internalType"": ""uint256"",
              ""name"": ""caring_task_count"",
              ""type"": ""uint256""
            },
            {
              ""internalType"": ""uint256"",
              ""name"": ""harvesting_task_count"",
              ""type"": ""uint256""
            },
            {
              ""internalType"": ""uint256"",
              ""name"": ""packaging_task_count"",
              ""type"": ""uint256""
            }
          ],
          ""internalType"": ""struct IPlanManagement.Plan"",
          ""name"": """",
          ""type"": ""tuple""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""inputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""harvesting_task_summaries"",
      ""outputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": ""plan_id"",
          ""type"": ""uint256""
        },
        {
          ""internalType"": ""uint256"",
          ""name"": ""total_tasks"",
          ""type"": ""uint256""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""overall_status"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""uint256"",
          ""name"": ""total_harvested_quantity"",
          ""type"": ""uint256""
        },
        {
          ""internalType"": ""uint256"",
          ""name"": ""last_updated"",
          ""type"": ""uint256""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""inputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""packaging_task_summaries"",
      ""outputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": ""plan_id"",
          ""type"": ""uint256""
        },
        {
          ""internalType"": ""uint256"",
          ""name"": ""total_tasks"",
          ""type"": ""uint256""
        },
        {
          ""internalType"": ""uint256"",
          ""name"": ""total_packed_quantity"",
          ""type"": ""uint256""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""overall_status"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""uint256"",
          ""name"": ""last_updated"",
          ""type"": ""uint256""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""inputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": ""plan_id"",
          ""type"": ""uint256""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""status"",
          ""type"": ""string""
        }
      ],
      ""name"": ""updatePlanStatus"",
      ""outputs"": [],
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    } 
  ]";
        HttpClient _httpClient;
        public VechainController(System.Net.Http.IHttpClientFactory httpClientFactory
)
        {
            _httpClient = httpClientFactory.CreateClient();

        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePlan()
        {      


            try
            {
                // Tạo khóa riêng tư ngẫu nhiên (trong thực tế nên lưu an toàn)
                var ecKey = EthECKey.GenerateKey();
                string privateKey = ecKey.GetPrivateKeyAsBytes().ToHex(true);

                // Tạo tài khoản từ khóa riêng tư
                var account = new Account(privateKey);

                // Khởi tạo Web3 với tài khoản và URL của VeChain TestNet
                var web3 = new Web3(account, "https://testnet.veblocks.net");

                // Lấy đối tượng hợp đồng
                var contract = web3.Eth.GetContract(abi, address);

                // Lấy hàm "createPlan" từ hợp đồng
                var createPlanFunction = contract.GetFunction("createPlan");

                // Các tham số cho hàm createPlan
                var plantId = 1;
                var yieldId = 2;
                var expertId = 3;
                var planName = "Test Plan";
                var startDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                var endDate = DateTimeOffset.UtcNow.AddDays(7).ToUnixTimeSeconds();
                var estimatedProduct = 100;
                var qrCode = "QR_CODE_DATA";

                // Cấu hình giao dịch
                var gasPrice = Web3.Convert.ToWei(20, UnitConversion.EthUnit.Gwei);
                var gasLimit = new HexBigInteger(300000);

                // VeChain sử dụng cơ chế fee delegation (MPP), nên cần xử lý đặc biệt
                // 1. Tạo giao dịch raw
                var data = createPlanFunction.GetData(plantId, yieldId, expertId, planName, startDate, endDate, estimatedProduct, qrCode).ToHexUTF8();
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);

                string dataHex = dataBytes.ToHex(true);

                // Tạo clauses cho giao dịch VeChain (tương tự như Ethereum transaction input)
                var clauseObject = new
                {
                    to = address,  // Đảm bảo rằng address được chuyển đổi thành chuỗi hex hợp lệ
                    value = "0x0",  // Đảm bảo rằng giá trị là chuỗi hex hợp lệ
                    data = data,  // Đảm bảo rằng data được chuyển thành chuỗi hex hợp lệ
                };

                // Tạo transaction body
                var transactionBody = new
                {
                    chainTag = "0x27",  // Đảm bảo chainTag đúng
                    blockRef = "0x014580240143ccd58608048aa1b548c4e7bf3c4fdc2ba767abaf3f6ede8d8f01",  // Kiểm tra lại blockRef có hợp lệ
                    expiration = 720,
                    clauses = new[] { clauseObject },
                    gasPriceCoef = 0,
                    gas = gasLimit.HexValue,  // Kiểm tra gasLimit.HexValue có hợp lệ không
                    dependsOn = (string)null,
                    nonce = new Random().Next(1000000).ToString(),  // Sử dụng một số ngẫu nhiên cho nonce thay vì DateTime
                };
                
                // Tiến hành gửi yêu cầu để lấy chữ ký từ nhà tài trợ (MPP)
                var sponsorUrl = "https://sponsor-testnet.vechain.energy/by/819";
                var sponsorRequest = new
                {
                    origin = account.Address,
                    raw = JsonConvert.SerializeObject(transactionBody)
                };

                var sponsorContent = new StringContent(
                    JsonConvert.SerializeObject(sponsorRequest),
                    Encoding.UTF8,
                    "application/json"
                );

                var sponsorResponse = await _httpClient.PostAsync(sponsorUrl, sponsorContent);

                if (!sponsorResponse.IsSuccessStatusCode)
                {
                    return BadRequest(new
                    {
                        Error = "Failed to get sponsor signature",
                        Details = await sponsorResponse.Content.ReadAsStringAsync() +" "+JsonConvert.SerializeObject(transactionBody)
                    });
                }

              

                var sponsorData = JsonConvert.DeserializeObject<SponsorResponse>(
                    await sponsorResponse.Content.ReadAsStringAsync());

                // 3. Ký giao dịch với chữ ký của cả người gửi và nhà tài trợ
                // Giả sử sponsorData.Signature chứa chữ ký của nhà tài trợ

                // Ký transaction body bằng khóa riêng tư của người gửi
                var messageToSign = JsonConvert.SerializeObject(transactionBody).HexToByteArray();
                var signer = new EthereumMessageSigner();
                var signature = signer.SignAndCalculateV(Sha3Keccack.Current.CalculateHash(messageToSign),
                                                        ecKey.GetPrivateKeyAsBytes());
                var r = signature.R.ToHex();
                var s = signature.S.ToHex();
                var v = signature.V.ToHex();
                var combinedSignature = "0x" + r + s + v;
                // 4. Kết hợp giao dịch đã ký với chữ ký của nhà tài trợ
                var signedTx = new
                {
                    raw = JsonConvert.SerializeObject(transactionBody),
                    signature = combinedSignature,
                    sponsorSignature = sponsorData.Signature
                };

                // 5. Gửi giao dịch đã ký lên mạng VeChain
                var broadcastUrl = "https://testnet.veblocks.net/transactions";
                var broadcastContent = new StringContent(
                    JsonConvert.SerializeObject(signedTx),
                    Encoding.UTF8,
                    "application/json");

                var broadcastResponse = await _httpClient.PostAsync(broadcastUrl, broadcastContent);

                if (!broadcastResponse.IsSuccessStatusCode)
                {
                    return BadRequest(new
                    {
                        Error = "Failed to broadcast transaction",
                        Details = await broadcastResponse.Content.ReadAsStringAsync()
                    });
                }

                var txResult = JsonConvert.DeserializeObject<TransactionResponse>(
                    await broadcastResponse.Content.ReadAsStringAsync());

                return Ok(new
                {
                    TransactionId = txResult.Id,
                    Message = "Plan created successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlan(int id)
        {
            try
            {
                // Tạo kết nối đến VeChain TestNet (không cần tài khoản cho truy vấn)
                var web3 = new Web3("https://testnet.veblocks.net");

                // Lấy đối tượng hợp đồng
                var contract = web3.Eth.GetContract(abi, address);

                // Giả sử có hàm "getPlan" trong hợp đồng
                var getPlanFunction = contract.GetFunction("getPlan");
                var plan = await getPlanFunction.CallDeserializingToObjectAsync<PlanDetail>(id);

                return Ok(plan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
    public class SponsorResponse
    {
        public string Signature { get; set; }
    }

    public class TransactionResponse
    {
        public string Id { get; set; }
    }

    public class PlanDetail
    {
        public int PlantId { get; set; }
        public int YieldId { get; set; }
        public int ExpertId { get; set; }
        public string PlanName { get; set; }
        public long StartDate { get; set; }
        public long EndDate { get; set; }
        public int EstimatedProduct { get; set; }
        public string QrCode { get; set; }
    }

}
