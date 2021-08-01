using BeCoreApp.Data.Enums;
using BeCoreApp.Data.Interfaces;
using BeCoreApp.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeCoreApp.Data.Entities
{
    [Table("UserBonuses")]
    public class UserBonus : DomainEntity<int>, IDateTracking, ISwitchable
    {
        [Column(TypeName = "decimal(18, 2)")]
        public decimal RewardPoint { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal RecruitingBonus { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UplineFund { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal MoneyReceive { get; set; }

        public UserLevelStrategyStatus LevelStrategy { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public Status Status { get; set; }
    }
}
