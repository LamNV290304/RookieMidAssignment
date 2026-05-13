using System;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Application.Usecase;
using MidAssignment.Domain.Entities;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.CategoryServices
{
    public class UpdateCategoryTest
    {
        [Fact]
        public async Task UpdateCategoryAsync_WhenValidationFails_ThrowsValidationException()
        {
            // Arrange
            var repository = new FakeCategoryRepository(new[]
            {
                new Category { Id = Guid.NewGuid(), Name = "Old", Description = "Old" }
            });

            var createValidator = new InlineValidator<CategoryCreateDto>();
            var updateValidator = new InlineValidator<CategoryUpdateDto>();
            updateValidator.RuleFor(x => x.Name).NotEmpty();

            var service = new CategoryService(repository, createValidator, updateValidator);

            var dto = new CategoryUpdateDto
            {
                Name = string.Empty,
                Description = "Invalid"
            };

            // Act
            await Assert.ThrowsAsync<ValidationException>(() =>
                service.UpdateCategoryAsync(repository.Items[0].Id, dto));

            // Assert
            Assert.Empty(repository.UpdatedEntities);
        }

        [Fact]
        public async Task UpdateCategoryAsync_WhenNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var repository = new FakeCategoryRepository();
            var createValidator = new InlineValidator<CategoryCreateDto>();
            var updateValidator = new InlineValidator<CategoryUpdateDto>();
            updateValidator.RuleFor(x => x.Name).NotEmpty();

            var service = new CategoryService(repository, createValidator, updateValidator);

            var dto = new CategoryUpdateDto
            {
                Name = "Updated",
                Description = "Updated"
            };

            // Act + Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                service.UpdateCategoryAsync(Guid.NewGuid(), dto));
        }

        [Fact]
        public async Task UpdateCategoryAsync_WhenValid_UpdatesCategory()
        {
            // Arrange
            var id = Guid.NewGuid();
            var repository = new FakeCategoryRepository(new[]
            {
                new Category { Id = id, Name = "Old", Description = "Old" }
            });

            var createValidator = new InlineValidator<CategoryCreateDto>();
            var updateValidator = new InlineValidator<CategoryUpdateDto>();
            updateValidator.RuleFor(x => x.Name).NotEmpty();

            var service = new CategoryService(repository, createValidator, updateValidator);

            var dto = new CategoryUpdateDto
            {
                Name = "New",
                Description = "New description"
            };

            // Act
            await service.UpdateCategoryAsync(id, dto);

            // Assert
            var updated = Assert.Single(repository.UpdatedEntities);
            Assert.Equal(id, updated.Id);
            Assert.Equal(dto.Name, updated.Name);
            Assert.Equal(dto.Description, updated.Description);

            var stored = Assert.Single(repository.Items);
            Assert.Equal(dto.Name, stored.Name);
            Assert.Equal(dto.Description, stored.Description);
        }
    }
}
