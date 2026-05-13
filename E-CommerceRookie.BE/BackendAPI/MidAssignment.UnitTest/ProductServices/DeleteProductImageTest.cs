using System;
using System.IO;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Application.Usecase;
using MidAssignment.Domain.Entities;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.ProductServices
{
    public class DeleteProductImageTest
    {
        [Fact]
        public async Task DeleteProductImageAsync_WhenImageUrlEmpty_ThrowsKeyNotFoundException()
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
                service.DeleteProductImageAsync(Guid.NewGuid(), ""));
        }

        [Fact]
        public async Task DeleteProductImageAsync_WhenProductNotFound_ThrowsKeyNotFoundException()
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
                service.DeleteProductImageAsync(Guid.NewGuid(), "/uploads/test.png"));
        }

        [Fact]
        public async Task DeleteProductImageAsync_WhenImageNotFound_ThrowsKeyNotFoundException()
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

            // Act + Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                service.DeleteProductImageAsync(productId, "/uploads/missing.png"));
        }

        [Fact]
        public async Task DeleteProductImageAsync_WhenValid_RemovesImageAndDeletesFile()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var productId = Guid.NewGuid();
            var imageFileName = "image.png";
            var uploadsPath = ProductTestFileHelper.GetUploadsPath(tempRoot.ContentRootPath);
            Directory.CreateDirectory(uploadsPath);
            var filePath = Path.Combine(uploadsPath, imageFileName);
            File.WriteAllBytes(filePath, new byte[] { 1, 2, 3 });

            var product = new Product
            {
                Id = productId,
                Name = "Mouse",
                Description = "Wireless",
                Price = 25m,
                Images = new List<ProductImage>
                {
                    new ProductImage { Id = Guid.NewGuid(), Url = $"/uploads/{imageFileName}" }
                }
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
            await service.DeleteProductImageAsync(productId, $"/uploads/{imageFileName}");

            // Assert
            var updated = Assert.Single(productRepository.UpdatedEntities);
            Assert.Empty(updated.Images);
            Assert.False(File.Exists(filePath));
        }
    }
}
