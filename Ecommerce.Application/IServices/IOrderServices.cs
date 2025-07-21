using Ecommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.IServices
{
    public interface IOrderServices
    {
        /// <summary>
        /// Places a new order for a customer.
        /// </summary>
        /// <param name="dto">Order creation data.</param>
        /// <returns>The created order as a response DTO.</returns>
        Task<OrderDto> AddOrderAsync(OrderCreateDto dto);

        /// <summary>
        /// Retrieves an order by its ID, including items and product info.
        /// </summary>
        /// <param name="id">Order ID.</param>
        /// <returns>The order if found, or null.</returns>
        Task<OrderDto?> GetOrderByIdAsync(int id);

        /// <summary>
        /// Updates the order status, shipping address, or order items.
        /// </summary>
        /// <param name="dto">Order update data.</param>
        Task UpdateOrderAsync(OrderUpdateDto dto);

        /// <summary>
        /// Deletes an order by its ID.
        /// </summary>
        /// <param name="id">Order ID.</param>
        Task DeleteOrderAsync(int id);

    }
}
