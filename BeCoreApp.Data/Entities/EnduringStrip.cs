using BeCoreApp.Data.Enums;
using BeCoreApp.Data.Interfaces;
using BeCoreApp.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeCoreApp.Data.Entities
{
    [Table("EnduringStrips")]
    public class EnduringStrip : DomainEntity<int>, ISwitchable, IDateTracking
    {
        public EnduringStrip()
        {
        }

        public EnduringStrip(int id, int operatingEnvironmentId, int creatureId,
            decimal? temperatureMin, decimal? temperatureMax, decimal? phMin, decimal? phMax,
            decimal? ammoniaMin, decimal? ammoniaMax, decimal? nitriteMin, decimal? nitriteMax,
            decimal? nitrateMin, decimal? nitrateMax, decimal? dissolvedOxygenMin, decimal? dissolvedOxygenMax,
            Status status)
        {
            Id = id;
            OperatingEnvironmentId = operatingEnvironmentId;
            CreatureId = creatureId;
            TemperatureMin = temperatureMin;
            TemperatureMax = temperatureMax;
            PHMin = phMin;
            PHMax = phMax;
            AmmoniaMin = ammoniaMin;
            AmmoniaMax = ammoniaMax;
            NitriteMin = nitriteMin;
            NitriteMax = nitriteMax;
            NitrateMin = nitrateMin;
            NitrateMax = nitrateMax;
            DissolvedOxygenMin = dissolvedOxygenMin;
            DissolvedOxygenMax = dissolvedOxygenMax;
            Status = status;
        }
        [Required]
        public int OperatingEnvironmentId { get; set; }
        [Required]
        public int CreatureId { get; set; }
        public decimal? TemperatureMin { get; set; }
        public decimal? TemperatureMax { get; set; }
        public decimal? PHMin { get; set; }
        public decimal? PHMax { get; set; }
        public decimal? AmmoniaMin { get; set; }
        public decimal? AmmoniaMax { get; set; }
        public decimal? NitriteMin { get; set; }
        public decimal? NitriteMax { get; set; }
        public decimal? NitrateMin { get; set; }
        public decimal? NitrateMax { get; set; }
        public decimal? DissolvedOxygenMin { get; set; }
        public decimal? DissolvedOxygenMax { get; set; }

        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [ForeignKey("OperatingEnvironmentId")]
        public virtual OperatingEnvironment OperatingEnvironment { set; get; }

        [ForeignKey("CreatureId")]
        public virtual Creature Creature { set; get; }
    }
}
