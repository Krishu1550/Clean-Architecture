using Ecommerce.API.Dto;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.IServices;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productService;

        public ProductController(IProductServices productService)
        {
            _productService = productService;
        }

        // GET: api/product
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(ApiResponse<IEnumerable<ProductDto>>.SuccessResponse(products));
        }

        // GET: api/product/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound(ApiResponse<ProductDto>.FailResponse("Product not found"));
            return Ok(ApiResponse<ProductDto>.SuccessResponse(product));
        }

        // POST: api/product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<ProductDto>.FailResponse("Validation failed", errors));
            }
            var createdProduct = await _productService.AddProductAsync(productDto);
            return Ok(ApiResponse<ProductDto>.SuccessResponse(createdProduct, "Product created successfully"));
        }

        // PUT: api/product
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto productDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<string>.FailResponse("Validation failed", errors));
            }
            await _productService.UpdateProductAsync(productDto);
            return Ok(ApiResponse<string>.SuccessResponse("Product updated successfully"));
        }

        // DELETE: api/product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok(ApiResponse<string>.SuccessResponse("Product deleted successfully"));
        }
    }
}
