using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Infrastructure.Persistence.Configurations
{
    public class SupervisorConfiguration : IEntityTypeConfiguration<Supervisor>
    {
        public void Configure(EntityTypeBuilder<Supervisor> builder)
        {
            builder
                .HasAlternateKey(s => s.ExternalId);

            builder
                .Property(s => s.Name).HasMaxLength(256);
            builder
                .Property(s => s.Surname).HasMaxLength(256);

            builder
                .HasOne(s => s.User);

            builder
                .HasOne(s => s.Faculty);

            builder
                .HasMany(s => s.GraduationPapers)
                .WithOne(g => g.Supervisor)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(s => s.SupervisorLanguages);

            builder
                .HasOne(s => s.JobPosition);

            builder
                .HasMany(s => s.ThesisTopics)
                .WithOne(t => t.Supervisor);

            builder
                .HasMany(s => s.FieldsOfInterest)
                .WithOne(f => f.Supervisor);
        }
    }
}
