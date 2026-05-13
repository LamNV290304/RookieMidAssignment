using System;
using System.Collections.Generic;
using System.Text;

namespace MidAssignment.Infrastructure.Persistences.Configurations
{
    public class AdminUserConfiguration : IEntityTypeConfiguration<AdminUser>
    {
        public void Configure(EntityTypeBuilder<AdminUser> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(a => a.PasswordHash)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
