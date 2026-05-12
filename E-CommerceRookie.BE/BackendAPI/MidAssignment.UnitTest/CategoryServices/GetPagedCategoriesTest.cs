using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Application.Usecase;
using MidAssignment.Domain.Entities;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.CategoryServices
{
    public class GetPagedCategoriesTest
    {
        [Fact]
        public async Task GetPagedCategoriesAsync_WhenNoKeyword_ReturnsCorrectPage()
        {
            var categories = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), Name = "Cat 1", Description = "Desc 1" },
                new Category { Id = Guid.NewGuid(), Name = "Cat 2", Description = "Desc 2" },
                new Category { Id = Guid.NewGuid(), Name = "Cat 3", Description = "Desc 3" },
                new Category { Id = Guid.NewGuid(), Name = "Cat 4", Description = "Desc 4" },
                new Category { Id = Guid.NewGuid(), Name = "Cat 5", Description = "Desc 5" }
            };

            var repository = new FakeCategoryRepository(categories);
            var createValidator = new InlineValidator<CategoryCreateDto>();
            var updateValidator = new InlineValidator<CategoryUpdateDto>();

            var service = new CategoryService(repository, createValidator, updateValidator);

            var result = await service.GetPagedCategoriesAsync(2, 2);
            var items = result.Items.ToList();

            Assert.Equal(5, result.TotalCount);
            Assert.Equal(2, result.PageNumber);
            Assert.Equal(2, result.PageSize);
            Assert.Equal(2, items.Count);
            Assert.Equal("Cat 3", items[0].Name);
            Assert.Equal("Cat 4", items[1].Name);
        }

        [Fact]
        public async Task GetPagedCategoriesAsync_WhenKeywordProvided_FiltersItems()
        {
            var categories = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), Name = "Books", Description = "All books" },
                new Category { Id = Guid.NewGuid(), Name = "Stationery", Description = "Notebook paper" },
                new Category { Id = Guid.NewGuid(), Name = "Electronics", Description = "Devices" }
            };

            var repository = new FakeCategoryRepository(categories);
            var createValidator = new InlineValidator<CategoryCreateDto>();
            var updateValidator = new InlineValidator<CategoryUpdateDto>();

            var service = new CategoryService(repository, createValidator, updateValidator);

            var result = await service.GetPagedCategoriesAsync(1, 10, "Book");
            var items = result.Items.ToList();

            Assert.Equal(2, result.TotalCount);
            Assert.Equal(2, items.Count);
            Assert.Contains(items, item => item.Name == "Books");
            Assert.Contains(items, item => item.Name == "Stationery");
        }
    }
}
