using Ecommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.IServices
{
    public interface IProductServices
    {
        Task<ProductDto> AddProductAsync(ProductCreateDto dto);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task UpdateProductAsync(ProductUpdateDto dto);
        Task DeleteProductAsync(int id);
    }
}
