using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeCoreApp.Application.ViewModels.Valuesshare
{
    public class NetworkViewModel
    {
        public NetworkViewModel()
        {
            Members = new List<AppUserViewModel>();
        }
        public string FullName { set; get; }
        public string Email { set; get; }
        public string Member { set; get; }
        public string Referal { get; set; }
        public string PhoneNumber { get; set; }

        public string ReferalLink { get; set; }

        public int TotalMember { get; set; }

        public int TotalF1 { get; set; }
        public int TotalF2 { get; set; }
        public int TotalF3 { get; set; }
        public int TotalF4 { get; set; }
        public int TotalF5 { get; set; }

        public List<AppUserViewModel> Members { get; set; }
    }
}