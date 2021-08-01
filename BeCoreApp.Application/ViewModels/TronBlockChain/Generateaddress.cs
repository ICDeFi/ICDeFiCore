using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Application.ViewModels.TronBlockChain
{
    public class Generateaddress
    {
        public bool success { get; set; }
        public GenerateaddressResult result { get; set; }
    }
    public class GenerateaddressResult
    {
        public string privateKey { get; set; }
        public string publicKey { get; set; }
        public GenerateaddressResultAddress address { get; set; }
    }
    public class GenerateaddressResultAddress
    {
        public string base58 { get; set; }
        public string hex { get; set; }
    }
}
