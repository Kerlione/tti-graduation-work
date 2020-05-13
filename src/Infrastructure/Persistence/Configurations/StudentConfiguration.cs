using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Infrastructure.Persistence.Configurations
{
    public class StudentConfiguration: IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .HasAlternateKey(s => s.ExternalId);
            builder
                .Property(s => s.Name)
                .HasMaxLength(256);
            builder
                .Property(s => s.Surname)
                .HasMaxLength(256);

            builder
                .HasOne(s => s.Programe);
            builder
                .HasOne(s => s.User);

            builder
                .HasOne(s => s.GraduationPaper)
                .WithOne(g => g.Student)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
