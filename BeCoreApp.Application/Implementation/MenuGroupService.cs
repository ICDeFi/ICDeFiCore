using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.IRepositories;
using BeCoreApp.Infrastructure.Interfaces;
using BeCoreApp.Utilities.Dtos;
using BeCoreApp.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BeCoreApp.Application.Implementation
{
    public class MenuGroupService : IMenuGroupService
    {
        private IMenuGroupRepository _menuGroupRepository;
        private IUnitOfWork _unitOfWork;

        public MenuGroupService(IMenuGroupRepository menuGroupRepository, IUnitOfWork unitOfWork)
        {
            _menuGroupRepository = menuGroupRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<MenuGroupViewModel> GetAllPaging(string startDate, string endDate, string keyword, string roleId, int pageIndex, int pageSize)
        {
            var query = _menuGroupRepository.FindAll();
            if (!string.IsNullOrEmpty(startDate))
            {
                DateTime start = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                query = query.Where(x => x.DateCreated >= start);
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                DateTime end = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                query = query.Where(x => x.DateCreated <= end);
            }

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            if (!string.IsNullOrWhiteSpace(roleId))
                query = query.Where(x => x.AppRoleId.ToString() == roleId);

            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(x => new MenuGroupViewModel()
                {
                    Id = x.Id,
                    RoleId = x.AppRoleId,
                    RoleName = x.AppRole.Name,
                    Name = x.Name,
                    Status = x.Status,
                    DateCreated = x.DateCreated,
                    DateModified = x.DateModified,
                })
                .ToList();

            return new PagedResult<MenuGroupViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public List<MenuGroupViewModel> GetAll()
        {
            return _menuGroupRepository.FindAll()
                .Where(x => x.Status == Status.Active)
                .Select(x => new MenuGroupViewModel()
                {
                    Id = x.Id,
                    RoleId = x.AppRoleId,
                    RoleName = x.AppRole.Name,
                    Name = x.Name,
                    Status = x.Status,
                    DateCreated = x.DateCreated,
                    DateModified = x.DateModified,
                }).ToList();
        }
        public MenuGroupViewModel GetByRoleId(string roleId)
        {
            var menuGroup = _menuGroupRepository.FindSingle(x => x.AppRoleId.ToString() == roleId);
            if (menuGroup == null)
                return null;

            return new MenuGroupViewModel()
            {
                Id = menuGroup.Id,
                RoleId = menuGroup.AppRoleId,
                Name = menuGroup.Name,
                Status = menuGroup.Status,
                DateCreated = menuGroup.DateCreated,
                DateModified = menuGroup.DateModified,
            };
        }


        public MenuGroupViewModel GetById(int id)
        {
            var menuGroup = _menuGroupRepository.FindById(id);
            if (menuGroup == null)
                return null;

            return new MenuGroupViewModel()
            {
                Id = menuGroup.Id,
                RoleId = menuGroup.AppRoleId,
                Name = menuGroup.Name,
                Status = menuGroup.Status,
                DateCreated = menuGroup.DateCreated,
                DateModified = menuGroup.DateModified,
            };
        }


        public void Add(MenuGroupViewModel menuGroupVm)
        {
            var menuGroup = new MenuGroup()
            {
                Id = menuGroupVm.Id,
                AppRoleId = menuGroupVm.RoleId,
                Name = menuGroupVm.Name,
                Status = menuGroupVm.Status,
                DateCreated = menuGroupVm.DateCreated,
                DateModified = menuGroupVm.DateModified,
            };

            _menuGroupRepository.Add(menuGroup);
        }

        public void Update(MenuGroupViewModel menuGroupVm)
        {
            var menuGroup = new MenuGroup()
            {
                Id = menuGroupVm.Id,
                AppRoleId = menuGroupVm.RoleId,
                Name = menuGroupVm.Name,
                Status = menuGroupVm.Status,
                DateCreated = menuGroupVm.DateCreated,
                DateModified = menuGroupVm.DateModified,
            };

            _menuGroupRepository.Update(menuGroup);
        }

        public void Delete(int id) => _menuGroupRepository.Remove(id);

        public void Save() => _unitOfWork.Commit();
    }
}
