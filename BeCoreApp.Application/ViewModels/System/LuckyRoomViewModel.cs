using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;

namespace BeCoreApp.Application.ViewModels.System
{
    public class LuckyRoomViewModel
    {
        public LuckyRoomViewModel()
        {
            AppUserLuckyRooms = new List<AppUserLuckyRoomViewModel>();
        }
        public int Id { get; set; }
        public LuckyRoomType Type { get; set; }
        public string TypeName { get; set; }
        public LuckyRoomStatus Status { get; set; }
        public string StatusName { get; set; }
        public DateTime DateCreated { get; set; }

        public int TotalNumberofGamers { get; set; }
        public string LastGamerName { get; set; }
        public string PreviousWinner { get; set; }

        public decimal TotalTRXAccumulationOfDay { get; set; }
        public decimal TotalTRXAccumulationOf5Day { get; set; }

        public List<AppUserLuckyRoomViewModel> AppUserLuckyRooms { get; set; }

    }
}
