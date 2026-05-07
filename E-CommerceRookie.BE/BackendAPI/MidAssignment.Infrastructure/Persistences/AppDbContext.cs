using System;
using System.Collections.Generic;
using System.Text;
using MidAssignment.Domain.Entities;

namespace MidAssignment.Infrastructure.Persistences
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
