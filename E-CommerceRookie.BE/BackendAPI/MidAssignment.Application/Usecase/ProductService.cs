using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Domain.Entities;
using MidAssignment.Domain.Interfaces;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.Application.Usecase
{
    public interface IProductService
    {
        Task<Guid> CreateProductAsync(ProductCreateDto dto);
    }

    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IValidator<ProductCreateDto> _validator;

        public ProductService(
            IGenericRepository<Product> productRepository,
            IGenericRepository<Category> categoryRepository,
            IValidator<ProductCreateDto> validator)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _validator = validator;
        }

        public async Task<Guid> CreateProductAsync(ProductCreateDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                Images = dto.ImageUrls.Select(url => new ProductImage 
                { 
                    Id = Guid.NewGuid(), 
                    Url = url 
                }).ToList()
            };

            await _productRepository.AddAsync(product);
            return product.Id;
        }
    }
}
