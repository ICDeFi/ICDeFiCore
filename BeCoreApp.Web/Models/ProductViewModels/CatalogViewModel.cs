using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Application.ViewModels.Product;
using BeCoreApp.Utilities.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeCoreApp.Models.ProductViewModels
{
    public class CatalogViewModel
    {
        public CatalogViewModel()
        {
            Products = new List<ProductViewModel>();
            Tags = new List<TagViewModel>();
        }
        public PagedResult<ProductViewModel> Data { get; set; }
        public List<ProductViewModel> Products { set; get; }
        public TagViewModel Tag { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public BlogCategoryViewModel BlogCategory { get; set; }

        public string SortType { set; get; }

        public int? PageSize { set; get; }

        public List<SelectListItem> SortTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem(){Value = "1",Text = "Thông Thường"},
            new SelectListItem(){Value = "2",Text = "Tin Mới Nhất"}
        };

        public List<SelectListItem> PageSizes { get; } = new List<SelectListItem>
        {
            new SelectListItem(){Value = "0",Text = "ALL"},
            new SelectListItem(){Value = "10",Text = "10"},
            new SelectListItem(){Value = "20",Text = "20"},
            new SelectListItem(){Value = "40",Text = "40"},
        };
    }
}
