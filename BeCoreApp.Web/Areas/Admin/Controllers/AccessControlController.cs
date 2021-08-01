using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Authorization;
using BeCoreApp.Data.Entities;
using BeCoreApp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BeCoreApp.Areas.Admin.Controllers
{
    public class AccessControlController : BaseController
    {
        private readonly IFunctionService _functionService;
        private readonly IRoleService _roleService;
        private readonly IAuthorizationService _authorizationService;
        public AccessControlController(IRoleService roleService,
            IFunctionService functionService, IAuthorizationService authorizationService)
        {
            _roleService = roleService;
            _functionService = functionService;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "ACCESSCONTROL", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Account/Login");

            AccessControlViewModel model = new AccessControlViewModel();
            model.AppRoles = await _roleService.GetAllAsync();
            model.AccessControlDTOs = _roleService.GetAllAccessControl();
            return View(model);
        }


        [HttpPost]
        public IActionResult SetPermission()
        {

            var functions = _functionService.GetAllFunctionToSetPermission();
            var appRoles = _roleService.GetAllRoleToSetPermission();
            var permissions = _roleService.GetAllPermissionToSetPermission();

            foreach (var function in functions)
            {
                foreach (var role in appRoles)
                {
                    string feature = Request.Form["disables_" + role.Id + "-" + function.Id];

                    var currentPermission = permissions
                        .FirstOrDefault(p => p.FunctionId == function.Id && p.RoleId == role.Id);

                    if (!string.IsNullOrWhiteSpace(feature))
                    {
                        if (currentPermission != null)
                        {
                            currentPermission.Feature = feature;
                            _roleService.UpdatePermission(currentPermission);
                        }
                        else
                        {
                            var newPermission = new Permission();
                            newPermission.Feature = feature;
                            newPermission.RoleId = role.Id;
                            newPermission.FunctionId = function.Id;
                            _roleService.AddPermission(newPermission);
                        }
                    }
                    else
                    {
                        if (currentPermission != null)
                        {
                            _roleService.DeletePermission(currentPermission.Id);
                        }
                    }
                }
            }


            return RedirectToAction("Index", "Admin/AccessControl");
        }
    }
}