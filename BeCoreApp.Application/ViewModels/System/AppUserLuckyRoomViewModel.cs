using BeCoreApp.Data.Enums;
using System;

namespace BeCoreApp.Application.ViewModels.System
{
    public class AppUserLuckyRoomViewModel
    {
        public int Id { get; set; }
        public decimal AmountBet { get; set; }
        public decimal AmountReceive { get; set; }
        public int LuckyRoomId { get; set; }
        public string LuckyRoomName { get; set; }
        public Guid AppUserId { get; set; }
        public string AppUserName { get; set; }
        public AppUserLuckyRoomStatus Status { get; set; }
        public string StatusName { get; set; }
        public DateTime DateCreated { get; set; }

        public LuckyRoomViewModel LuckyRoom { get; set; }
        public AppUserViewModel AppUser { get; set; }
    }
}
