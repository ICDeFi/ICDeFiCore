using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeCoreApp.Data.Enums
{
    public enum ExchangeType
    {
        [Description("VS TOKEN 1")]
        VSTOKEN1 = 1,
        [Description("VÍ GAME")]
        WALLETGAME = 2,
        [Description("VÍ WIN")]
        WALLETWIN = 3,
        [Description("VS TOKEN 2")]
        VSTOKEN2 = 4
    }
}
