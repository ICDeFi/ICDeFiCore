using BeCoreApp.Data.Enums;
using BeCoreApp.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeCoreApp.Data.Entities
{
    [Table("AppUserLuckyRooms")]
    public class AppUserLuckyRoom : DomainEntity<int>
    {
        public int LuckyRoomId { get; set; }
        public Guid AppUserId { get; set; }
        public decimal AmountBet { get; set; }
        public decimal AmountReceive { get; set; }
        public AppUserLuckyRoomStatus Status { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey("LuckyRoomId")]
        public virtual LuckyRoom LuckyRoom { set; get; }

        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { set; get; }
    }
}