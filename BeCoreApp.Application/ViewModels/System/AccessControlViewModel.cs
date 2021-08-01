using BeCoreApp.Application.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Application.ViewModels.System
{
    public class AccessControlViewModel
    {
        public AccessControlViewModel()
        {
            AppRoles = new List<AppRoleViewModel>();
            AccessControlDTOs = new List<AccessControlDTO>();
        }
        public List<AccessControlDTO> AccessControlDTOs { get; set; }
        public List<AppRoleViewModel> AppRoles { get; set; }
    }

    public class AccessControlDTO
    {
        public AccessControlDTO()
        {
            IsPermissions = new List<bool>();
        }
        public string Id { get; set; }

        public string Name { get; set; }

        public string Action { get; set; }

        public bool Root { get; set; }

        public List<bool> IsPermissions { get; set; }

        public string RequestKey { get; set; }
    }
}
