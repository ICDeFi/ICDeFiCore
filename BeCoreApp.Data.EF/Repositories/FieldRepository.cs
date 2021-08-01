using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class FieldRepository : EFRepository<Field, int>, IFieldRepository
    {
        public FieldRepository(AppDbContext context) : base(context)
        {
        }
    }
}
