using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.IServices;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepository;

public class ProductService : IProductServices
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto> AddProductAsync(ProductCreateDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        var savedProduct = await _productRepository.AddProductAsync(product);
        return _mapper.Map<ProductDto>(savedProduct);
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProductAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        return product == null ? null : _mapper.Map<ProductDto>(product);
    }

    public async Task UpdateProductAsync(ProductUpdateDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        await _productRepository.UpdateProductAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteProductAsync(id);
    }
}
