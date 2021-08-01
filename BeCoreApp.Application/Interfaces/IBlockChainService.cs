using BeCoreApp.Application.ViewModels.BlockChain;
using BeCoreApp.Utilities.Dtos;
using System.Threading.Tasks;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3.Accounts;

namespace BeCoreApp.Application.Interfaces
{
    public interface IBlockChainService
    {
        Account CreateEthereumAccount();
        CoinMarKetCapInfoViewModel GetListingLatest(int startIndex, int limitItem, string unitConvertTo);
        Task<Transaction> GetTransactionByTransactionID(string transactionID);
        Task<decimal> GetERC20Balance(string owner,string smartContractAddress, int decimalPlaces);
        Task<decimal> GetEtherBalance(string publishKey);
        Task<string> SendERC20Async(string privateKeyERC20, string receiverAddress, string contractAddress, decimal tokenAmount, int decimalPlaces);
        Task<TransactionReceipt> SendEthAsync(string privateKey, string receiverAddress, decimal tokenAmount);
    }
}
