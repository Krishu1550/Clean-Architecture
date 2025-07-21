using Ecommerce.Application.DTOs;

public interface ICustomerService
{
    /// <summary>
    /// Adds a new customer to the system.
    /// </summary>
    /// <param name="dto">Customer creation data.</param>
    /// <returns>The newly created customer as a DTO.</returns>
    Task<CustomerDto> AddCustomerAsync(CustomerCreateDto dto);

    /// <summary>
    /// Gets a customer by their unique ID.
    /// </summary>
    /// <param name="id">The customer's ID.</param>
    /// <returns>The customer DTO if found, or null.</returns>
    Task<CustomerDto?> GetCustomerByIdAsync(int id);

   
    /// Deletes a customer by ID.
    /// </summary>
    /// <param name="id">The customer ID to delete.</param>
    Task DeleteCustomerAsync(int id);
}
