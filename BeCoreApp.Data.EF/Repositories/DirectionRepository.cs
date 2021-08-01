using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class DirectionRepository : EFRepository<Direction, int>, IDirectionRepository
    {
        public DirectionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
