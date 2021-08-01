using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class DistrictRepository : EFRepository<District, int>, IDistrictRepository
    {
        public DistrictRepository(AppDbContext context) : base(context)
        {
        }
    }
}
