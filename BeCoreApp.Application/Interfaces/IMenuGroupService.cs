using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Utilities.Dtos;
using System;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IMenuGroupService
    {
        PagedResult<MenuGroupViewModel> GetAllPaging(string startDate, string endDate, string keyword,string roleId, int pageIndex, int pageSize);

        List<MenuGroupViewModel> GetAll();

        MenuGroupViewModel GetByRoleId(string roleId);

        MenuGroupViewModel GetById(int id);

        void Add(MenuGroupViewModel menuGroupVm);

        void Update(MenuGroupViewModel menuGroupVm);

        void Delete(int id);

        void Save();
    }
}
