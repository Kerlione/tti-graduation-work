using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(u => u.Username)
                .HasMaxLength(100)
                .IsRequired();


            builder
                .Property(u => u.Password)
                .HasMaxLength(1024)
                .IsRequired();

            builder
                .HasAlternateKey(u => u.Username);
        }
    }
}
