using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Entities;
using BeCoreApp.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeCoreApp.Application.Interfaces
{
    public interface IRoleService
    {
        PagedResult<AppRoleViewModel> GetAllPagingAsync(string keyword, int page, int pageSize);

        Task<bool> AddAsync(AppRoleViewModel userVm);

        Task DeleteAsync(Guid id);

        Task<List<AppRoleViewModel>> GetAllAsync();

        Task<AppRoleViewModel> GetById(Guid id);

        Task UpdateAsync(AppRoleViewModel userVm);

        List<Permission> GetAllPermissionToSetPermission();

        List<AppRole> GetAllRoleToSetPermission();

        List<AccessControlDTO> GetAllAccessControl();

        void UpdatePermission(Permission permission);

        void AddPermission(Permission permission);

        void DeletePermission(int id);

        void SetPermission(List<PermissionViewModel> permissions, Guid roleId);

        Task<bool> CheckPermission(string functionId, string action, string[] roles);
    }
}
