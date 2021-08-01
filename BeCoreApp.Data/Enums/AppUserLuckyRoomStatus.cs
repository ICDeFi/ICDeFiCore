using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeCoreApp.Data.Enums
{
    public enum AppUserLuckyRoomStatus
    {
        [Description("Waiting")]
        Waiting = 0,
        [Description("Win")]
        Win = 1,
        [Description("Lose")]
        Lose = 2
    }
}
