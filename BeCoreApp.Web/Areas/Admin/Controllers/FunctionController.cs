using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Authorization;
using BeCoreApp.Data.Enums;
using BeCoreApp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BeCoreApp.Areas.Admin.Controllers
{
    public class FunctionController : BaseController
    {
        #region Initialize
        private readonly IFunctionService _functionService;
        private readonly IAuthorizationService _authorizationService;
        public FunctionController(IFunctionService functionService, 
            IAuthorizationService authorizationService)
        {
            _functionService = functionService;
            _authorizationService = authorizationService;
        }
        #endregion Initialize

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "FUNCTION", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Account/Login");

            return View();
        }

        [HttpPost]
        public IActionResult SaveEntity(FunctionViewModel functionVm)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState.Values.SelectMany(v => v.Errors));
            else
            {

                if (functionVm.Action == 0)
                    _functionService.Add(functionVm);
                else
                    _functionService.Update(functionVm);

                _functionService.Save();

                return new OkObjectResult(functionVm);
            }
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (!ModelState.IsValid)
                return new BadRequestResult();
            else
            {
                _functionService.Delete(id);
                _functionService.Save();

                return new OkObjectResult(1);
            }
        }

        [HttpPost]
        public IActionResult CheckIsExistId(string id)
        {
            var data = _functionService.GetById(id);
            if (data != null)
                return new OkObjectResult(true);

            return new OkObjectResult(false);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _functionService.GetTreeAll(Status.NoneActive);
            return new ObjectResult(model);
        }

        [HttpPost]
        public IActionResult UpdateParentId(string id, string parentId)
        {
            _functionService.UpdateParentId(id, parentId);
            _functionService.Save();
            return new ObjectResult(true);
        }

        [HttpPost]
        public IActionResult ReOrder(string sourceId, string targetId)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);
            else
            {
                if (sourceId == targetId)
                    return new BadRequestResult();
                else
                {
                    _functionService.ReOrder(sourceId, targetId);
                    _functionService.Save();
                    return new OkObjectResult(sourceId);
                }
            }
        }
    }
}