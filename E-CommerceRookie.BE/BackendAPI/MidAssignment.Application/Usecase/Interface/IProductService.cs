using System;
using System.Threading.Tasks;

namespace MidAssignment.Application.Usecase.Interface
{
    public interface IProductService
    {
        Task<Guid> CreateProductAsync(ProductCreateDto dto);
        Task UpdateProductAsync(Guid id, ProductUpdateDto dto);
        Task DeleteProductAsync(Guid id);
        Task DeleteProductImageAsync(Guid productId, string imageUrl);
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task<PagedResultDto<ProductDto>> GetPagedProductsAsync(int pageNumber, int pageSize, string? keyword = null, Guid? categoryId = null);
    }
}
