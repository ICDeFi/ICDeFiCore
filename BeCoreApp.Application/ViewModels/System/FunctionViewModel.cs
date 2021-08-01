using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeCoreApp.Application.ViewModels.System
{
    public class FunctionViewModel
    {
        public FunctionViewModel()
        {
            MenuItems = new List<MenuItemViewModel>();
            Permissions = new List<PermissionViewModel>();
        }
        public string Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { set; get; }

        [Required]
        [StringLength(250)]
        public string URL { set; get; }

        [StringLength(128)]
        public string ParentId { set; get; }

        public string IconCss { get; set; }
        public int SortOrder { set; get; }
        public Status Status { set; get; }
        public Status IsFrontEnd { set; get; }
        public string ActionControl { set; get; }
        public int Action { set; get; }
        public List<MenuItemViewModel> MenuItems { set; get; }
        public List<PermissionViewModel> Permissions { get; set; }
    }
}
