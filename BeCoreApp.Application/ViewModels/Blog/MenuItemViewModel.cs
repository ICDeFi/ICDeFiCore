using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Enums;

namespace BeCoreApp.Application.ViewModels.Blog
{
    public class MenuItemViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [Required]
        [StringLength(250)]
        public string URL { set; get; }

        [Required]
        [StringLength(128)]
        public string FunctionId { get; set; }
        public string FunctionName { get; set; }

        [Required]
        public int MenuGroupId { get; set; }
        public string MenuGroupName { get; set; }
        public int? ParentId { set; get; }
        public string IconCss { get; set; }
        public int SortOrder { set; get; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public MenuGroupViewModel MenuGroup { set; get; }
        public FunctionViewModel Function { set; get; }
    }
}
