using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeCoreApp.Models.AccountViewModels
{
    public class ProfileViewModel
    {
        public Guid? Id { set; get; }
        public string ID { get; set; }
        public Guid? ReferralId { get; set; }
        public bool IsSystem { get; set; }
        [Required]
        public string FullName { set; get; }
        public string BirthDay { set; get; }
        public string Email { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        [Required]
        public string PhoneNumber { set; get; }
        public string Avatar { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PrivateKey { get; set; }
        public string PublishKey { get; set; }

        public decimal? WalletStart { get; set; }
        public decimal? WalletGame { get; set; }
        public decimal? WalletWin { get; set; }
        public decimal? WalletValuesShare { get; set; }
        public decimal? WalletUsdtReferral { get; set; }
        public decimal? WalletUsdt { get; set; }

        public DateTime? ActivatedDate { get; set; }
        public bool? IsActivated { get; set; }

        public string WithdrawPublishKey { get; set; }
        public string CMNDImage { get; set; }
        public string BankBillImage { get; set; }
        public string BankCardImage { get; set; }
        public bool? IsUpdatedKYC { get; set; }
        public DateTime? UpdateKYCDate { get; set; }


        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string ByCreated { get; set; }
        public string ByModified { get; set; }

    }
}
