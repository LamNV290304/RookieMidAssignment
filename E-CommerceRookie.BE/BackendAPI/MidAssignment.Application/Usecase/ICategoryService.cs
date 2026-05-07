using System;
using System.Threading.Tasks;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.Application.Usecase
{
    public interface ICategoryService
    {
        Task<Guid> CreateCategoryAsync(CategoryCreateDto dto);
    }
}
