using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.IRepository
{
    public interface IProductRepository
    {

        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product> AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);

    }
}
