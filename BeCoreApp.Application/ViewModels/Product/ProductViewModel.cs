using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Data.Enums;

namespace BeCoreApp.Application.ViewModels.Product
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            ProductTags = new List<ProductTagViewModel>();
            ProductImages = new List<ProductImageViewModel>();
            ProductQuantities = new List<ProductQuantityViewModel>();
        }

        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal OriginalPrice { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public string MildContent { get; set; }

        public bool? HomeFlag { get; set; }

        public bool? HotFlag { get; set; }

        public int? ViewCount { get; set; }

        public List<string> Tags { get; set; }

        public string SeoPageTitle { set; get; }

        [StringLength(255)]
        public string SeoAlias { set; get; }

        [StringLength(255)]
        public string SeoKeywords { set; get; }

        [StringLength(255)]
        public string SeoDescription { set; get; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }

        public Status Status { set; get; }
        public string BlogCategoryName { get; set; }
        public int BlogCategoryId { get; set; }
        public string ProductTypeId { get; set; }

        public ProductTypeViewModel ProductType { set; get; }
        public BlogCategoryViewModel BlogCategory { set; get; }
        public List<ProductTagViewModel> ProductTags { set; get; }
        public List<ProductImageViewModel> ProductImages { set; get; }
        public List<ProductQuantityViewModel> ProductQuantities { set; get; }
    }
}
