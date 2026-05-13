using System.Linq.Expressions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MidAssignment.Application.Usecase
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IValidator<ProductCreateDto> _createValidator;
        private readonly IValidator<ProductUpdateDto> _updateValidator;
        private readonly IWebHostEnvironment _environment;

        public ProductService(
            IGenericRepository<Product> productRepository,
            IGenericRepository<Category> categoryRepository,
            IValidator<ProductCreateDto> createValidator,
            IValidator<ProductUpdateDto> updateValidator,
            IWebHostEnvironment environment)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _environment = environment;
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

            var images = await SaveImagesAsync(dto.ImageUrls);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                Images = images
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

            if (dto.ImageUrls != null && dto.ImageUrls.Count > 0)
            {
                product.Images.Clear();
                var images = await SaveImagesAsync(dto.ImageUrls);
                foreach (var image in images)
                {
                    product.Images.Add(image);
                }
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

        public async Task DeleteProductImageAsync(Guid productId, string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                throw new KeyNotFoundException("Image not found.");
            }

            var product = await _productRepository.GetByIdWithIncludeAsync(productId, "Images");
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            var image = product.Images.FirstOrDefault(i => i.Url == imageUrl);
            if (image == null)
            {
                throw new KeyNotFoundException("Image not found.");
            }

            product.Images.Remove(image);
            await _productRepository.UpdateAsync(product);

            var uploadsPath = Path.GetFullPath(
                Path.Combine(_environment.ContentRootPath, "..", "MidAssignment.Shared", "Uploads"));
            var fileName = Path.GetFileName(image.Url);
            var filePath = Path.Combine(uploadsPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdWithIncludeAsync(id, "Category", "Images");
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? string.Empty,
                ImageUrls = product.Images.Select(i => i.Url).ToList(),
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        public async Task<PagedResultDto<ProductDto>> GetPagedProductsAsync(int pageNumber, int pageSize, string? keyword = null, Guid? categoryId = null)
        {
            keyword = keyword?.Trim();

            Expression<Func<Product, bool>> predicate;

            if (!string.IsNullOrWhiteSpace(keyword) && categoryId.HasValue && categoryId.Value != Guid.Empty)
            {
                var searchTerm = keyword.ToLower();
                predicate = product =>
                    (product.Name.ToLower().Contains(searchTerm) ||
                    product.Description.ToLower().Contains(searchTerm) ||
                    product.Category.Name.ToLower().Contains(searchTerm)) &&
                    product.CategoryId == categoryId.Value;
            }
            else if (!string.IsNullOrWhiteSpace(keyword))
            {
                var searchTerm = keyword.ToLower();
                predicate = product =>
                    product.Name.ToLower().Contains(searchTerm) ||
                    product.Description.ToLower().Contains(searchTerm) ||
                    product.Category.Name.ToLower().Contains(searchTerm);
            }
            else if (categoryId.HasValue && categoryId.Value != Guid.Empty)
            {
                predicate = product => product.CategoryId == categoryId.Value;
            }
            else
            {
                predicate = product => true;
            }

            var (items, totalCount) = await _productRepository.GetPagedWithIncludeAsync(pageNumber, pageSize, predicate, "Category", "Images");

            var productDtos = items.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? string.Empty,
                ImageUrls = p.Images.Select(i => i.Url).ToList(),
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            });

            return new PagedResultDto<ProductDto>
            {
                Items = productDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        private async Task<List<ProductImage>> SaveImagesAsync(IEnumerable<IFormFile> files)
        {
            var result = new List<ProductImage>();
            if (files == null)
            {
                return result;
            }

            var uploadsPath = Path.GetFullPath(
                Path.Combine(_environment.ContentRootPath, "..", "MidAssignment.Shared", "Uploads"));
            Directory.CreateDirectory(uploadsPath);

            foreach (var file in files)
            {
                    if (file == null || file.Length == 0)
                    {
                        continue;
                    }

                var extension = Path.GetExtension(file.FileName);
                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsPath, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                result.Add(new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = $"/uploads/{fileName}"
                });
            }

            return result;
        }
    }
}
