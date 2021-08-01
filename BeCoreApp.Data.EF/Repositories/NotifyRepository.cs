using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class NotifyRepository : EFRepository<Notify, int>, INotifyRepository
    {
        public NotifyRepository(AppDbContext context) : base(context)
        {
        }
    }
}
