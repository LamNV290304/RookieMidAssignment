using MidAssignment.Shared.DTOs;

namespace MidAssignment.UI.Services
{
    public interface ICategoryApiService
    {
        Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto?> GetCategoryByIdAsync(Guid id);
    }
}
