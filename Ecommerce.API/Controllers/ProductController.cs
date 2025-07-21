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
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductServices productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // GET: api/product
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(ApiResponse<IEnumerable<ProductDto>>.SuccessResponse(products));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all products");
                return StatusCode(500, ApiResponse<IEnumerable<ProductDto>>.FailResponse("An error occurred while fetching products."));
            }
        }

        // GET: api/product/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                _logger.LogInformation("Retrieving product with ID {Id}.", id);
                if (product == null)
                {
                    _logger.LogWarning($"Product with ID {id} not found.");
                    return NotFound(ApiResponse<ProductDto>.FailResponse("Product not found"));
                }

                return Ok(ApiResponse<ProductDto>.SuccessResponse(product));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product with ID {id}");
                return StatusCode(500, ApiResponse<ProductDto>.FailResponse("An error occurred while fetching the product."));
            }
        }

        // POST: api/product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productDto)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                return StatusCode(500, ApiResponse<ProductDto>.FailResponse("An error occurred while creating the product."));
            }
        }

        // PUT: api/product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto productDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(ApiResponse<string>.FailResponse("Validation failed", errors));
                }

                var existingProduct = await _productService.GetProductByIdAsync(id);
                if (existingProduct == null)
                    return NotFound(ApiResponse<string>.FailResponse("Product not found"));

                productDto.Id = id; // Ensure ID is set
                await _productService.UpdateProductAsync(productDto);

                return Ok(ApiResponse<string>.SuccessResponse("Product updated successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating product with ID {id}");
                return StatusCode(500, ApiResponse<string>.FailResponse("An error occurred while updating the product."));
            }
        }

        // DELETE: api/product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                    return NotFound(ApiResponse<string>.FailResponse("Product not found"));

                await _productService.DeleteProductAsync(id);
                return Ok(ApiResponse<string>.SuccessResponse("Product deleted successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting product with ID {id}");
                return StatusCode(500, ApiResponse<string>.FailResponse("An error occurred while deleting the product."));
            }
        }
    }
}
