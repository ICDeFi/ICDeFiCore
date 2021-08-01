using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Enums;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IWalletTransferService
    {
        PagedResult<WalletTransferViewModel> GetAllPaging(string keyword, int pageIndex, int pageSize);

        void Add(WalletTransferViewModel Model);

        void Save();
    }
}
