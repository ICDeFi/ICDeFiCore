using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Application.ViewModels.Blog
{
    public class BlogCategoryTreeViewModel
    {
        public BlogCategoryTreeViewModel()
        {
            children = new List<BlogCategoryTreeViewModel>();
        }

        public int id { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public BlogCategoryTreeData data { get; set; }
        public BlogCategoryTreeState state { get; set; }

        public List<BlogCategoryTreeViewModel> children { get; set; }
    }
    public class BlogCategoryTreeState
    {
        public bool opened { get; set; } = true;
    }
    public class BlogCategoryTreeData
    {
        public int rootId { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public int? parentId { get; set; }
        public string functionId { get; set; }
        public string functionName { get; set; }
        public string iconCss { get; set; }
        public string seoAlias { get; set; }
        public string seoPageTitle { get; set; }
        public string seoKeywords { get; set; }
        public string seoDescription { get; set; }
        public int sortOrder { set; get; }
        public Status isMain { get; set; }
        public Status status { get; set; }
        public MenuFrontEndType type { get; set; }
        public string typeName { get; set; }
    }
}
