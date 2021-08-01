using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class ProjectImageRepository : EFRepository<ProjectImage, int>, IProjectImageRepository
    {
        public ProjectImageRepository(AppDbContext context) : base(context)
        {
        }
    }
}
