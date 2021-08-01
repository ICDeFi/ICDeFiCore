using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeCoreApp.Data.Enums
{
    public enum TransactionType
    {
        [Description("Withdraw DOLP")]
        WithdrawDOLP = 1,
        [Description("Withdraw TRX")]
        WithdrawTRX = 2,
        [Description("Swap")]
        Swap = 3,
        [Description("Buy")]
        Buy = 4,
        [Description("Affiliate Withdraw")]
        AffiliateWithdraw = 5,
        [Description("Affiliate Buy")]
        AffiliateBuy = 6,
        [Description("Play Lucky Buy Rounds")]
        PlayLuckyBuyRounds = 7,
        [Description("Win Lucky Buy Rounds")]
        WinLuckyBuyRounds = 8,
        [Description("Win Lucky Round Free")]
        WinLuckyRoundFree = 9,
        [Description("Win Lucky Round Member")]
        WinLuckyRoundMember = 10
    }
}
