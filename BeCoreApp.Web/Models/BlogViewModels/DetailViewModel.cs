using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Application.ViewModels.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeCoreApp.Models.BlogViewModels
{
    public class DetailViewModel
    {
        public DetailViewModel()
        {
            Blogs = new List<BlogViewModel>();
            Tags = new List<TagViewModel>();
            BlogTags = new List<TagViewModel>();
            SideBarBlogCategorys = new List<BlogCategoryViewModel>();
        }
        public List<TagViewModel> BlogTags { get; set; }
        public BlogViewModel Blog { get; set; }
        public List<BlogViewModel> Blogs { set; get; }
        public List<BlogViewModel> BlogRelates { set; get; }
        public List<TagViewModel> Tags { get; set; }
        public BlogCategoryViewModel BlogCategory { get; set; }
        public List<BlogCategoryViewModel> SideBarBlogCategorys { get; set; }
    }
}
