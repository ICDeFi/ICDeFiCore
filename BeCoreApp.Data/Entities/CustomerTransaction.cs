using BeCoreApp.Data.Enums;
using BeCoreApp.Data.Interfaces;
using BeCoreApp.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeCoreApp.Data.Entities
{
    [Table("CustomerTransactions")]
    public class CustomerTransaction : DomainEntity<int>
    {
        [Required]
        public string TransactionHas { get; set; }

        [Required]
        public string AddressTo { get; set; }

        [Required]
        public decimal Amount { get; set; }


        [Required]
        public TransactionType Type { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public Guid AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { set; get; }
    }
}
