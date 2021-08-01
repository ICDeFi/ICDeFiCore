using BeCoreApp.Data.EF.Extensions;
using BeCoreApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Configurations
{
    public class WardConfiguration : DbEntityConfiguration<Ward>
    {
        public override void Configure(EntityTypeBuilder<Ward> entity)
        {
            entity.HasOne(u => u.Province).WithMany(u => u.Wards).IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
