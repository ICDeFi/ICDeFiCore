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
using System.Text;

namespace BeCoreApp.Application.Implementation
{
    public class MenuItemService : IMenuItemService
    {
        private IMenuItemRepository _menuItemRepository;
        private IUnitOfWork _unitOfWork;

        public MenuItemService(IMenuItemRepository menuItemRepository, IUnitOfWork unitOfWork)
        {
            _menuItemRepository = menuItemRepository;
            _unitOfWork = unitOfWork;
        }

        public List<MenuItemViewModel> GetAll()
        {
            return _menuItemRepository.FindAll()
                .Where(x => x.Status == Status.Active)
                .Select(x => new MenuItemViewModel()
                {
                    Id = x.Id,
                    DateCreated = x.DateCreated,
                    DateModified = x.DateModified,
                    FunctionId = x.FunctionId,
                    FunctionName = x.Function.Name,
                    IconCss = x.IconCss,
                    MenuGroupId = x.MenuGroupId,
                    MenuGroupName = x.MenuGroup.Name,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    SortOrder = x.SortOrder,
                    Status = x.Status,
                    URL = x.URL
                }).ToList();
        }

        public string GetMenuString(int menuGroupId, string userName)
        {
            var menuItems = _menuItemRepository.FindAll(x => x.MenuGroupId == menuGroupId).ToList();

            var builder = new StringBuilder();
            builder.Append("<ul class='side-menu toggle-menu'>");

            builder.Append(GetMenuLiString(menuItems, null));

            builder.Append("<li class=\"slide\">");
            builder.Append("<a href=\"/admin/account/logout\" class=\"side-menu__item\">");
            builder.Append("<span class=\"icon-menu-img\"><i class=\"ion ion-log-out side_menu_img svg-1\" alt=\"image\"></i></span><span class=\"side-menu__label\">Logout</span></a></li>");

            builder.Append("</ul>");
            return builder.ToString();
        }

        private string GetMenuLiString(List<MenuItem> menuList, int? parentId)
        {
            var children = GetChildrenMenu(menuList, parentId);

            if (children.Count <= 0)
                return "";

            var builder = new StringBuilder();

            foreach (var item in children)
            {
                var childStr = GetMenuLiString(menuList, item.Id);
                if (!string.IsNullOrWhiteSpace(childStr))
                {
                    builder.Append("<li class=\"slide\">");
                    builder.Append("<a href=\"#\" class=\"side-menu__item\" data-toggle=\"slide\">");
                    builder.AppendFormat("<span class=\"icon-menu-img\"><i class=\"{0} side_menu_img svg-1\" alt=\"image\"></i></span>", item.IconCss);
                    builder.AppendFormat("<span class=\"side-menu__label\">{0}</span><i class=\"angle fa fa-angle-right\"></i>", item.Name);
                    builder.Append("</a>");
                    builder.Append("<ul class=\"slide-menu\">");
                    builder.Append(childStr);
                    builder.Append("</ul>");
                    builder.Append("</li>");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(item.IconCss))
                    {
                        builder.Append("<li class=\"slide\">");
                        builder.AppendFormat("<a href=\"{0}\" class=\"side-menu__item\">", item.URL);
                        builder.AppendFormat("<span class=\"icon-menu-img\"><i class=\"{0} side_menu_img svg-1\" alt=\"image\"></span><span class=\"side-menu__label\">{1}</span></a></li>", item.IconCss, item.Name);
                    }
                    else
                    {
                        builder.Append("<li>");
                        builder.AppendFormat("<a href=\"{0}\" class=\"slide-item\">", item.URL);
                        builder.AppendFormat("<span class=\"side-menu__label\">{0}</span></a></li>", item.Name);
                    }

                }
            }

            return builder.ToString();
        }

        private List<MenuItem> GetChildrenMenu(List<MenuItem> menuList, int? parentId)
        {
            return menuList.Where(x => x.ParentId == parentId).OrderBy(x => x.SortOrder).ToList();
        }

        public List<MenuItemTreeViewModel> GetTreeAllByMenuGroup(int menuGroupId)
        {
            var listData = _menuItemRepository.FindAll(x => x.MenuGroupId == menuGroupId)
                .Select(x => new MenuItemTreeViewModel()
                {
                    id = x.Id,
                    text = x.Name,
                    icon = "fa fa-folder",
                    state = new MenuItemTreeState { opened = true },
                    data = new MenuItemTreeData
                    {
                        rootId = x.Id,
                        parentId = x.ParentId,
                        sortOrder = x.SortOrder,
                        functionId = x.FunctionId,
                        functionName = x.Function.Name,
                        menuGroupId = x.MenuGroupId,
                        menuGroupName = x.MenuGroup.Name,
                        url = x.URL,
                        status = x.Status,
                        iconCss = x.IconCss,
                    }
                });
            if (listData.Count() == 0)
                return new List<MenuItemTreeViewModel>();

            var groups = listData.GroupBy(i => i.data.parentId);

            var roots = groups.FirstOrDefault(g => g.Key.HasValue == false).ToList();

            if (roots.Count > 0)
            {
                var dict = groups.Where(g => g.Key.HasValue)
                    .ToDictionary(g => g.Key.Value, g => g.ToList());

                for (int i = 0; i < roots.Count; i++)
                    AddChildren(roots[i], dict);
            }

            return roots;
        }

        private void AddChildren(MenuItemTreeViewModel root, IDictionary<int, List<MenuItemTreeViewModel>> source)
        {
            if (source.ContainsKey(root.id))
            {
                root.children = source[root.id].OrderBy(x => x.data.sortOrder).ToList();
                for (int i = 0; i < root.children.Count; i++)
                    AddChildren(root.children[i], source);
            }
            else
            {
                root.icon = "fa fa-file m--font-warning";
                root.children = new List<MenuItemTreeViewModel>();
            }
            //come dfsdfsdfsdf
        }

        public void UpdateParentId(int id, int? newParentId)
        {
            var menuItem = _menuItemRepository.FindById(id);
            menuItem.SortOrder = 1;
            menuItem.ParentId = newParentId;

            var functiondbs = _menuItemRepository.FindAll(x => x.ParentId == newParentId);
            if (functiondbs.Count() > 0)
                menuItem.SortOrder += functiondbs.Max(x => x.SortOrder);

            _menuItemRepository.Update(menuItem);
        }

        public MenuItemViewModel GetById(int id)
        {
            var menuItem = _menuItemRepository.FindById(id);
            if (menuItem == null)
                return null;

            return new MenuItemViewModel()
            {
                Id = menuItem.Id,
                DateCreated = menuItem.DateCreated,
                DateModified = menuItem.DateModified,
                IconCss = menuItem.IconCss,
                MenuGroupId = menuItem.MenuGroupId,
                MenuGroupName = menuItem.MenuGroup.Name,
                FunctionId = menuItem.FunctionId,
                FunctionName = menuItem.Function.Name,
                Name = menuItem.Name,
                ParentId = menuItem.ParentId,
                SortOrder = menuItem.SortOrder,
                Status = menuItem.Status,
                URL = menuItem.URL
            };
        }

        public void Add(MenuItemViewModel menuItemVm)
        {
            if (menuItemVm.ParentId == 0)
                menuItemVm.ParentId = null;

            var menuItemdbs = _menuItemRepository.FindAll(x => x.ParentId == menuItemVm.ParentId &&
            x.MenuGroupId == menuItemVm.MenuGroupId);

            if (menuItemdbs.Count() > 0)
                menuItemVm.SortOrder += menuItemdbs.Max(x => x.SortOrder);

            var menuItem = new MenuItem()
            {
                Id = menuItemVm.Id,
                DateCreated = menuItemVm.DateCreated,
                DateModified = menuItemVm.DateModified,
                IconCss = menuItemVm.IconCss,
                MenuGroupId = menuItemVm.MenuGroupId,
                FunctionId = menuItemVm.FunctionId,
                Name = menuItemVm.Name,
                ParentId = menuItemVm.ParentId,
                SortOrder = menuItemVm.SortOrder,
                Status = menuItemVm.Status,
                URL = menuItemVm.URL
            };

            _menuItemRepository.Add(menuItem);
        }

        public void Update(MenuItemViewModel menuItemVm)
        {
            var menuItem = new MenuItem()
            {
                Id = menuItemVm.Id,
                DateCreated = menuItemVm.DateCreated,
                DateModified = menuItemVm.DateModified,
                IconCss = menuItemVm.IconCss,
                MenuGroupId = menuItemVm.MenuGroupId,
                FunctionId = menuItemVm.FunctionId,
                Name = menuItemVm.Name,
                ParentId = menuItemVm.ParentId,
                SortOrder = menuItemVm.SortOrder,
                Status = menuItemVm.Status,
                URL = menuItemVm.URL
            };
            _menuItemRepository.Update(menuItem);
        }

        public void Delete(int id)
        {
            var menuItemChildrens = _menuItemRepository.FindAll(x => x.ParentId == id).ToList();
            for (int i = 0; i < menuItemChildrens.Count(); i++)
            {
                menuItemChildrens[i].ParentId = null;
                _menuItemRepository.Update(menuItemChildrens[i]);
            }

            _menuItemRepository.Remove(id);
        }

        public void ReOrder(int sourceId, int targetId)
        {
            var source = _menuItemRepository.FindById(sourceId);
            var target = _menuItemRepository.FindById(targetId);
            int tempOrder = source.SortOrder;

            source.SortOrder = target.SortOrder;
            target.SortOrder = tempOrder;

            _menuItemRepository.Update(source);
            _menuItemRepository.Update(target);
        }

        public void Save() =>
            _unitOfWork.Commit();
    }
}