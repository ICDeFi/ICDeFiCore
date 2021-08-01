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
    [Table("EnterpriseFields")]
    public class EnterpriseField : DomainEntity<int>, ISwitchable, IDateTracking
    {
        public EnterpriseField()
        {
        }

        public EnterpriseField(int id, int enterpriseId, int fieldId)
        {
            Id = id;
            EnterpriseId = enterpriseId;
            FieldId = fieldId;
        }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        [Required]
        public int EnterpriseId { get; set; }
        [Required]
        public int FieldId { get; set; }

        [ForeignKey("EnterpriseId")]
        public virtual Enterprise Enterprise { set; get; }
        [ForeignKey("FieldId")]
        public virtual Field Field { set; get; }
    }
}
