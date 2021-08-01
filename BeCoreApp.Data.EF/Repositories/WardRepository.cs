using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class WardRepository : EFRepository<Ward, int>, IWardRepository
    {
        public WardRepository(AppDbContext context) : base(context)
        {
        }
    }
}
