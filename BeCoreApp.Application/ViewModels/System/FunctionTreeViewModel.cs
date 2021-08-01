using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Application.ViewModels.System
{
    public class FunctionTreeViewModel
    {
        public FunctionTreeViewModel()
        {
            children = new List<FunctionTreeViewModel>();
        }

        public string id { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public FunctionTreeData data { get; set; }
        public FunctionTreeState state { get; set; }

        public List<FunctionTreeViewModel> children { get; set; }
    }
    public class FunctionTreeState
    {
        public bool opened { get; set; } = true;
    }
    public class FunctionTreeData
    {
        public string rootId { get; set; }
        public string url { get; set; }
        public string actionControl { get; set; }
        public string parentId { get; set; }
        public string iconCss { get; set; }
        public int sortOrder { set; get; }
        public Status status { get; set; }
        public Status isFrontEnd { set; get; }
    }
}
