using Microsoft.AspNetCore.Mvc;
using MidAssignment.UI.Models;
using MidAssignment.UI.Services;

namespace MidAssignment.UI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductApiService _productApiService;
        private readonly ICategoryApiService _categoryApiService;

        public ProductsController(IProductApiService productApiService, ICategoryApiService categoryApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index(Guid? categoryId = null, string? keyword = null, int pageNumber = 1, int pageSize = 12)
        {
            var categories = await _categoryApiService.GetCategoriesAsync();
            var pagedProducts = await _productApiService.GetProductsAsync(pageNumber, pageSize, categoryId, keyword);
            var featuredProducts = await _productApiService.GetFeaturedProductsAsync();

            var model = new ProductListViewModel
            {
                Categories = categories,
                Products = pagedProducts.Items.ToList(),
                FeaturedProducts = featuredProducts,
                SelectedCategoryId = categoryId,
                Keyword = keyword,
                PageNumber = pagedProducts.PageNumber,
                PageSize = pagedProducts.PageSize,
                TotalCount = pagedProducts.TotalCount
            };

            return View(model);
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var product = await _productApiService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var model = new ProductDetailViewModel
            {
                Product = product
            };

            return View(model);
        }
    }
}
