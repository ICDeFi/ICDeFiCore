using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeCoreApp.Data.Enums
{
    public enum FeedbackType
    {
        [Description("Mới")]
        New = 1,
        [Description("Đã Xem")]
        Watched = 2,
        [Description("Đã Phản Hồi")]
        Responded = 3
    }
}
