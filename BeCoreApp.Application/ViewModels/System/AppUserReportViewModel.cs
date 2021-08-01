using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;

namespace BeCoreApp.Application.ViewModels.System
{
    public class AppUserReportViewModel
    {
        public int TotalMemberOfSystem { get; set; }

        public int TotalMemberActive { get; set; }

        public int TotalMemberInactive { get; set; }

        public int TotalMemberNewOfDay { get; set; }

        public decimal TotalAmountInSystem { get; set; }

        public decimal TotalRewardPoints { get; set; }

        public decimal TotalAmountInMember { get; set; }

        public decimal TotalAmountIn7Member { get; set; }

        public decimal TotalAmountUplineFundOfMember { get; set; }

        public decimal TotalProfit { get; set; }
    }
}
