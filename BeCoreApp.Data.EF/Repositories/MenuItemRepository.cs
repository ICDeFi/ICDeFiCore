using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class MenuItemRepository : EFRepository<MenuItem, int>, IMenuItemRepository
    {
        public MenuItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
