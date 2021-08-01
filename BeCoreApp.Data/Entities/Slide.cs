using BeCoreApp.Data.Enums;
using BeCoreApp.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeCoreApp.Data.Entities
{
    [Table("Slides")]
    public class Slide : DomainEntity<int>
    {
        [Required]
        [StringLength(250)]
        public string Name { set; get; }

        [StringLength(500)]
        public string Description { set; get; }

        [Required]
        [StringLength(500)]
        public string Url { set; get; }

        [Required]
        [StringLength(250)]
        public string Image { set; get; }

        public bool? HotFlag { set; get; }

        public DateTime DateCreated { set; get; }

        public DateTime DateModified { set; get; }

        public Status Status { set; get; }
    }
}
