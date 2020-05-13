using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Infrastructure.Persistence.Configurations
{
    public class LanguageConfiguration: IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.Property(l => l.Title_EN).HasMaxLength(256);
            builder.Property(l => l.Title_LV).HasMaxLength(256);
            builder.Property(l => l.Title_RU).HasMaxLength(256);
        }
    }
}
