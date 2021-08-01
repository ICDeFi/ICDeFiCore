using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IMenuItemService
    {
        List<MenuItemViewModel> GetAll();
        string GetMenuString(int menuGroupId, string UserName);
        List<MenuItemTreeViewModel> GetTreeAllByMenuGroup(int menuGroupId);
        MenuItemViewModel GetById(int id);
        void Add(MenuItemViewModel menuItemVm);
        void Update(MenuItemViewModel menuItemVm);
        void Delete(int id);
        void ReOrder(int sourceId, int targetId);
        void UpdateParentId(int id, int? parentId);
        void Save();
    }
}
