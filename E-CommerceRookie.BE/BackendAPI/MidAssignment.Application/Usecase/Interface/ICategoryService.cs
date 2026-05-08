using System;
using System.Threading.Tasks;

namespace MidAssignment.Application.Usecase.Interface
{
    public interface ICategoryService
    {
        Task<Guid> CreateCategoryAsync(CategoryCreateDto dto);
        Task UpdateCategoryAsync(Guid id, CategoryUpdateDto dto);
        Task DeleteCategoryAsync(Guid id);
        Task<PagedResultDto<CategoryDto>> GetPagedCategoriesAsync(int pageNumber, int pageSize);
    }
}
