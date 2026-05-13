using MidAssignment.Shared.DTOs;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System.Text.Json;

namespace MidAssignment.UI.Services
{
    public class CategoryApiService : ICategoryApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync()
        {
            var query = new Dictionary<string, string?>
            {
                ["pageNumber"] = "1",
                ["pageSize"] = "20"
            };

            var url = QueryHelpers.AddQueryString("api/Categories", query);
            var response = await _httpClient.GetFromJsonAsync<PagedResultDto<CategoryDto>>(url, _jsonOptions);

            return response?.Items?.ToList() ?? new List<CategoryDto>();
        }

        public Task<CategoryDto?> GetCategoryByIdAsync(Guid id)
        {
            return _httpClient.GetFromJsonAsync<CategoryDto>($"api/Categories/{id}", _jsonOptions);
        }
    }
}
