using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.Interfaces
{
    public interface IHasSoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
