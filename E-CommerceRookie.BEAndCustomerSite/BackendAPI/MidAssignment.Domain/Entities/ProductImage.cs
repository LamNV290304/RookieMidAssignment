using System;

namespace MidAssignment.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public Guid Id { get; set; }
        public string Url { get; set; } = string.Empty;
        
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
    }
}
