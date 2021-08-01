using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeCoreApp.Data.EF.Repositories
{
    public class WalletTransferRepository : EFRepository<WalletTransfer, int>, IWalletTransferRepository
    {
        public WalletTransferRepository(AppDbContext context) : base(context)
        {
        }
    }
}
