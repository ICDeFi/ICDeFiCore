using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class ClassifiedCategoryRepository : EFRepository<ClassifiedCategory, int>, IClassifiedCategoryRepository
    {
        public ClassifiedCategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
