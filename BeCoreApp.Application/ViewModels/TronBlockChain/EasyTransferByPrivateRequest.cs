using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace BeCoreApp.Application.ViewModels.TronBlockChain
{
    public class EasyTransferByPrivateRequest
    {
        public string privateKey { get; set; }
        public string toAddress { get; set; }
        public BigInteger amount { get; set; }
    }

    public class EasyTransferAssetByPrivateRequest : EasyTransferByPrivateRequest
    {
        public string assetAddress { get; set; }
    }
}
