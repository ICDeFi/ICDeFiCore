using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Application.ViewModels.Location;
using BeCoreApp.Authorization;
using BeCoreApp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeCoreApp.Areas.Admin.Controllers
{
    public class MenuItemController : BaseController
    {
        public readonly IMenuItemService _menuItemService;
        public readonly IMenuGroupService _menuGroupService;
        public readonly IFunctionService _functionService;
        private readonly IAuthorizationService _authorizationService;
        public MenuItemController(IMenuItemService menuItemService,
            IMenuGroupService menuGroupService,
            IFunctionService functionService, IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _menuItemService = menuItemService;
            _menuGroupService = menuGroupService;
            _functionService = functionService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var result = await _authorizationService.AuthorizeAsync(User, "MENUITEM", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Account/Login");

            ViewBag.MenuGroupId = id;
            return View();
        }

        [HttpGet]
        public IActionResult GetAll(int menuGroupId)
        {
            var model = _menuItemService.GetTreeAllByMenuGroup(menuGroupId);
            return new ObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllFunction()
        {
            var model = _functionService.GetTreeAll();
            return new ObjectResult(model);
        }

        [HttpPost]
        public IActionResult UpdateParentId(int id, int? parentId)
        {
            var roleName = User.GetSpecificClaim("RoleName");
            if (roleName.ToLower() != "admin")
                return RedirectToAction("Index", "Home");

            _menuItemService.UpdateParentId(id, parentId);
            _menuItemService.Save();
            return new ObjectResult(true);
        }

        [HttpPost]
        public IActionResult ReOrder(int sourceId, int targetId)
        {
            var roleName = User.GetSpecificClaim("RoleName");
            if (roleName.ToLower() != "admin")
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);
            else
            {
                if (sourceId == targetId)
                    return new BadRequestResult();
                else
                {
                    _menuItemService.ReOrder(sourceId, targetId);
                    _menuItemService.Save();
                    return new OkObjectResult(sourceId);
                }
            }
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _menuItemService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(MenuItemViewModel menuItemVm)
        {
            var roleName = User.GetSpecificClaim("RoleName");
            if (roleName.ToLower() != "admin")
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState.Values.SelectMany(v => v.Errors));

            if (menuItemVm.Id == 0)
                _menuItemService.Add(menuItemVm);
            else
                _menuItemService.Update(menuItemVm);

            _menuItemService.Save();
            return new OkObjectResult(menuItemVm);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var roleName = User.GetSpecificClaim("RoleName");
            if (roleName.ToLower() != "admin")
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            _menuItemService.Delete(id);
            _menuItemService.Save();

            return new OkObjectResult(id);
        }
    }
}