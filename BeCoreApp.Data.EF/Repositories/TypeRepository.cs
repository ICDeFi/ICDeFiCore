using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class TypeRepository : EFRepository<BeCoreApp.Data.Entities.Type, int>, ITypeRepository
    {
        public TypeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
