using BeCoreApp.Data.Enums;
using BeCoreApp.Data.Interfaces;
using BeCoreApp.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeCoreApp.Data.Entities
{
    [Table("BlogCategories")]
    public class BlogCategory : DomainEntity<int>, ISwitchable, IDateTracking, IHasSeoMetaData
    {
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
        public string Description { set; get; }
        [Required]
        [StringLength(128)]
        public string FunctionId { get; set; }
        [Required]
        [StringLength(500)]
        public string URL { set; get; }
        public int? ParentId { set; get; }
        public string IconCss { get; set; }
        public int SortOrder { set; get; }
        public string SeoPageTitle { get; set; }
        public string SeoAlias { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public Status Status { get; set; }
        public Status IsMain { get; set; }
        public MenuFrontEndType Type { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [ForeignKey("FunctionId")]
        public virtual Function Function { set; get; }
        public virtual ICollection<Blog> Blogs { set; get; }
    }
}
