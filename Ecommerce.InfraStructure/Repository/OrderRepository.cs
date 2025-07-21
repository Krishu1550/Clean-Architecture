using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepository;
using Ecommerce.InfraStructure.Persitence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;


namespace Ecommerce.InfraStructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private ECommerceContext _context;
        public OrderRepository(ECommerceContext context) 
        {
            _context = context;

        }

        public async Task AddOrderAsync(Order order)
        {
            await  _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(s=>s.Id==orderId);
            if (order == null)
            {
                throw new ArgumentException("Order not found.", nameof(orderId));
            }
            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
        }

        public Task<Order> GetOrderByIdAsync(int id)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product) // Assuming OrderItem has a Product navigation property
                .FirstOrDefaultAsync(o => o.Id == id);
        }

    }
  
}
