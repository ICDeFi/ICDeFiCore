using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeCoreApp.Application.ViewModels.System
{
    public class NotifyViewModel
    {
        public int Id { get; set; }

        public string Name { set; get; }
        public string MildContent { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public Status Status { set; get; }
        public decimal WalletTRX { get; set; }
    }
}
