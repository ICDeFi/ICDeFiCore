using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Application.ViewModels.Enterprise;
using BeCoreApp.Application.ViewModels.Product;
using BeCoreApp.Application.ViewModels.Project;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Enums;
using BeCoreApp.Utilities.Constants;
using BeCoreApp.Utilities.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace BeCoreApp.Areas.Admin.Controllers
{
    public class NotifyController : BaseController
    {
        private readonly INotifyService _notifyService;
        public NotifyController(INotifyService notifyService)
        {
            _notifyService = notifyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region AJAX API
        [HttpGet]
        public IActionResult GetFirst()
        {
            var model = _notifyService.GetFirst();
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(NotifyViewModel notifyVm)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState.Values.SelectMany(v => v.Errors));
            else
            {
                if (notifyVm.Id == 0)
                    _notifyService.Add(notifyVm);
                else
                    _notifyService.Update(notifyVm);

                _notifyService.Save();

                return new OkObjectResult(notifyVm);
            }
        }
        #endregion
    }
}