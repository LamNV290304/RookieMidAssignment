using MidAssignment.Shared.DTOs;

namespace MidAssignment.UI.Services
{
    public interface IProductApiService
    {
        Task<PagedResultDto<ProductDto>> GetProductsAsync(int pageNumber = 1, int pageSize = 12, Guid? categoryId = null, string? keyword = null);
        Task<IReadOnlyList<ProductDto>> GetFeaturedProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(Guid id);
    }
}
