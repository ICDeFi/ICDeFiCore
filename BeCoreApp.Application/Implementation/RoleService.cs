using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using BeCoreApp.Infrastructure.Interfaces;
using BeCoreApp.Utilities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeCoreApp.Application.Implementation
{
    public class RoleService : IRoleService
    {
        private RoleManager<AppRole> _roleManager;
        private IFunctionService _functionService;
        private IFunctionRepository _functionRepository;
        private IPermissionRepository _permissionRepository;
        private IUnitOfWork _unitOfWork;
        public RoleService(
            RoleManager<AppRole> roleManager,
            IUnitOfWork unitOfWork,
            IFunctionService functionService,
            IFunctionRepository functionRepository,
            IPermissionRepository permissionRepository)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _functionService = functionService;
            _functionRepository = functionRepository;
            _permissionRepository = permissionRepository;
        }

        public PagedResult<AppRoleViewModel> GetAllPagingAsync(string keyword, int page, int pageSize)
        {
            var query = _roleManager.Roles;
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword)
                || x.Description.Contains(keyword));

            int totalRow = query.Count();
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var data = query.Select(x => new AppRoleViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();

            var paginationSet = new PagedResult<AppRoleViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public async Task<List<AppRoleViewModel>> GetAllAsync()
        {
            return await _roleManager.Roles
                .Select(x => new AppRoleViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync();
        }

        public async Task<AppRoleViewModel> GetById(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            return new AppRoleViewModel
            {
                Description = role.Description,
                Id = role.Id,
                Name = role.Name
            };
        }

        public async Task<bool> AddAsync(AppRoleViewModel roleVm)
        {
            var role = new AppRole()
            {
                Name = roleVm.Name,
                Description = roleVm.Description
            };
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }

        public async Task UpdateAsync(AppRoleViewModel roleVm)
        {
            var role = await _roleManager.FindByIdAsync(roleVm.Id.ToString());
            role.Description = roleVm.Description;
            role.Name = roleVm.Name;
            await _roleManager.UpdateAsync(role);
        }

        public async Task DeleteAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            await _roleManager.DeleteAsync(role);
        }

        public Task<bool> CheckPermission(string functionId, string action, string[] roles)
        {
            var functions = _functionRepository.FindAll();
            var permissions = _permissionRepository.FindAll();
            var query = from f in functions
                        join p in permissions on f.Id equals p.FunctionId
                        join r in _roleManager.Roles on p.RoleId equals r.Id
                        where roles.Contains(r.Name) && f.Id == functionId
                        //&& ((p.CanCreate && action == "Create")
                        //|| (p.CanUpdate && action == "Update")
                        //|| (p.CanDelete && action == "Delete")
                        //|| (p.CanRead && action == "Read")
                        //)
                        select p;
            return query.AnyAsync();
        }

        public List<AppRole> GetAllRoleToSetPermission()
        {
            var appRoles = _roleManager.Roles.ToList();
            return appRoles;
        }

        public List<Permission> GetAllPermissionToSetPermission()
        {
            var permissions = _permissionRepository.FindAll().ToList();
            return permissions;
        }

        public List<AccessControlDTO> GetAllAccessControl()
        {
            var models = new List<AccessControlDTO>();

            var functions = _functionService.GetAllFunctionToSetPermission();

            var appRoles = GetAllRoleToSetPermission();

            var permissions = GetAllPermissionToSetPermission();

            foreach (var function in functions)
            {

                var accessControl = new AccessControlDTO();
                accessControl.Root = true;
                accessControl.Id = function.Id;
                accessControl.Name = function.Name;
                models.Add(accessControl);


                string[] actions = function.ActionControl.ToLower().Split(',');

                foreach (string action in actions.Where(x => !string.IsNullOrWhiteSpace(x)))
                {

                    accessControl = new AccessControlDTO();
                    accessControl.Root = false;
                    accessControl.Id = function.Id;
                    accessControl.Name = function.Name + " : " + action.First().ToString().ToUpper() + string.Join(string.Empty, action.Skip(1));
                    accessControl.Action = action;

                    foreach (var role in appRoles)
                    {
                        var currentPermission = permissions
                            .FirstOrDefault(p => p.FunctionId == function.Id && p.RoleId == role.Id);

                        if (currentPermission != null && currentPermission.Feature.Contains(action))
                            accessControl.IsPermissions.Add(true);
                        else
                            accessControl.IsPermissions.Add(false);
                    }

                    models.Add(accessControl);
                }
            }

            return models;
        }

        public void UpdatePermission(Permission permission)
        {
            _permissionRepository.Update(permission);
            _unitOfWork.Commit();
        }

        public void AddPermission(Permission permission)
        {
            _permissionRepository.Add(permission);
            _unitOfWork.Commit();
        }
        public void DeletePermission(int id)
        {
            _permissionRepository.Remove(id);
            _unitOfWork.Commit();
        }

        public void SetPermission(List<PermissionViewModel> permissionVms, Guid roleId)
        {
            var permissions = Mapper.Map<List<PermissionViewModel>, List<Permission>>(permissionVms);
            var oldPermission = _permissionRepository.FindAll().Where(x => x.RoleId == roleId).ToList();
            if (oldPermission.Count > 0)
            {
                _permissionRepository.RemoveMultiple(oldPermission);
            }
            foreach (var permission in permissions)
            {
                _permissionRepository.Add(permission);
            }
            _unitOfWork.Commit();
        }


    }
}
