using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Application.ViewModels.Product
{
    public class TreeViewModel
    {
        public TreeViewModel()
        {
            children = new List<TreeViewModel>();
        }

        public int id { get; set; }
        public string text { get; set; }
        public int? parentId { get; set; }
        public int sortOrder { set; get; }

        public List<TreeViewModel> children { get; set; }
    }
}
