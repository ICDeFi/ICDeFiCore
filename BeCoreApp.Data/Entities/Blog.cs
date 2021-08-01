using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.Interfaces;
using BeCoreApp.Infrastructure.SharedKernel;

namespace BeCoreApp.Data.Entities
{
    [Table("Blogs")]
    public class Blog : DomainEntity<int>, ISwitchable, IDateTracking, IHasSeoMetaData
    {
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
        [MaxLength(256)]
        public string Image { set; get; }
        [MaxLength(500)]
        public string Description { set; get; }
        public string Video { get; set; }
        public string Comment { get; set; }
        public string Share { get; set; }
        public string Like { get; set; }
        public string ReferralLink { set; get; }
        public string ReferralLinkRule { set; get; }
        public string MildContent { set; get; }
        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public Status Status { set; get; }

        [MaxLength(256)]
        public string SeoPageTitle { set; get; }

        [MaxLength(256)]
        public string SeoAlias { set; get; }

        [MaxLength(256)]
        public string SeoKeywords { set; get; }

        [MaxLength(256)]
        public string SeoDescription { set; get; }

        public int BlogCategoryId { get; set; }

        [ForeignKey("BlogCategoryId")]
        public virtual BlogCategory BlogCategory { set; get; }

        public virtual ICollection<BlogTag> BlogTags { set; get; }
    }
}
