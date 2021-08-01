using BeCoreApp.Data.EF.Extensions;
using BeCoreApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Configurations
{
    public class StreetConfiguration : DbEntityConfiguration<Street>
    {
        public override void Configure(EntityTypeBuilder<Street> entity)
        {
            entity.HasOne(u => u.Province).WithMany(u => u.Streets)
                .IsRequired().OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(u => u.Ward).WithMany(u => u.Streets)
                .IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
