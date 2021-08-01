using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Application.ViewModels.TronBlockChain
{
    public class BaseResponse
    {
        public bool success { get; set; }
        public string result { get; set; }
        public string error { get; set; }
    }
}
