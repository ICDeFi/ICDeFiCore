using BeCoreApp.Data.Enums;
using BeCoreApp.Data.Interfaces;
using BeCoreApp.Infrastructure.SharedKernel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeCoreApp.Data.Entities
{
    [Table("LuckyRooms")]
    public class LuckyRoom : DomainEntity<int>
    {
        public LuckyRoomType Type { get; set; }
        public LuckyRoomStatus Status { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<AppUserLuckyRoom> AppUserLuckyRooms { get; set; }
    }
}