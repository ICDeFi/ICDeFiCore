using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class PageRepository : EFRepository<Page, int>, IPageRepository
    {
        public PageRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
