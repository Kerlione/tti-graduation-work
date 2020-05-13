using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Infrastructure.Persistence.Configurations
{
    public class FacultyConfiguration: IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder.HasAlternateKey(f => f.ExternalId);

            builder.Property(f => f.Title_EN).HasMaxLength(250);
            builder.Property(f => f.Title_LV).HasMaxLength(250);
            builder.Property(f => f.Title_RU).HasMaxLength(250);


            builder.Property(f => f.ShortTitle_EN).HasMaxLength(50);
            builder.Property(f => f.ShortTitle_LV).HasMaxLength(50);
            builder.Property(f => f.ShortTitle_RU).HasMaxLength(50);

            builder
                .HasMany(f => f.Programes)
                .WithOne(p => p.Faculty);
        }
    }
}
