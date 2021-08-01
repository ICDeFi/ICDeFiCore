using BeCoreApp.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeCoreApp.Data.Entities
{
    [Table("Invests")]
    public class Invest : DomainEntity<int>
    {
        [Required]
        public string TransactionHash { get; set; }

        [Required]
        public string FromAddress { get; set; }

        [Required]
        public string ToAddress { get; set; }

        [Required]
        public decimal EthereumAmount { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public Guid AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { set; get; }
    }
}
