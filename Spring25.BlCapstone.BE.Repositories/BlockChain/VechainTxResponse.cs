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
        public TxId TxId { get; set; }
    }

    public class TxId
    {
        public string ContractAddress { get; set; }
        public TxReceipt TxReceipt { get; set; }
    }

    public class TxReceipt
    {
        public long GasUsed { get; set; }
        public string GasPayer { get; set; }
        public string Paid { get; set; }
        public string Reward { get; set; }
        public bool Reverted { get; set; }
        public Meta Meta { get; set; }
        public List<Output> Outputs { get; set; }
    }

    public class Meta
    {
        public string BlockID { get; set; }
        public long BlockNumber { get; set; }
        public long BlockTimestamp { get; set; }
        public string TxID { get; set; }
        public string TxOrigin { get; set; }
    }

    public class Output
    {
        public string ContractAddress { get; set; }
        public List<Event> Events { get; set; }
        public List<object> Transfers { get; set; }
    }

    public class Event
    {
        public string Address { get; set; }
        public List<string> Topics { get; set; }
        public string Data { get; set; }
    }

}
