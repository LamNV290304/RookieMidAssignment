using MidAssignment.Application.Usecase.Interface;

namespace MidAssignment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? keyword = null, [FromQuery] Guid? categoryId = null)
        {
            var result = await _productService.GetPagedProductsAsync(pageNumber, pageSize, keyword, categoryId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto dto)
        {
            var id = await _productService.CreateProductAsync(dto);
            return CreatedAtAction("Create", new { id }, id);
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(Guid id, [FromForm] ProductUpdateDto dto)
        {
            await _productService.UpdateProductAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}/images")]
        public async Task<IActionResult> DeleteImage(Guid id, [FromQuery] string url)
        {
            await _productService.DeleteProductImageAsync(id, url);
            return NoContent();
        }
    }
}
