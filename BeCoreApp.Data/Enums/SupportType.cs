using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeCoreApp.Data.Enums
{
    public enum SupportType
    {
        [Description("Mới")]
        New = 1,
        [Description("Đã Phản Hồi")]
        Responded = 2
    }
}
