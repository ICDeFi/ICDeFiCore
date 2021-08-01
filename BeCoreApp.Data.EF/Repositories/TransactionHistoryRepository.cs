using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class TransactionHistoryRepository : EFRepository<TransactionHistory, int>, ITransactionHistoryRepository
    {
        public TransactionHistoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
