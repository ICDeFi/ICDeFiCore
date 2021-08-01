using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeCoreApp.Data.Entities
{
    [Table("AppRoles")]
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole() : base()
        {
            MenuGroups = new List<MenuGroup>();
            Permissions = new List<Permission>();
        }

        [StringLength(250)]
        public string Description { get; set; }
        public virtual ICollection<MenuGroup> MenuGroups { set; get; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
