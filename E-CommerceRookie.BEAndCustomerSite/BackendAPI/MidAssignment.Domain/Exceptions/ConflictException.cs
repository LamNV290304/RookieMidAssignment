using System;
using System.Collections.Generic;
using System.Text;

namespace MidAssignment.Domain.Exceptions
{
    public class ConflictException : ApplicationException
    {
        public ConflictException(string message)
            : base("Conflict", message)
        {
        }
    }
}
