using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.Interfaces
{
    public interface IHasOwner<T>
    {
        T OwnerId { get; set; }
    }
}
