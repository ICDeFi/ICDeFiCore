using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeCoreApp.Data.Enums
{
    public enum BillStatus
    {
        [Description("New bill")]
        New,
        [Description("In Progress")]
        InProgress,
        [Description("Returned")]
        Returned,
        [Description("Cancelled")]
        Cancelled,
        [Description("Completed")]
        Completed
    }
}
