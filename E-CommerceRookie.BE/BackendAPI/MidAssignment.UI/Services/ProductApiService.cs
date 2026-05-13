using MidAssignment.Shared.DTOs;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System.Text.Json;

namespace MidAssignment.UI.Services
{
    public class ProductApiService : IProductApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagedResultDto<ProductDto>> GetProductsAsync(int pageNumber = 1, int pageSize = 20, Guid? categoryId = null, string? keyword = null)
        {
            var query = new Dictionary<string, string?>
            {
                ["pageNumber"] = pageNumber.ToString(),
                ["pageSize"] = pageSize.ToString()
            };

            if (categoryId.HasValue)
            {
                query["categoryId"] = categoryId.Value.ToString();
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query["keyword"] = keyword.Trim();
            }

            var url = QueryHelpers.AddQueryString("api/Products", query);
            var response = await _httpClient.GetFromJsonAsync<PagedResultDto<ProductDto>>(url, _jsonOptions);

            return response ?? new PagedResultDto<ProductDto>();
        }

        public async Task<IReadOnlyList<ProductDto>> GetFeaturedProductsAsync()
        {
            var result = await GetProductsAsync(pageNumber: 1, pageSize: 4);
            return result.Items
                .OrderByDescending(item => item.CreatedAt)
                .Take(4)
                .ToList();
        }

        public Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            return _httpClient.GetFromJsonAsync<ProductDto>($"api/Products/{id}", _jsonOptions);
        }
    }
}
