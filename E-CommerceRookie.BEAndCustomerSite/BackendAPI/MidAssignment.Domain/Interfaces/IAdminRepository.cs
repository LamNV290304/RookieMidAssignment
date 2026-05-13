using MidAssignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidAssignment.Domain.Interfaces
{
    public interface IAdminRepository
    {
        Task<AdminUser?> GetAdminByEmailAsync(string email);
    }
}
