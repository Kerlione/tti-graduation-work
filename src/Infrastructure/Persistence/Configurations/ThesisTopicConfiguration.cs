using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Infrastructure.Persistence.Configurations
{
    public class ThesisTopicConfiguration : IEntityTypeConfiguration<ThesisTopic>
    {
        public void Configure(EntityTypeBuilder<ThesisTopic> builder)
        {
            builder.Property(g => g.Title_EN).HasMaxLength(1024);
            builder.Property(g => g.Title_LV).HasMaxLength(1024);
            builder.Property(g => g.Title_RU).HasMaxLength(1024);

            builder
                .HasOne(t => t.Supervisor)
                .WithMany(s => s.ThesisTopics);
        }
    }
}
