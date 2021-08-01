using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.Interfaces
{
    public interface ISwitchable
    {
        Status Status { get; set; }
    }
}
