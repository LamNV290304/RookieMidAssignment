using System;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Application.Usecase;
using MidAssignment.Domain.Entities;
using MidAssignment.Domain.Interfaces;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.Application.Usecase
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IValidator<CategoryCreateDto> _validator;

        public CategoryService(IGenericRepository<Category> repository, IValidator<CategoryCreateDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Guid> CreateCategoryAsync(CategoryCreateDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
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
    }
}
