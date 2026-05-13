using MidAssignment.Domain.Entities;
using MidAssignment.Infrastructure.Persistences;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidAssignment.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<AdminUser?> GetAdminByEmailAsync(string email)
        {
            return Task.FromResult(_context.AdminUsers.FirstOrDefault(a => a.Email == email));
        }
    }
}
