using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeCoreApp.Data.Enums
{
    public enum LuckyRoomType
    {
        [Description("LBR")]
        LuckyBuyRound = 0,
        [Description("LRF")]
        LuckyRoundFree = 1,
        [Description("LRM")]
        LuckyRoundMember = 2
    }
}
