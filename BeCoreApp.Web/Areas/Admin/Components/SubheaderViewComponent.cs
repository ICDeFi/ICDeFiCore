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
    public class SubHeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
