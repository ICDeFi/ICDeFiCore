using BeCoreApp.Data.EF.Extensions;
using BeCoreApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Data.EF.Configurations
{
    public class ProjectConfiguration : DbEntityConfiguration<Project>
    {
        public override void Configure(EntityTypeBuilder<Project> entity)
        {
            entity.HasOne(u => u.Enterprise).WithMany(u => u.Projects).IsRequired().OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(u => u.Province).WithMany(u => u.Projects).IsRequired().OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(u => u.Ward).WithMany(u => u.Projects).IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
