using System;
using System.Collections.Generic;
using System.Text;

namespace MidAssignment.Infrastructure.Persistences
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
