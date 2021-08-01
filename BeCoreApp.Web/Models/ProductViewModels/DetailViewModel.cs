using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Application.ViewModels.Product;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeCoreApp.Models.ProductViewModels
{
    public class DetailViewModel
    {
        public DetailViewModel()
        {
            ProductTags = new List<TagViewModel>();
            RelatedProducts = new List<ProductViewModel>();
            Tags = new List<TagViewModel>();
        }
        public ProductViewModel Product { get; set; }
        public List<TagViewModel> ProductTags { get; set; }

        public List<ProductViewModel> RelatedProducts { get; set; }

        public List<TagViewModel> Tags { set; get; }
    }
}
