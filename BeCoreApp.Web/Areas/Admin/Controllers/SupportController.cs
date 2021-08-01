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
using BeCoreApp.Data.Enums;
using BeCoreApp.Extensions;
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
    public class SupportController : BaseController
    {
        private readonly ISupportService _SupportService;
        public SupportController(ISupportService SupportService)
        {
            _SupportService = SupportService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            string appUserId = User.GetSpecificClaim("UserId");
            var model = _SupportService.GetAllPaging(keyword, appUserId, page, pageSize);
            return new OkObjectResult(model);
        }

        #region AJAX API


        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _SupportService.GetById(id);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(SupportViewModel supportVm)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState.Values.SelectMany(v => v.Errors));
            else
            {
                string appUserId = User.GetSpecificClaim("UserId");
                supportVm.AppUserId = new Guid(appUserId);
                _SupportService.Add(supportVm);
                _SupportService.Save();

                return new OkObjectResult(supportVm);
            }
        }
        #endregion
    }
}