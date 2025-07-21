using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.IServices;
using Ecommerce.API.Dto;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // POST: api/customer
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(ApiResponse<CustomerDto>.FailResponse("Validation failed", errors));
            }

            var createdCustomer = await _customerService.AddCustomerAsync(customerDto);
            return Ok(ApiResponse<CustomerDto>.SuccessResponse(createdCustomer, "Customer created successfully."));
        }

        // GET: api/customer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound(ApiResponse<CustomerDto>.FailResponse("Customer not found"));

            return Ok(ApiResponse<CustomerDto>.SuccessResponse(customer));
        }


        // DELETE: api/customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return Ok(ApiResponse<string>.SuccessResponse("Customer deleted successfully"));
        }
    }
}
