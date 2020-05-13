using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Infrastructure.Persistence.Configurations
{
    public class ProgrameConfiguration: IEntityTypeConfiguration<Programe>
    {
        public void Configure(EntityTypeBuilder<Programe> builder)
        {
            builder.HasAlternateKey(p => p.ExternalId);

            builder.Property(p => p.Title_EN).HasMaxLength(250);
            builder.Property(p => p.Title_LV).HasMaxLength(250);
            builder.Property(p => p.Title_RU).HasMaxLength(250);

            builder
                .HasOne(p => p.Faculty)
                .WithMany(f => f.Programes);
        }
    }
}
