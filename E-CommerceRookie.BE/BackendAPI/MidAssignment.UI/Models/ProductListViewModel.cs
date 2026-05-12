using MidAssignment.Shared.DTOs;

namespace MidAssignment.UI.Models
{
    public class ProductListViewModel
    {
        public IReadOnlyList<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public IReadOnlyList<ProductDto> Products { get; set; } = new List<ProductDto>();
        public Guid? SelectedCategoryId { get; set; }
        public string? Keyword { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
