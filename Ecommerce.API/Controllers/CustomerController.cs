using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.DTOs;
using Ecommerce.API.Dto;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerService customerService , ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;   
        }

        // POST: api/customer
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateDto customerDto)
        {
            try
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
           catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                _logger.LogError(ex, "An error occurred while creating a customer.");
                return StatusCode(500, ApiResponse<CustomerDto>.FailResponse("An unexpected error occurred while creating the customer."));

            }
        }

        // GET: api/customer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try 
            { 
                var customer = await _customerService.GetCustomerByIdAsync(id);
                _logger.LogInformation("Retrieving customer with ID {Id}.", id);
                 if (!ModelState.IsValid)
                 {
                     var errors = ModelState.Values
                         .SelectMany(v => v.Errors)
                         .Select(e => e.ErrorMessage)
                         .ToList();
                     return BadRequest(ApiResponse<CustomerDto>.FailResponse("Validation failed", errors));
                }
                if (customer == null)
                    return NotFound(ApiResponse<CustomerDto>.FailResponse("Customer not found"));

                 return Ok(ApiResponse<CustomerDto>.SuccessResponse(customer));
            }
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while retrieving the customer with ID {Id}.", id);
                return StatusCode(500, ApiResponse<CustomerDto>.FailResponse("An unexpected error occurred while retrieving the customer."));
            }
        }


        // DELETE: api/customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _customerService.DeleteCustomerAsync(id);
                return Ok(ApiResponse<string>.SuccessResponse("Customer deleted successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the customer with ID {Id}.", id);
                return StatusCode(500, ApiResponse<string>.FailResponse("An unexpected error occurred while deleting the customer."));
            }
        }
    }
}
