using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class StreetRepository : EFRepository<Street, int>, IStreetRepository
    {
        public StreetRepository(AppDbContext context) : base(context)
        {
        }
    }
}
