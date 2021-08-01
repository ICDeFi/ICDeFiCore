using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class ExchangeRepository : EFRepository<Exchange, int>, IExchangeRepository
    {
        public ExchangeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
