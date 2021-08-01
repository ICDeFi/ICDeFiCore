using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.Interfaces;
using BeCoreApp.Infrastructure.SharedKernel;

namespace BeCoreApp.Data.Entities
{
    [Table("Exchanges")]
    public class Exchange : DomainEntity<int>
    {
        [Required]
        public Guid AppUserId { set; get; }
        [Required]
        public ExchangeType WalletFrom { set; get; }
        [Required]
        public ExchangeType WalletTo { set; get; }
        [Required]
        public DateTime ExchangeDate { get; set; }
        [Required]
        public decimal Amount { get; set; }
        

        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { set; get; }
    }
}
