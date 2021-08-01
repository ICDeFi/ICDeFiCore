using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class EnterpriseFieldRepository : EFRepository<EnterpriseField, int>, IEnterpriseFieldRepository
    {
        public EnterpriseFieldRepository(AppDbContext context) : base(context)
        {
        }
    }
}
