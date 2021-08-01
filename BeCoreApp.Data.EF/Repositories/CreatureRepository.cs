using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class CreatureRepository : EFRepository<Creature, int>, ICreatureRepository
    {
        public CreatureRepository(AppDbContext context) : base(context)
        {
        }
    }
}
