using BeCoreApp.Data.Enums;
using BeCoreApp.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeCoreApp.Data.Entities
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>, IDateTracking, ISwitchable
    {
        public bool IsSystem { get; set; } = false;
        public string FullName { get; set; }
        public DateTime? BirthDay { set; get; }
        public string Avatar { get; set; }
        public Guid? ReferralId { get; set; }

        public string TRXPrivateKey { get; set; }
        public string TRXPublishKey { get; set; }
        public string TRXAddressBase58 { get; set; }
        public string TRXAddressHex { get; set; }

        public decimal DOLPBalance { get; set; }

        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string ByCreated { get; set; }
        public string ByModified { get; set; }

        public virtual ICollection<CustomerTransaction> CustomerTransactions { set; get; }
        public virtual ICollection<Exchange> Exchanges { set; get; }
        public virtual ICollection<Support> Supports { set; get; }
        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }
    }
}