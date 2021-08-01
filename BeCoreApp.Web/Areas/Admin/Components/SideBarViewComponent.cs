using BeCoreApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeCoreApp.Extensions;
using System.Security.Claims;
using BeCoreApp.Utilities.Constants;
using BeCoreApp.Application.ViewModels.System;

namespace BeCoreApp.Areas.Admin.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        IMenuGroupService _menuGroupService;
        IMenuItemService _menuItemService;
        public SideBarViewComponent(
            IMenuGroupService menuGroupService,
            IMenuItemService menuItemService)
        {
            _menuGroupService = menuGroupService;
            _menuItemService = menuItemService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roleId = ((ClaimsPrincipal)User).GetSpecificClaim("RoleId");
            var menuGroup = _menuGroupService.GetByRoleId(roleId);
            if (menuGroup == null)
                return View(new List<string>());

            var userName = ((ClaimsPrincipal)User).GetSpecificClaim("UserName");

            var menuContent = _menuItemService.GetMenuString(menuGroup.Id, userName);

            return View(new List<string> { new string(menuContent) });
        }
    }
}
