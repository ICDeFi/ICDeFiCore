using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class ProjectCategoryRepository : EFRepository<ProjectCategory, int>, IProjectCategoryRepository
    {
        public ProjectCategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
