using System;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Application.Usecase;
using MidAssignment.Domain.Entities;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.ProductServices
{
    public class DeleteProductTest
    {
        [Fact]
        public async Task DeleteProductAsync_WhenNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var productRepository = new FakeProductRepository();
            var categoryRepository = new FakeCategoryRepository();
            var createValidator = new InlineValidator<ProductCreateDto>();
            var updateValidator = new InlineValidator<ProductUpdateDto>();
            var environment = new FakeWebHostEnvironment { ContentRootPath = tempRoot.ContentRootPath };

            var service = new ProductService(
                productRepository,
                categoryRepository,
                createValidator,
                updateValidator,
                environment);

            // Act + Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                service.DeleteProductAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task DeleteProductAsync_WhenFound_RemovesProduct()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var productId = Guid.NewGuid();
            var productRepository = new FakeProductRepository(new[]
            {
                new Product { Id = productId, Name = "Mouse", Description = "Wireless", Price = 25m }
            });
            var categoryRepository = new FakeCategoryRepository();
            var createValidator = new InlineValidator<ProductCreateDto>();
            var updateValidator = new InlineValidator<ProductUpdateDto>();
            var environment = new FakeWebHostEnvironment { ContentRootPath = tempRoot.ContentRootPath };

            var service = new ProductService(
                productRepository,
                categoryRepository,
                createValidator,
                updateValidator,
                environment);

            // Act
            await service.DeleteProductAsync(productId);

            // Assert
            var deleted = Assert.Single(productRepository.DeletedEntities);
            Assert.Equal(productId, deleted.Id);
            Assert.Empty(productRepository.Items);
        }
    }
}
