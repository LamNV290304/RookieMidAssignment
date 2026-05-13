using System;
using System.Collections.Generic;
using System.Text;

namespace MidAssignment.Shared.DTOs
{
    public class LoginDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
