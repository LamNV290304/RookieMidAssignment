using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Application.Usecase;
using MidAssignment.Domain.Entities;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.ProductServices
{
    public class GetPagedProductsTest
    {
        [Fact]
        public async Task GetPagedProductsAsync_WhenKeywordProvided_FiltersItems()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var category = new Category { Id = Guid.NewGuid(), Name = "Books", Description = "Books" };
            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Book A",
                    Description = "Novel",
                    Price = 10m,
                    CategoryId = category.Id,
                    Category = category
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Notebook",
                    Description = "Paper",
                    Price = 5m,
                    CategoryId = category.Id,
                    Category = category
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Mouse",
                    Description = "Wireless",
                    Price = 25m,
                    CategoryId = Guid.NewGuid(),
                    Category = new Category { Id = Guid.NewGuid(), Name = "Accessories", Description = "Accessories" }
                }
            };

            var productRepository = new FakeProductRepository(products);
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
            var result = await service.GetPagedProductsAsync(1, 10, "Book");
            var items = result.Items.ToList();

            // Assert
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(2, items.Count);
            Assert.Contains(items, item => item.Name == "Book A");
            Assert.Contains(items, item => item.Name == "Notebook");
        }

        [Fact]
        public async Task GetPagedProductsAsync_WhenCategoryProvided_FiltersItems()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var categoryA = new Category { Id = Guid.NewGuid(), Name = "Accessories", Description = "Accessories" };
            var categoryB = new Category { Id = Guid.NewGuid(), Name = "Books", Description = "Books" };
            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Mouse",
                    Description = "Wireless",
                    Price = 25m,
                    CategoryId = categoryA.Id,
                    Category = categoryA
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Keyboard",
                    Description = "Mechanical",
                    Price = 99m,
                    CategoryId = categoryA.Id,
                    Category = categoryA
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Book A",
                    Description = "Novel",
                    Price = 10m,
                    CategoryId = categoryB.Id,
                    Category = categoryB
                }
            };

            var productRepository = new FakeProductRepository(products);
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
            var result = await service.GetPagedProductsAsync(1, 10, null, categoryA.Id);
            var items = result.Items.ToList();

            // Assert
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(2, items.Count);
            Assert.All(items, item => Assert.Equal(categoryA.Id, item.CategoryId));
        }

        [Fact]
        public async Task GetPagedProductsAsync_WhenKeywordAndCategoryProvided_FiltersItems()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var categoryA = new Category { Id = Guid.NewGuid(), Name = "Books", Description = "Books" };
            var categoryB = new Category { Id = Guid.NewGuid(), Name = "Accessories", Description = "Accessories" };
            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Book A",
                    Description = "Novel",
                    Price = 10m,
                    CategoryId = categoryA.Id,
                    Category = categoryA
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Notebook",
                    Description = "Paper",
                    Price = 5m,
                    CategoryId = categoryA.Id,
                    Category = categoryA
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Book A",
                    Description = "Novel",
                    Price = 10m,
                    CategoryId = categoryB.Id,
                    Category = categoryB
                }
            };

            var productRepository = new FakeProductRepository(products);
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
            var result = await service.GetPagedProductsAsync(1, 10, "Book", categoryA.Id);
            var items = result.Items.ToList();

            // Assert
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(2, items.Count);
            Assert.All(items, item => Assert.Equal(categoryA.Id, item.CategoryId));
        }

        [Fact]
        public async Task GetPagedProductsAsync_WhenNoFilters_ReturnsAllItems()
        {
            // Arrange
            using var tempRoot = TempContentRoot.Create();
            var category = new Category { Id = Guid.NewGuid(), Name = "Books", Description = "Books" };
            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Book A",
                    Description = "Novel",
                    Price = 10m,
                    CategoryId = category.Id,
                    Category = category
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Mouse",
                    Description = "Wireless",
                    Price = 25m,
                    CategoryId = Guid.NewGuid(),
                    Category = new Category { Id = Guid.NewGuid(), Name = "Accessories", Description = "Accessories" }
                }
            };

            var productRepository = new FakeProductRepository(products);
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
            var result = await service.GetPagedProductsAsync(1, 10);
            var items = result.Items.ToList();

            // Assert
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(2, items.Count);
        }
    }
}
