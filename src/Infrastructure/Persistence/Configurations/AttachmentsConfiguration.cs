using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Infrastructure.Persistence.Configurations
{
    public class AttachmentsConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.Property(a => a.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder
                .HasOne<Step>(a => a.Step)
                .WithMany(s => s.Attachments);
        }
    }
}
