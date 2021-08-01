using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

namespace BeCoreApp.Application.ViewModels.BlockChain
{
    [Function("transfer", "bool")]
    public class TransferFunctionBaseViewModel: FunctionMessage
    {
        [Parameter("address", "_to", 1)]
        public virtual string To { get; set; }
        [Parameter("uint256", "_value", 2)]
        public virtual BigInteger TokenAmount { get; set; }
    }
}
