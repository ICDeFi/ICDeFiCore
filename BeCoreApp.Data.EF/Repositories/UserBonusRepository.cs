using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;

namespace BeCoreApp.Data.EF.Repositories
{
    public class UserBonusRepository : EFRepository<UserBonus, int>, IUserBonusRepository
    {
        public UserBonusRepository(AppDbContext context) : base(context)
        {
        }
    }
}
