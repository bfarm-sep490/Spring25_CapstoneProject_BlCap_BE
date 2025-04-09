using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.BlockChain
{
    public class VechainTxResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public TxId TxId { get; set; } = new TxId();
     }

    public class TxId
    {
        public string ContractAddress { get; set; } = null;
        public object TxReceipt { get; set; }
    }
}
