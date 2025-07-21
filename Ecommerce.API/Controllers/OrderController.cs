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

        public OrderController(IOrderServices orderService)
        {
            _orderService = orderService;
        }

        // POST: api/order
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderDto)
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

        // GET: api/order/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound(ApiResponse<OrderDto>.FailResponse("Order not found"));

            return Ok(ApiResponse<OrderDto>.SuccessResponse(order));
        }

        // DELETE: api/order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return Ok(ApiResponse<string>.SuccessResponse("Order deleted successfully"));
        }
    }
}
