using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepository;
using Ecommerce.InfraStructure.Persitence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;


namespace Ecommerce.InfraStructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
      private ECommerceContext _context;
        public CustomerRepository(ECommerceContext context) { 
        
            _context = context;

        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int  customerId)
        {
           var customer=  await _context.Customers.FirstOrDefaultAsync(s => s.Id == customerId);

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return _context.Customers
                .Include(c => c.Address) // Include related Address entity if needed
                .FirstOrDefaultAsync(c => c.Id == customerId);
        }
    }
}
