using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class ProjectLibraryRepository : EFRepository<ProjectLibrary, int>, IProjectLibraryRepository
    {
        public ProjectLibraryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
