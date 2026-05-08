using MidAssignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidAssignment.Infrastructure.Persistences.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(c => c.Email).IsUnique();

            builder.Property(c => c.Phone)
                .HasMaxLength(20);
        }
    }
}
