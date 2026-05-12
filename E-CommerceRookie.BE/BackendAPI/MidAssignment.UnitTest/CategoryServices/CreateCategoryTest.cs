using System;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Application.Usecase;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.CategoryServices
{
    public class CreateCategoryAsyncTest
    {
        [Fact]
        public async Task CreateCategoryAsync_WhenValidationFails_ThrowsValidationException()
        {
            var repository = new FakeCategoryRepository();
            var createValidator = new InlineValidator<CategoryCreateDto>();
            createValidator.RuleFor(x => x.Name).NotEmpty();
            var updateValidator = new InlineValidator<CategoryUpdateDto>();

            var service = new CategoryService(repository, createValidator, updateValidator);

            var dto = new CategoryCreateDto
            {
                Name = string.Empty,
                Description = "Invalid"
            };

            await Assert.ThrowsAsync<ValidationException>(() => service.CreateCategoryAsync(dto));
        }

        [Fact]
        public async Task CreateCategoryAsync_WhenValid_AddsCategoryAndReturnsId()
        {
            var repository = new FakeCategoryRepository();
            var createValidator = new InlineValidator<CategoryCreateDto>();
            createValidator.RuleFor(x => x.Name).NotEmpty();
            var updateValidator = new InlineValidator<CategoryUpdateDto>();

            var service = new CategoryService(repository, createValidator, updateValidator);

            var dto = new CategoryCreateDto
            {
                Name = "Books",
                Description = "All book categories"
            };

            var id = await service.CreateCategoryAsync(dto);

            var added = Assert.Single(repository.AddedEntities);
            Assert.NotEqual(Guid.Empty, id);
            Assert.Equal(id, added.Id);
            Assert.Equal(dto.Name, added.Name);
            Assert.Equal(dto.Description, added.Description);
        }
    }
}
