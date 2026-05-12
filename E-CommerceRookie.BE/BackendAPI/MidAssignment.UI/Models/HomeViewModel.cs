using MidAssignment.Shared.DTOs;

namespace MidAssignment.UI.Models
{
    public class HomeViewModel
    {
        public IReadOnlyList<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public IReadOnlyList<ProductDto> Products { get; set; } = new List<ProductDto>();
        public IReadOnlyList<ProductDto> FeaturedProducts { get; set; } = new List<ProductDto>();
    }
}
