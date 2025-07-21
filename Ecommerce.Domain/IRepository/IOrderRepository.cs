using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.IRepository
{
    public interface IOrderRepository
    {

        Task<Order> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);

        Task DeleteOrderAsync(int orderId);

    }
}
