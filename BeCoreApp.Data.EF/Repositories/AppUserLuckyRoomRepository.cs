using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class AppUserLuckyRoomRepository : EFRepository<AppUserLuckyRoom, int>, IAppUserLuckyRoomRepository
    {
        public AppUserLuckyRoomRepository(AppDbContext context) : base(context)
        {
        }
    }
}
