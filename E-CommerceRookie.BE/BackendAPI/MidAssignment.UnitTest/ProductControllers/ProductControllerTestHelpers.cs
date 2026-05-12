using System;
using System.Threading.Tasks;
using MidAssignment.Application.Usecase.Interface;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.ProductControllers
{
    internal sealed class FakeProductService : IProductService
    {
        public Guid CreateResult { get; set; } = Guid.NewGuid();
        public ProductCreateDto? LastCreateDto { get; private set; }
        public (Guid Id, ProductUpdateDto Dto)? LastUpdate { get; private set; }
        public Guid? LastDeleteId { get; private set; }
        public (Guid Id, string Url)? LastDeleteImage { get; private set; }
        public Guid? LastGetById { get; private set; }
        public (int PageNumber, int PageSize, string? Keyword, Guid? CategoryId)? LastGetPaged { get; private set; }

        public ProductDto ProductToReturn { get; set; } = new ProductDto();
        public PagedResultDto<ProductDto> PagedResultToReturn { get; set; } = new PagedResultDto<ProductDto>();

        public Task<Guid> CreateProductAsync(ProductCreateDto dto)
        {
            LastCreateDto = dto;
            return Task.FromResult(CreateResult);
        }

        public Task UpdateProductAsync(Guid id, ProductUpdateDto dto)
        {
            LastUpdate = (id, dto);
            return Task.CompletedTask;
        }

        public Task DeleteProductAsync(Guid id)
        {
            LastDeleteId = id;
            return Task.CompletedTask;
        }

        public Task DeleteProductImageAsync(Guid productId, string imageUrl)
        {
            LastDeleteImage = (productId, imageUrl);
            return Task.CompletedTask;
        }

        public Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            LastGetById = id;
            return Task.FromResult(ProductToReturn);
        }

        public Task<PagedResultDto<ProductDto>> GetPagedProductsAsync(int pageNumber, int pageSize, string? keyword = null, Guid? categoryId = null)
        {
            LastGetPaged = (pageNumber, pageSize, keyword, categoryId);
            return Task.FromResult(PagedResultToReturn);
        }
    }
}
