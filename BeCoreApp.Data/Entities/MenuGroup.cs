using BeCoreApp.Data.Enums;
using BeCoreApp.Data.Interfaces;
using BeCoreApp.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeCoreApp.Data.Entities
{
    [Table("MenuGroups")]
    public class MenuGroup : DomainEntity<int>, ISwitchable, IDateTracking
    {
        public MenuGroup()
        {
            MenuItems = new List<MenuItem>();
        }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        [Required]
        public Guid AppRoleId { get; set; }

        [ForeignKey("AppRoleId")]
        public virtual AppRole AppRole { set; get; }
        public virtual ICollection<MenuItem> MenuItems { set; get; }
    }
}
