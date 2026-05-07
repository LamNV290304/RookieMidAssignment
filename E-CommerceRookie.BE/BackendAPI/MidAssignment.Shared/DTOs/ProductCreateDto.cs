using System;
using System.Collections.Generic;

namespace MidAssignment.Shared.DTOs
{
    public class ProductCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public List<string> ImageUrls { get; set; } = new();
    }
}
