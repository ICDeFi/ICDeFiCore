using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeCoreApp.Data.Enums
{
    public enum KYCType
    {
        [Description("Chờ Duyệt")]
        Pending = 1,
        [Description("Từ Chối")]
        Reject = 2,
        [Description("Xác Nhận")]
        Approval = 3,
        [Description("Khóa")]
        Lock = 4
    }
}
