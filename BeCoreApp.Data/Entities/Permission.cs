using BeCoreApp.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeCoreApp.Data.Entities
{
    [Table("Permissions")]
    public class Permission : DomainEntity<int>
    {
        [Required]
        public Guid RoleId { get; set; }

        [StringLength(128)]
        [Required]
        public string FunctionId { get; set; }

        [StringLength(1000)]
        public string Feature { set; get; }


        [ForeignKey("RoleId")]
        public virtual AppRole AppRole { get; set; }

        [ForeignKey("FunctionId")]
        public virtual Function Function { get; set; }
    }
}
