using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Infrastructure.Persistence.Configurations
{
    public class GraduationPaperConfiguration: IEntityTypeConfiguration<GraduationPaper>
    {
        public void Configure(EntityTypeBuilder<GraduationPaper> builder)
        {
            builder
                .HasOne(g => g.Supervisor)
                .WithMany(s => s.GraduationPapers);

            builder
                .HasOne(g => g.Student)
                .WithOne(s => s.GraduationPaper);

            builder
                .HasMany(g => g.Steps)
                .WithOne(s => s.GraduationPaper);

            builder.HasOne(g => g.Faculty);

            builder.Property(g => g.Title_EN).HasMaxLength(1024);
            builder.Property(g => g.Title_LV).HasMaxLength(1024);
            builder.Property(g => g.Title_RU).HasMaxLength(1024);
        }
    }
}
