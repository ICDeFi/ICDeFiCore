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
using BeCoreApp.Utilities.Dtos;
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
    public class NetworkController : BaseController
    {
        private readonly IUserService _userService;
        public NetworkController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.GetSpecificClaim("UserId");
            var model = await _userService.GetNetworkInfo(userId);
            return View(model);
        }

        [HttpGet]
        public IActionResult GetAllPaging(int refIndex, string keyword, int page, int pageSize)
        {
            var userId = User.GetSpecificClaim("UserId");
            var model = _userService.GetCustomerReferralPagingAsync(userId, refIndex, keyword, page, pageSize);
            return new OkObjectResult(model);
        }
    }
}