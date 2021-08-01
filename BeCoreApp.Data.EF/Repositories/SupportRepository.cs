using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class SupportRepository : EFRepository<Support, int>, ISupportRepository
    {
        public SupportRepository(AppDbContext context) : base(context)
        {
        }
    }
}
