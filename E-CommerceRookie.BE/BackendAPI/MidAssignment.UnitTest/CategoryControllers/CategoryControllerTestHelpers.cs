using System;
using System.Threading.Tasks;
using MidAssignment.Application.Usecase.Interface;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.CategoryControllers
{
    internal sealed class FakeCategoryService : ICategoryService
    {
        public Guid CreateResult { get; set; } = Guid.NewGuid();
        public CategoryCreateDto? LastCreateDto { get; private set; }
        public (Guid Id, CategoryUpdateDto Dto)? LastUpdate { get; private set; }
        public Guid? LastDeleteId { get; private set; }
        public Guid? LastGetById { get; private set; }
        public (int PageNumber, int PageSize, string? Keyword)? LastGetPaged { get; private set; }

        public CategoryDto CategoryToReturn { get; set; } = new CategoryDto();
        public PagedResultDto<CategoryDto> PagedResultToReturn { get; set; } = new PagedResultDto<CategoryDto>();

        public Task<Guid> CreateCategoryAsync(CategoryCreateDto dto)
        {
            LastCreateDto = dto;
            return Task.FromResult(CreateResult);
        }

        public Task UpdateCategoryAsync(Guid id, CategoryUpdateDto dto)
        {
            LastUpdate = (id, dto);
            return Task.CompletedTask;
        }

        public Task DeleteCategoryAsync(Guid id)
        {
            LastDeleteId = id;
            return Task.CompletedTask;
        }

        public Task<CategoryDto> GetCategoryByIdAsync(Guid id)
        {
            LastGetById = id;
            return Task.FromResult(CategoryToReturn);
        }

        public Task<PagedResultDto<CategoryDto>> GetPagedCategoriesAsync(int pageNumber, int pageSize, string? keyword = null)
        {
            LastGetPaged = (pageNumber, pageSize, keyword);
            return Task.FromResult(PagedResultToReturn);
        }
    }
}
