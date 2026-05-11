using MidAssignment.Infrastructure.Persistences;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidAssignment.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        protected readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<bool> IsEmailExist(string email)
        {
            return Task.FromResult(_context.Customers.Any(c => c.Email == email && !c.IsDeleted));
        }
    }
}
