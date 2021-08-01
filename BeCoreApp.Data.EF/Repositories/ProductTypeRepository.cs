using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class ProductTypeRepository : EFRepository<ProductType, string>, IProductTypeRepository
    {
        public ProductTypeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
