using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Infrastructure.Persistence.Configurations
{
    public class StepConfiguration : IEntityTypeConfiguration<Step>
    {
        public void Configure(EntityTypeBuilder<Step> builder)
        {
            builder
                .HasMany(s => s.Attachments)
                .WithOne(a => a.Step);

            builder.Property(s => s.StepData)
                .HasMaxLength(65536);

            builder.Property(s => s.Comment)
                .HasMaxLength(2048);

            builder
                .HasOne(s => s.GraduationPaper)
                .WithMany(g => g.Steps);
        }
    }
}
