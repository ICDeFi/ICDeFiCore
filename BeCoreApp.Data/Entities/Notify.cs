using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.Interfaces;
using BeCoreApp.Infrastructure.SharedKernel;

namespace BeCoreApp.Data.Entities
{
    [Table("Notifies")]
    public class Notify : DomainEntity<int>, ISwitchable, IDateTracking
    {
        [Required]
        public string Name { set; get; }
        public string MildContent { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public Status Status { set; get; }
    }
}
