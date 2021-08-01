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
    [Table("OperatingEnvironments")]
    public class OperatingEnvironment : DomainEntity<int>, ISwitchable, IDateTracking
    {
        public OperatingEnvironment()
        {
            EnduringStrips = new List<EnduringStrip>();
        }

        public OperatingEnvironment(int id, string name,
            Status status, string seoPageTitle,
               string seoAlias, string seoMetaKeyword,
               string seoMetaDescription)
        {
            Id = id;
            Name = name;
            Status = status;
            EnduringStrips = new List<EnduringStrip>();
        }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public virtual ICollection<EnduringStrip> EnduringStrips { set; get; }
    }
}
