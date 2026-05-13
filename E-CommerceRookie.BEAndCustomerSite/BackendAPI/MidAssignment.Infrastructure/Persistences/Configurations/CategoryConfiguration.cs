using System;
using System.Collections.Generic;
using System.Text;

namespace MidAssignment.Infrastructure.Persistences.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public virtual void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500);
        }
    }
}
