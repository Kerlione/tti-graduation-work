using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Infrastructure.Persistence.Configurations
{
    public class SupervisorLanguagesConfiguration : IEntityTypeConfiguration<SupervisorLanguage>
    {
        public void Configure(EntityTypeBuilder<SupervisorLanguage> builder)
        {
            builder.HasKey(sl => new
            {
                sl.SupervisorId,
                sl.LanguageId
            });

            builder.HasOne(sl => sl.Supervisor)
                .WithMany(s => s.SupervisorLanguages)
                .HasForeignKey(sl => sl.SupervisorId);

            builder.HasOne(sl => sl.Language)
                .WithMany(l => l.SupervisorLanguages)
                .HasForeignKey(sl => sl.LanguageId);
        }
    }
}
