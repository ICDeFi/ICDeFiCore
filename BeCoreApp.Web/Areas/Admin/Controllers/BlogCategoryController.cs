using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Application.ViewModels.Location;
using BeCoreApp.Data.Enums;
using BeCoreApp.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeCoreApp.Areas.Admin.Controllers
{
    public class BlogCategoryController : BaseController
    {
        public IBlogCategoryService _blogCategoryService;
        public IFunctionService _functionService;

        public BlogCategoryController(IBlogCategoryService blogCategoryService,
            IFunctionService functionService)
        {
            _blogCategoryService = blogCategoryService;
            _functionService = functionService;
        }

        public IActionResult Index()
        {
            List<EnumModel> enums = ((MenuFrontEndType[])Enum.GetValues(typeof(MenuFrontEndType)))
                .Select(c => new EnumModel()
                {
                    Value = (int)c,
                    Name = c.GetDescription()
                }).ToList();

            ViewBag.Type = new SelectList(enums, "Value", "Name");

            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _blogCategoryService.GetTreeAll();
            return new ObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllFunction()
         => new ObjectResult(_functionService.GetTreeAll(Status.Active));

        [HttpPost]
        public IActionResult UpdateParentId(int id, int? parentId)
        {
            _blogCategoryService.UpdateParentId(id, parentId);
            _blogCategoryService.Save();
            return new ObjectResult(true);
        }


        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _blogCategoryService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(BlogCategoryViewModel blogCategoryVm)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState.Values.SelectMany(v => v.Errors));

            if (blogCategoryVm.Id == 0)
                _blogCategoryService.Add(blogCategoryVm);
            else
            {
                _blogCategoryService.Update(blogCategoryVm);
            }

            return new OkObjectResult(blogCategoryVm);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            _blogCategoryService.Delete(id);
            _blogCategoryService.Save();

            return new OkObjectResult(id);
        }
    }
}