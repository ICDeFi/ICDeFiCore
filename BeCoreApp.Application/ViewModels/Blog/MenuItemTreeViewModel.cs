using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Application.ViewModels.Blog
{
    public class MenuItemTreeViewModel
    {
        public MenuItemTreeViewModel()
        {
            children = new List<MenuItemTreeViewModel>();
        }

        public int id { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public MenuItemTreeData data { get; set; }
        public MenuItemTreeState state { get; set; }

        public List<MenuItemTreeViewModel> children { get; set; }
    }
    public class MenuItemTreeState
    {
        public bool opened { get; set; } = true;
    }
    public class MenuItemTreeData
    {
        public int rootId { get; set; }
        public string url { get; set; }
        public int? parentId { get; set; }
        public string functionName { get; set; }
        public string functionId { get; set; }
        public int menuGroupId { get; set; }
        public string menuGroupName { get; set; }
        public string iconCss { get; set; }
        public int sortOrder { set; get; }
        public Status status { get; set; }
    }
}
