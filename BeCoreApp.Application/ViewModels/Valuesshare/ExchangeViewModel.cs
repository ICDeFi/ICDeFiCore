using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeCoreApp.Application.ViewModels.Valuesshare
{
    public class ExchangeViewModel
    {
        public int Id { get; set; }
        public Guid AppUserId { set; get; }
        public ExchangeType WalletFrom { set; get; }
        public ExchangeType WalletTo { set; get; }
        public DateTime ExchangeDate { get; set; }
        public decimal Amount { get; set; }


        public AppUserViewModel AppUser { set; get; }
    }
}