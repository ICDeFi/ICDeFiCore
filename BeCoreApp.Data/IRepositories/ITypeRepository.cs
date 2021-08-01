using BeCoreApp.Data.Entities;
using BeCoreApp.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.IRepositories
{
    public interface ITypeRepository : IRepository<BeCoreApp.Data.Entities.Type, int>
    {
    }
}
