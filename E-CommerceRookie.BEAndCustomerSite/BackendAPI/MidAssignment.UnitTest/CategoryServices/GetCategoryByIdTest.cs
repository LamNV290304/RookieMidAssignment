using System;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Application.Usecase;
using MidAssignment.Domain.Entities;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.CategoryServices
{
    public class GetCategoryByIdTest
    {
        [Fact]
        public async Task GetCategoryByIdAsync_WhenNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var repository = new FakeCategoryRepository();
            var createValidator = new InlineValidator<CategoryCreateDto>();
            var updateValidator = new InlineValidator<CategoryUpdateDto>();

            var service = new CategoryService(repository, createValidator, updateValidator);

            // Act + Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                service.GetCategoryByIdAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task GetCategoryByIdAsync_WhenFound_ReturnsDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var repository = new FakeCategoryRepository(new[]
            {
                new Category { Id = id, Name = "Books", Description = "All books" }
            });
            var createValidator = new InlineValidator<CategoryCreateDto>();
            var updateValidator = new InlineValidator<CategoryUpdateDto>();

            var service = new CategoryService(repository, createValidator, updateValidator);

            // Act
            var result = await service.GetCategoryByIdAsync(id);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal("Books", result.Name);
            Assert.Equal("All books", result.Description);
        }
    }
}
