using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Application.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeCoreApp.Models
{
    public class FooterViewModel
    {
        public FooterViewModel()
        {
            FooterBlogCategorys = new List<BlogCategoryViewModel>();
        }
        public List<BlogCategoryViewModel> FooterBlogCategorys { get; set; }
    }
}
