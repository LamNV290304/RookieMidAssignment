using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Application.Usecase.Interface;
using MidAssignment.Domain.Entities;
using MidAssignment.Domain.Interfaces;

namespace MidAssignment.Application.Usecase
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IValidator<CategoryCreateDto> _createValidator;
        private readonly IValidator<CategoryUpdateDto> _updateValidator;

        public CategoryService(
            IGenericRepository<Category> repository, 
            IValidator<CategoryCreateDto> createValidator,
            IValidator<CategoryUpdateDto> updateValidator)
        {
            _repository = repository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<Guid> CreateCategoryAsync(CategoryCreateDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description
            };

            await _repository.AddAsync(category);
            return category.Id;
        }

        public async Task UpdateCategoryAsync(Guid id, CategoryUpdateDto dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            category.Name = dto.Name;
            category.Description = dto.Description;

            await _repository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            await _repository.DeleteAsync(category);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<PagedResultDto<CategoryDto>> GetPagedCategoriesAsync(int pageNumber, int pageSize, string? keyword = null)
        {
            keyword = keyword?.Trim();

            Expression<Func<Category, bool>>? predicate = null;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var searchTerm = keyword.ToLower();
                predicate = category =>
                    category.Name.ToLower().Contains(searchTerm) ||
                    category.Description.ToLower().Contains(searchTerm);
            }

            var (items, totalCount) = await _repository.GetPagedAsync(pageNumber, pageSize, predicate);

            var categoryDtos = items.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            });

            return new PagedResultDto<CategoryDto>
            {
                Items = categoryDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
