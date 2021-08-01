using BeCoreApp.Data.EF.Extensions;
using BeCoreApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Configurations
{
    public class EnterpriseConfiguration : DbEntityConfiguration<Enterprise>
    {
        public override void Configure(EntityTypeBuilder<Enterprise> entity)
        {
            entity.HasOne(u => u.Province).WithMany(u => u.Enterprises).IsRequired().OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(u => u.Ward).WithMany(u => u.Enterprises).IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
