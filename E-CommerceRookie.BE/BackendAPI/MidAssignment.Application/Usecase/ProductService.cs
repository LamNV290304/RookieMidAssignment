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
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IValidator<ProductCreateDto> _createValidator;
        private readonly IValidator<ProductUpdateDto> _updateValidator;

        public ProductService(
            IGenericRepository<Product> productRepository,
            IGenericRepository<Category> categoryRepository,
            IValidator<ProductCreateDto> createValidator,
            IValidator<ProductUpdateDto> updateValidator)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<Guid> CreateProductAsync(ProductCreateDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
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

        public async Task UpdateProductAsync(Guid id, ProductUpdateDto dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var product = await _productRepository.GetByIdWithIncludeAsync(id, "Images");
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;

            // Simple image replacement: clear old and add new
            product.Images.Clear();
            foreach (var url in dto.ImageUrls)
            {
                product.Images.Add(new ProductImage { Id = Guid.NewGuid(), Url = url });
            }

            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }
            await _productRepository.DeleteAsync(product);
        }
    }
}
