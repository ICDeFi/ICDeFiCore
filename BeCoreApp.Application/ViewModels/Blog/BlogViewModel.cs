using BeCoreApp.Application.ViewModels.Valuesshare;
using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeCoreApp.Application.ViewModels.Blog
{
    public class BlogViewModel
    {
        public BlogViewModel()
        {
            BlogTags = new List<BlogTagViewModel>();
        }
        public int Id { set; get; }
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
        public string MildContent { set; get; }
        public string ReferralLink { set; get; }
        public string ReferralLinkRule { set; get; }
        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }
        public List<string> Tags { get; set; }
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
        public string BlogCategoryName { get; set; }

        public BlogCategoryViewModel BlogCategory { set; get; }
        public List<BlogTagViewModel> BlogTags { set; get; }

    }
}