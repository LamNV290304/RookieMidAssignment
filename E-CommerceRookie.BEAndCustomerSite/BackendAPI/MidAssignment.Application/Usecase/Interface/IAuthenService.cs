using System;
using System.Collections.Generic;
using System.Text;

namespace MidAssignment.Application.Usecase.Interface
{
    public interface IAuthenService
    {
        Task<string> Login(LoginDto loginDto);
    }
}
