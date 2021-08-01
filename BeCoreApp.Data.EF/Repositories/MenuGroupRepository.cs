using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class MenuGroupRepository : EFRepository<MenuGroup, int>, IMenuGroupRepository
    {
        public MenuGroupRepository(AppDbContext context) : base(context)
        {
        }
    }
}
