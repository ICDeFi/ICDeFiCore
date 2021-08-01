using BeCoreApp.Application.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Application.ViewModels.System
{
    public class AppRoleViewModel
    {
        public AppRoleViewModel()
        {
            MenuGroups = new List<MenuGroupViewModel>();
            Permissions = new List<PermissionViewModel>();
        }

        public Guid? Id { set; get; }

        public string Name { set; get; }

        public string Description { set; get; }

        public List<MenuGroupViewModel> MenuGroups { get; set; }
        public List<PermissionViewModel> Permissions { get; set; }
    }
}
