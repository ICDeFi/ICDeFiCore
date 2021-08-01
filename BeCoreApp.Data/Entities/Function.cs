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
    [Table("Functions")]
    public class Function : DomainEntity<string>, ISwitchable, ISortable
    {
        public Function()
        {
            MenuItems = new List<MenuItem>();
            Permissions = new List<Permission>();
        }

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

        [StringLength(500)]
        public string ActionControl { get; set; }

        public virtual ICollection<MenuItem> MenuItems { set; get; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
