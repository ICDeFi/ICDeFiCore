using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class ProvinceRepository : EFRepository<Province, int>, IProvinceRepository
    {
        public ProvinceRepository(AppDbContext context) : base(context)
        {
        }
    }
}
