using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;

namespace BeCoreApp.Data.EF.Repositories
{
    public class InvestRepository : EFRepository<Invest, int>, IInvestRepository
    {
        public InvestRepository(AppDbContext context) : base(context)
        {
        }
    }
}
