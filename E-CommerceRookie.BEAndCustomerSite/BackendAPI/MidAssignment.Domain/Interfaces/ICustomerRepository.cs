using System;
using System.Collections.Generic;
using System.Text;

namespace MidAssignment.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<bool> IsEmailExist(string email);
    }
}
