using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Application.ViewModels.BlockChain
{
    public class TransferViewModel
    {
        public int WithdrawType { get; set; }
        public decimal Amount { get; set; }
        public string AddressReceiving { get; set; }
    }
}
