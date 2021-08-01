using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class ProjectRepository : EFRepository<Project, int>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context)
        {
        }
    }
}
