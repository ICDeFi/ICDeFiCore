using BeCoreApp.Data.Enums;
using BeCoreApp.Data.Interfaces;
using BeCoreApp.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeCoreApp.Data.Entities
{
    [Table("Products")]
    public class Product : DomainEntity<int>, ISwitchable, IDateTracking, IHasSeoMetaData
    {
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

        public int BlogCategoryId { get; set; }

        public string ProductTypeId { get; set; }


        [ForeignKey("ProductTypeId")]
        public virtual ProductType ProductType { set; get; }

        [ForeignKey("BlogCategoryId")]
        public virtual BlogCategory BlogCategory { set; get; }

        public virtual ICollection<ProductTag> ProductTags { set; get; }
        public virtual ICollection<ProductImage> ProductImages { set; get; }
        public virtual ICollection<ProductQuantity> ProductQuantities { set; get; }
    }
}
