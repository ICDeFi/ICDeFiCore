using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Application.ViewModels.Valuesshare;
using BeCoreApp.Data.Entities;
using BeCoreApp.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeCoreApp.Application.Interfaces
{
    public interface IUserService
    {

        #region Customer
        PagedResult<AppUserViewModel> GetAllCustomerPagingAsync(string keyword, int page, int pageSize);
        Task<NetworkViewModel> GetNetworkInfo(string id);
        PagedResult<AppUserViewModel> GetCustomerReferralPagingAsync(string customerId, int refIndex, string keyword, int page, int pageSize);
        #endregion

        #region User system
        PagedResult<AppUserViewModel> GetAllPagingAsync(string keyword, int page, int pageSize);

        List<AppUserTreeViewModel> GetTreeAll();

        Task<AppUserViewModel> GetById(string id);

        Task<bool> AddAsync(AppUserViewModel userVm);

        Task UpdateAsync(AppUserViewModel userVm);

        Task DeleteAsync(string id);

        #endregion
    }
}
