using System;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Application.Usecase;
using MidAssignment.Domain.Entities;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.CategoryServices
{
    public class DeleteCategoryTest
    {
        [Fact]
        public async Task DeleteCategoryAsync_WhenNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var repository = new FakeCategoryRepository();
            var createValidator = new InlineValidator<CategoryCreateDto>();
            var updateValidator = new InlineValidator<CategoryUpdateDto>();

            var service = new CategoryService(repository, createValidator, updateValidator);

            // Act + Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                service.DeleteCategoryAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task DeleteCategoryAsync_WhenFound_RemovesCategory()
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
            await service.DeleteCategoryAsync(id);

            // Assert
            var deleted = Assert.Single(repository.DeletedEntities);
            Assert.Equal(id, deleted.Id);
            Assert.Empty(repository.Items);
        }
    }
}
