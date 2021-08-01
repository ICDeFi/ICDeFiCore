using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.Interfaces;
using BeCoreApp.Infrastructure.SharedKernel;

namespace BeCoreApp.Data.Entities
{
    [Table("TransactionHistories")]
    public class TransactionHistory : DomainEntity<int>
    {
        public Guid AppUserId { set; get; }
        public string Image { get; set; }
        public decimal Amount { get; set; }

        public string TransactionHash { set; get; }

        public string Note { get; set; }

        public TransactionHistoryType Type { set; get; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { set; get; }
    }
}
