using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class UnitRepository : EFRepository<Unit, int>, IUnitRepository
    {
        public UnitRepository(AppDbContext context) : base(context)
        {
        }
    }
}
