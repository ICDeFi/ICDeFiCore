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
    [Table("Feedbacks")]
    public class Feedback : DomainEntity<int>, ISwitchable, IDateTracking
    {
        [StringLength(500)]
        [Required]
        public string Title { set; get; }

        [StringLength(250)]
        public string FullName { set; get; }

        [StringLength(250)]
        public string Email { set; get; }

        [StringLength(250)]
        public string Phone { set; get; }

        [StringLength(1000)]
        public string Message { set; get; }

        public FeedbackType Type { set; get; }

        public Status Status { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public string CreatedBy { set; get; }
        public string ModifiedBy { set; get; }
    }
}
