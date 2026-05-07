using System;

namespace MidAssignment.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
