using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeCoreApp.Web.Api
{
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("")]
    public class BlockChainController : Controller
    {
        //public static CryptoCurrency blockchain = new CryptoCurrency();

        //[HttpPost("transactions/new")]
        //public IActionResult new_transaction([FromBody]Transaction transaction)
        //{
        //    var rsp = blockchain.CreateTransaction(transaction);
        //    return Ok(rsp);
        //}
        //[HttpGet("transactions/get")]
        //public IActionResult get_transactions()
        //{
        //    var rsp = new { transactions = blockchain.GetTransactions() };
        //    return Ok(rsp);
        //}
        //[HttpGet("chain")]
        //public IActionResult full_chain()
        //{
        //    var blocks = blockchain.GetBlocks();
        //    var rsp = new { chain = blocks, length = blocks.Count };
        //    return Ok(rsp);
        //}
        //[HttpGet("mine")]
        //public IActionResult mine()
        //{
        //    var block = blockchain.Mine();
        //    var rsp = new
        //    {
        //        message = "New Block Forged",
        //        block_number = block.Index,
        //        transactions = block.Transactions.ToArray(),
        //        nonce = block.Proof,
        //        previous_hash = block.PreviousHash
        //    };

        //    return Ok(rsp);
        //}
        //[HttpPost("nodes/register")]
        //public IActionResult register_nodes(string[] nodes)//{ "Urls": ["localhost:54321", "localhost:54345", "localhost:12321"] }
        //{
        //    blockchain.RegisterNodes(nodes);
        //    var rsp = new
        //    {
        //        message = "New nodes have been added",
        //        total_nodes = nodes.Count()//'total_nodes': [node for node in blockchain.nodes],
        //    };

        //    return Created("", rsp);
        //}
        //[HttpGet("nodes/resolve")]
        //public IActionResult consensus()
        //{
        //    return Ok(blockchain.Consensus());
        //}
        //[HttpGet("nodes/get")]
        //public IActionResult get_nodes()
        //{
        //    return Ok(new { nodes = blockchain.GetNodes() });
        //}
        ////////miner/////
        //[HttpGet("wallet/miner")]
        //public IActionResult GetMinersWallet()
        //{
        //    return Ok(blockchain.GetMinersWallet());
        //}
    }
}