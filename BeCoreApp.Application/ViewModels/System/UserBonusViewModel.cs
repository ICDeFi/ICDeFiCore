using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeCoreApp.Application.ViewModels.System
{
    public class UserBonusViewModel
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal RewardPoint { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal RecruitingBonus { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UplineFund { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal MoneyReceive { get; set; }

        public string LevelStrategyName { get; set; }

        public UserLevelStrategyStatus LevelStrategy { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public Status Status { get; set; }

    }
}
