using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class OperatingEnvironmentRepository : EFRepository<OperatingEnvironment, int>, IOperatingEnvironmentRepository
    {
        public OperatingEnvironmentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
