using Ecommerce.API.Dto;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderServices orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        // POST: api/order
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(ApiResponse<OrderDto>.FailResponse("Validation failed", errors));
                }

                var createdOrder = await _orderService.AddOrderAsync(orderDto);
                return Ok(ApiResponse<OrderDto>.SuccessResponse(createdOrder, "Order created successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order");
                return StatusCode(500, ApiResponse<OrderDto>.FailResponse("An error occurred while creating the order."));
            }
        }

        // GET: api/order/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                _logger.LogInformation("Retrieving Order with ID {Id}.", id);
                if (order == null)
                {
                    _logger.LogWarning($"Order with ID {id} not found.");
                    return NotFound(ApiResponse<OrderDto>.FailResponse("Order not found"));
                }

                return Ok(ApiResponse<OrderDto>.SuccessResponse(order));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving order with ID {id}");
                return StatusCode(500, ApiResponse<OrderDto>.FailResponse("An error occurred while retrieving the order."));
            }
        }

  
    

        // DELETE: api/order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var existingOrder = await _orderService.GetOrderByIdAsync(id);
                if (existingOrder == null)
                    return NotFound(ApiResponse<string>.FailResponse("Order not found"));

                await _orderService.DeleteOrderAsync(id);
                return Ok(ApiResponse<string>.SuccessResponse("Order deleted successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting order with ID {id}");
                return StatusCode(500, ApiResponse<string>.FailResponse("An error occurred while deleting the order."));
            }
        }
    }
}
