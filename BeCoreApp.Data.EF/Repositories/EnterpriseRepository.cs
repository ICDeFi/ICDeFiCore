using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class EnterpriseRepository : EFRepository<Enterprise, int>, IEnterpriseRepository
    {
        public EnterpriseRepository(AppDbContext context) : base(context)
        {
        }
    }
}
