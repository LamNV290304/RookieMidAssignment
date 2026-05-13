using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Application.Usecase;
using MidAssignment.Domain.Entities;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.ProductServices
{
    public class GetProductByIdTest
    {
        [Fact]
        public async Task GetProductByIdAsync_WhenNotFound_ThrowsKeyNotFoundException()
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
                service.GetProductByIdAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task GetProductByIdAsync_WhenFound_ReturnsDto()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var categoryId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId,
                Name = "Mouse",
                Description = "Wireless",
                Price = 25m,
                CategoryId = categoryId,
                Category = new Category { Id = categoryId, Name = "Accessories", Description = "Accessories" },
                Images = new List<ProductImage>
                {
                    new ProductImage { Id = Guid.NewGuid(), Url = "/uploads/a.png" },
                    new ProductImage { Id = Guid.NewGuid(), Url = "/uploads/b.png" }
                },
                CreatedAt = DateTime.UtcNow
            };

            var productRepository = new FakeProductRepository(new[] { product });
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
            var result = await service.GetProductByIdAsync(productId);

            // Assert
            Assert.Equal(productId, result.Id);
            Assert.Equal("Mouse", result.Name);
            Assert.Equal("Accessories", result.CategoryName);
            Assert.Equal(2, result.ImageUrls.Count);
        }
    }
}
