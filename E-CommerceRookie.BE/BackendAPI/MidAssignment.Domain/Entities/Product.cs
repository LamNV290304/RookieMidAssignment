using System;
using System.Collections.Generic;

namespace MidAssignment.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
        
        public virtual ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
    }
}
