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
    public class MenuGroupController : BaseController
    {
        public IMenuGroupService _menuGroupService;
        public IRoleService _roleService;
        private readonly IAuthorizationService _authorizationService;
        public MenuGroupController(IMenuGroupService menuGroupService,
            IRoleService roleService, IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _menuGroupService = menuGroupService;
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "MENUGROUP", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Account/Login");

            var roles = await _roleService.GetAllAsync();
            ViewBag.RoleId = new SelectList(roles, "Id", "Name");
            return View();
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _menuGroupService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllPaging(string keyword, string roleId, int page, int pageSize)
        {
            var model = _menuGroupService.GetAllPaging("", "", keyword, roleId, page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(MenuGroupViewModel menuGroupVm)
        {
            var roleName = User.GetSpecificClaim("RoleName");
            if (roleName.ToLower() != "admin")
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }

            if (menuGroupVm.Id == 0)
            {
                var countItem = _menuGroupService.GetAll()
                    .Where(x => x.RoleId == menuGroupVm.RoleId).Count();
                if (countItem > 0)
                    return new BadRequestObjectResult("Vai trò đã tồn tại trong hệ thống!!!");

                _menuGroupService.Add(menuGroupVm);
            }
            else
            {
                var countItem = _menuGroupService.GetAll()
                    .Where(x => x.RoleId == menuGroupVm.RoleId && x.Id != menuGroupVm.Id).Count();
                if (countItem > 0)
                    return new BadRequestObjectResult("Vai trò đã tồn tại trong hệ thống!!!");

                _menuGroupService.Update(menuGroupVm);
            }


            _menuGroupService.Save();
            return new OkObjectResult(menuGroupVm);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var roleName = User.GetSpecificClaim("RoleName");
            if (roleName.ToLower() != "admin")
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            _menuGroupService.Delete(id);
            _menuGroupService.Save();

            return new OkObjectResult(id);
        }
    }
}