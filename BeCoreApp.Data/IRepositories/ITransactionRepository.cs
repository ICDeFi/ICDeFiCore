using BeCoreApp.Data.Entities;
using BeCoreApp.Infrastructure.Interfaces;

namespace BeCoreApp.Data.IRepositories
{
    public interface ITransactionRepository : IRepository<CustomerTransaction, int>
    {

    }
}