using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepository;
using Ecommerce.InfraStructure.Persitence;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.InfraStructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ECommerceContext _context;

        public ProductRepository(ECommerceContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _context.Products.ToListAsync(); // Assuming you have a DbSet<Product> in your context
        }

        public Task<Product?> GetProductByIdAsync(int id)
        {
            return _context.Products.AsNoTracking().FirstOrDefaultAsync(
                p => p.Id == id
                );// Returns null if not found   
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

    }
}
