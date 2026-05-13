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
    public class CreateProductTest
    {
        [Fact]
        public async Task CreateProductAsync_WhenValidationFails_ThrowsValidationException()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var productRepository = new FakeProductRepository();
            var categoryRepository = new FakeCategoryRepository();
            var createValidator = new InlineValidator<ProductCreateDto>();
            createValidator.RuleFor(x => x.Name).NotEmpty();
            var updateValidator = new InlineValidator<ProductUpdateDto>();
            var environment = new FakeWebHostEnvironment { ContentRootPath = tempRoot.ContentRootPath };

            var service = new ProductService(
                productRepository,
                categoryRepository,
                createValidator,
                updateValidator,
                environment);

            var dto = new ProductCreateDto
            {
                Name = string.Empty,
                Description = "Invalid",
                Price = 10m,
                CategoryId = Guid.NewGuid(),
                ImageUrls = new List<IFormFile>()
            };

            // Act + Assert
            await Assert.ThrowsAsync<ValidationException>(() => service.CreateProductAsync(dto));
        }

        [Fact]
        public async Task CreateProductAsync_WhenCategoryMissing_ThrowsKeyNotFoundException()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var productRepository = new FakeProductRepository();
            var categoryRepository = new FakeCategoryRepository();
            var createValidator = new InlineValidator<ProductCreateDto>();
            createValidator.RuleFor(x => x.Name).NotEmpty();
            var updateValidator = new InlineValidator<ProductUpdateDto>();
            var environment = new FakeWebHostEnvironment { ContentRootPath = tempRoot.ContentRootPath };

            var service = new ProductService(
                productRepository,
                categoryRepository,
                createValidator,
                updateValidator,
                environment);

            var dto = new ProductCreateDto
            {
                Name = "Keyboard",
                Description = "Mechanical",
                Price = 99m,
                CategoryId = Guid.NewGuid(),
                ImageUrls = new List<IFormFile>()
            };

            // Act + Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.CreateProductAsync(dto));
        }

        [Fact]
        public async Task CreateProductAsync_WhenValid_AddsProductAndSavesImages()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var categoryId = Guid.NewGuid();
            var productRepository = new FakeProductRepository();
            var categoryRepository = new FakeCategoryRepository(new[]
            {
                new Category { Id = categoryId, Name = "Accessories", Description = "Accessories" }
            });
            var createValidator = new InlineValidator<ProductCreateDto>();
            createValidator.RuleFor(x => x.Name).NotEmpty();
            var updateValidator = new InlineValidator<ProductUpdateDto>();
            var environment = new FakeWebHostEnvironment { ContentRootPath = tempRoot.ContentRootPath };

            var service = new ProductService(
                productRepository,
                categoryRepository,
                createValidator,
                updateValidator,
                environment);

            var image = ProductTestFileHelper.CreateFormFile("photo.jpg", new byte[] { 1, 2, 3 });
            var dto = new ProductCreateDto
            {
                Name = "Mouse",
                Description = "Wireless",
                Price = 25m,
                CategoryId = categoryId,
                ImageUrls = new List<IFormFile> { image }
            };

            // Act
            var id = await service.CreateProductAsync(dto);

            // Assert
            var added = Assert.Single(productRepository.AddedEntities);
            Assert.Equal(id, added.Id);
            Assert.Single(added.Images);

            var uploadsPath = ProductTestFileHelper.GetUploadsPath(tempRoot.ContentRootPath);
            var fileName = Path.GetFileName(added.Images.First().Url);
            var filePath = Path.Combine(uploadsPath, fileName);
            Assert.True(File.Exists(filePath));
        }

        [Fact]
        public async Task CreateProductAsync_WhenImagesNullOrEmpty_SkipsSavingFiles()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var categoryId = Guid.NewGuid();
            var productRepository = new FakeProductRepository();
            var categoryRepository = new FakeCategoryRepository(new[]
            {
                new Category { Id = categoryId, Name = "Accessories", Description = "Accessories" }
            });
            var createValidator = new InlineValidator<ProductCreateDto>();
            createValidator.RuleFor(x => x.Name).NotEmpty();
            var updateValidator = new InlineValidator<ProductUpdateDto>();
            var environment = new FakeWebHostEnvironment { ContentRootPath = tempRoot.ContentRootPath };

            var service = new ProductService(
                productRepository,
                categoryRepository,
                createValidator,
                updateValidator,
                environment);

            var emptyFile = new FormFile(new MemoryStream(), 0, 0, "file", "empty.jpg");
            var dto = new ProductCreateDto
            {
                Name = "Mouse",
                Description = "Wireless",
                Price = 25m,
                CategoryId = categoryId,
                ImageUrls = new List<IFormFile> { null!, emptyFile }
            };

            // Act
            await service.CreateProductAsync(dto);

            // Assert
            var added = Assert.Single(productRepository.AddedEntities);
            Assert.Empty(added.Images);
        }

        [Fact]
        public async Task CreateProductAsync_WhenImagesNull_ReturnsEmptyImageList()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var categoryId = Guid.NewGuid();
            var productRepository = new FakeProductRepository();
            var categoryRepository = new FakeCategoryRepository(new[]
            {
                new Category { Id = categoryId, Name = "Accessories", Description = "Accessories" }
            });
            var createValidator = new InlineValidator<ProductCreateDto>();
            createValidator.RuleFor(x => x.Name).NotEmpty();
            var updateValidator = new InlineValidator<ProductUpdateDto>();
            var environment = new FakeWebHostEnvironment { ContentRootPath = tempRoot.ContentRootPath };

            var service = new ProductService(
                productRepository,
                categoryRepository,
                createValidator,
                updateValidator,
                environment);

            var dto = new ProductCreateDto
            {
                Name = "Mouse",
                Description = "Wireless",
                Price = 25m,
                CategoryId = categoryId,
                ImageUrls = null!
            };

            // Act
            await service.CreateProductAsync(dto);

            // Assert
            var added = Assert.Single(productRepository.AddedEntities);
            Assert.Empty(added.Images);
        }
    }
}
