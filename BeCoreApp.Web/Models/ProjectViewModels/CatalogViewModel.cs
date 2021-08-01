using BeCoreApp.Application.ViewModels.Project;
using BeCoreApp.Utilities.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeCoreApp.Models.ProjectViewModels
{
    public class CatalogViewModel
    {
        public CatalogViewModel()
        {
            ProjectVMs = new List<ProjectViewModel>();
            ProjectCategoryVMs = new List<ProjectCategoryViewModel>();
        }
        public PagedResult<ProjectViewModel> Data { get; set; }
        public List<ProjectViewModel> ProjectVMs { set; get; }
        public ProjectCategoryViewModel ProjectCategoryVM { get; set; }
        public List<ProjectCategoryViewModel> ProjectCategoryVMs { set; get; }

        public string SortType { set; get; }
        public int? PageSize { set; get; }

        public List<SelectListItem> SortTypes { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(){Value = "1",Text = "Thông Thường"},
            new SelectListItem(){Value = "2",Text = "Tin Mới Nhất"}
        };

        public List<SelectListItem> PageSizes { get; } = new List<SelectListItem>
        {
            new SelectListItem(){Value = "0",Text = "ALL"},
            new SelectListItem(){Value = "12",Text = "12"},
            new SelectListItem(){Value = "24",Text = "24"},
            new SelectListItem(){Value = "48",Text = "48"},
        };
    }
}
