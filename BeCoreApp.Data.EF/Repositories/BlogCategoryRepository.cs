using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class BlogCategoryRepository : EFRepository<BlogCategory, int>, IBlogCategoryRepository
    {
        public BlogCategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
