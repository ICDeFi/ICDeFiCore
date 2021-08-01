using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Enums;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeCoreApp.Application.Interfaces
{
    public interface ITransactionHistoryService
    {
        PagedResult<TransactionHistoryViewModel> GetAllPaging(string keyword, string appUserId, int pageIndex, int pageSize);
        TransactionHistoryViewModel GetById(int id);
        bool IsExist(string transactionHash);

        void Add(TransactionHistoryViewModel Model);
        void Reject(int id, string note);
        void Save();
    }
}
