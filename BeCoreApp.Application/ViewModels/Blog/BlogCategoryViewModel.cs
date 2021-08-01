using BeCoreApp.Application.ViewModels.Product;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BeCoreApp.Application.ViewModels.Blog
{
    public class BlogCategoryViewModel
    {
        public BlogCategoryViewModel()
        {
            Blogs = new List<BlogViewModel>();
            Products = new List<ProductViewModel>();
            BlogCategoryChild = new List<BlogCategoryViewModel>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
        public string Description { get; set; }
        [Required]
        [StringLength(128)]
        public string FunctionId { get; set; }
        public string FunctionName { get; set; }
        [Required]
        [StringLength(500)]
        public string URL { set; get; }
        public int? ParentId { set; get; }
        public string IconCss { get; set; }
        public int SortOrder { set; get; }
        [MaxLength(256)]
        public string SeoPageTitle { set; get; }
        [MaxLength(256)]
        public string SeoAlias { set; get; }
        [MaxLength(256)]
        public string SeoKeywords { set; get; }
        [MaxLength(256)]
        public string SeoDescription { set; get; }
        public Status Status { get; set; }
        public Status IsMain { get; set; }
        public MenuFrontEndType Type { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int CountBlog { get; set; }

        public FunctionViewModel Function { set; get; }

        public List<BlogViewModel> Blogs { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public List<BlogCategoryViewModel> BlogCategoryChild { get; set; }
    }
}
