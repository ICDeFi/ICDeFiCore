using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Application.ViewModels.Valuesshare;
using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;

namespace BeCoreApp.Application.ViewModels.System
{
    public class AppUserViewModel
    {
        public AppUserViewModel()
        {
            Roles = new List<string>();
            Transactions = new List<TransactionViewModel>();
            Exchanges = new List<ExchangeViewModel>();
            Supports = new List<SupportViewModel>();
        }

        public Guid? Id { set; get; }
        public Guid? ReferralId { get; set; }
        public string Referal { get; set; }
        public bool IsSystem { get; set; }
        public string FullName { set; get; }
        public string BirthDay { set; get; }
        public string Email { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string PhoneNumber { set; get; }
        public string Avatar { get; set; }
        public bool EmailConfirmed { get; set; }

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

        public List<string> Roles { get; set; }
        public List<TransactionViewModel> Transactions { set; get; }
        public List<ExchangeViewModel> Exchanges { set; get; }
        public List<SupportViewModel> Supports { set; get; }
    }
}
