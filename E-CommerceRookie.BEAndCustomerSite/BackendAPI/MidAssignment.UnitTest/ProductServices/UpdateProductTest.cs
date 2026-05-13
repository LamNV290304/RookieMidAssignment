using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using MidAssignment.Application.Usecase;
using MidAssignment.Domain.Entities;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.ProductServices
{
    public class UpdateProductTest
    {
        [Fact]
        public async Task UpdateProductAsync_WhenValidationFails_ThrowsValidationException()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var productRepository = new FakeProductRepository(new[]
            {
                new Product { Id = Guid.NewGuid(), Name = "Old", Description = "Old", Price = 1m }
            });
            var categoryRepository = new FakeCategoryRepository();
            var createValidator = new InlineValidator<ProductCreateDto>();
            var updateValidator = new InlineValidator<ProductUpdateDto>();
            updateValidator.RuleFor(x => x.Name).NotEmpty();
            var environment = new FakeWebHostEnvironment { ContentRootPath = tempRoot.ContentRootPath };

            var service = new ProductService(
                productRepository,
                categoryRepository,
                createValidator,
                updateValidator,
                environment);

            var dto = new ProductUpdateDto
            {
                Name = string.Empty,
                Description = "Invalid",
                Price = 1m,
                CategoryId = Guid.NewGuid()
            };

            // Act
            await Assert.ThrowsAsync<ValidationException>(() =>
                service.UpdateProductAsync(productRepository.Items[0].Id, dto));

            // Assert
            Assert.Empty(productRepository.UpdatedEntities);
        }

        [Fact]
        public async Task UpdateProductAsync_WhenProductNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var productRepository = new FakeProductRepository();
            var categoryRepository = new FakeCategoryRepository();
            var createValidator = new InlineValidator<ProductCreateDto>();
            var updateValidator = new InlineValidator<ProductUpdateDto>();
            updateValidator.RuleFor(x => x.Name).NotEmpty();
            var environment = new FakeWebHostEnvironment { ContentRootPath = tempRoot.ContentRootPath };

            var service = new ProductService(
                productRepository,
                categoryRepository,
                createValidator,
                updateValidator,
                environment);

            var dto = new ProductUpdateDto
            {
                Name = "Updated",
                Description = "Updated",
                Price = 10m,
                CategoryId = Guid.NewGuid()
            };

            // Act + Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                service.UpdateProductAsync(Guid.NewGuid(), dto));
        }

        [Fact]
        public async Task UpdateProductAsync_WhenCategoryMissing_ThrowsKeyNotFoundException()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var productId = Guid.NewGuid();
            var productRepository = new FakeProductRepository(new[]
            {
                new Product { Id = productId, Name = "Old", Description = "Old", Price = 1m }
            });
            var categoryRepository = new FakeCategoryRepository();
            var createValidator = new InlineValidator<ProductCreateDto>();
            var updateValidator = new InlineValidator<ProductUpdateDto>();
            updateValidator.RuleFor(x => x.Name).NotEmpty();
            var environment = new FakeWebHostEnvironment { ContentRootPath = tempRoot.ContentRootPath };

            var service = new ProductService(
                productRepository,
                categoryRepository,
                createValidator,
                updateValidator,
                environment);

            var dto = new ProductUpdateDto
            {
                Name = "Updated",
                Description = "Updated",
                Price = 10m,
                CategoryId = Guid.NewGuid()
            };

            // Act + Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                service.UpdateProductAsync(productId, dto));
        }

        [Fact]
        public async Task UpdateProductAsync_WhenValid_UpdatesProductAndReplacesImages()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var categoryId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId,
                Name = "Old",
                Description = "Old",
                Price = 1m,
                CategoryId = categoryId,
                Category = new Category { Id = categoryId, Name = "Accessories", Description = "Accessories" },
                Images = new List<ProductImage>
                {
                    new ProductImage { Id = Guid.NewGuid(), Url = "/uploads/old.jpg" }
                }
            };

            var productRepository = new FakeProductRepository(new[] { product });
            var categoryRepository = new FakeCategoryRepository(new[]
            {
                new Category { Id = categoryId, Name = "Accessories", Description = "Accessories" }
            });
            var createValidator = new InlineValidator<ProductCreateDto>();
            var updateValidator = new InlineValidator<ProductUpdateDto>();
            updateValidator.RuleFor(x => x.Name).NotEmpty();
            var environment = new FakeWebHostEnvironment { ContentRootPath = tempRoot.ContentRootPath };

            var service = new ProductService(
                productRepository,
                categoryRepository,
                createValidator,
                updateValidator,
                environment);

            var image = ProductTestFileHelper.CreateFormFile("new.jpg", new byte[] { 5, 6, 7 });
            var dto = new ProductUpdateDto
            {
                Name = "New",
                Description = "New description",
                Price = 25m,
                CategoryId = categoryId,
                ImageUrls = new List<IFormFile> { image }
            };

            // Act
            await service.UpdateProductAsync(productId, dto);

            // Assert
            var updated = Assert.Single(productRepository.UpdatedEntities);
            Assert.Equal("New", updated.Name);
            Assert.Single(updated.Images);
            Assert.DoesNotContain(updated.Images, img => img.Url == "/uploads/old.jpg");

            var uploadsPath = ProductTestFileHelper.GetUploadsPath(tempRoot.ContentRootPath);
            var fileName = Path.GetFileName(updated.Images.First().Url);
            var filePath = Path.Combine(uploadsPath, fileName);
            Assert.True(File.Exists(filePath));
        }

        [Fact]
        public async Task UpdateProductAsync_WhenImagesNullOrEmpty_ClearsImages()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var categoryId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId,
                Name = "Old",
                Description = "Old",
                Price = 1m,
                CategoryId = categoryId,
                Category = new Category { Id = categoryId, Name = "Accessories", Description = "Accessories" },
                Images = new List<ProductImage>
                {
                    new ProductImage { Id = Guid.NewGuid(), Url = "/uploads/old.jpg" }
                }
            };

            var productRepository = new FakeProductRepository(new[] { product });
            var categoryRepository = new FakeCategoryRepository(new[]
            {
                new Category { Id = categoryId, Name = "Accessories", Description = "Accessories" }
            });
            var createValidator = new InlineValidator<ProductCreateDto>();
            var updateValidator = new InlineValidator<ProductUpdateDto>();
            updateValidator.RuleFor(x => x.Name).NotEmpty();
            var environment = new FakeWebHostEnvironment { ContentRootPath = tempRoot.ContentRootPath };

            var service = new ProductService(
                productRepository,
                categoryRepository,
                createValidator,
                updateValidator,
                environment);

            var emptyFile = new FormFile(new MemoryStream(), 0, 0, "file", "empty.jpg");
            var dto = new ProductUpdateDto
            {
                Name = "New",
                Description = "New description",
                Price = 25m,
                CategoryId = categoryId,
                ImageUrls = new List<IFormFile> { null!, emptyFile }
            };

            // Act
            await service.UpdateProductAsync(productId, dto);

            // Assert
            var updated = Assert.Single(productRepository.UpdatedEntities);
            Assert.Empty(updated.Images);
        }
    }
}
