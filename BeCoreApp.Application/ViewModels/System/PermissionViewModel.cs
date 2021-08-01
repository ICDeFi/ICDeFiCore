using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Application.ViewModels.System
{
    public class PermissionViewModel
    {
        public int Id { get; set; }

        public Guid RoleId { get; set; }

        public string FunctionId { get; set; }

        public string Feature { get; set; }

        public AppRoleViewModel AppRole { get; set; }

        public FunctionViewModel Function { get; set; }
    }
}
