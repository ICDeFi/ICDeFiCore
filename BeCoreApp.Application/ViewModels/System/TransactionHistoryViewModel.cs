using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeCoreApp.Application.ViewModels.System
{
    public class TransactionHistoryViewModel
    {
        public int Id { get; set; }
        public string TransactionHash { get; set; }
        public string Image { get; set; }
        public decimal Amount { get; set; }
        public TransactionHistoryType Type { get; set; }
        public string TypeName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public Guid AppUserId { get; set; }

        public string AppUserName { get; set; }

        public string Note { get; set; }

        public AppUserViewModel AppUser { set; get; }
    }
}
