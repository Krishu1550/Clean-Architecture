using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.IServices;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepository;

public class OrderService : IOrderServices
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto> AddOrderAsync(OrderCreateDto dto)
    {
        // Validate customer
        var customer = await _customerRepository.GetCustomerByIdAsync(dto.CustomerId);
        if (customer == null)
            throw new ArgumentException("Invalid customer ID.", nameof(dto.CustomerId));

        // Map shipping address
        var shippingAddress = _mapper.Map<Address>(dto.ShippingAddress);

        // Create order
        var order = new Order(dto.CustomerId, shippingAddress);

        // Add order items
        foreach (var itemDto in dto.OrderItems)
        {
            var product = await _productRepository.GetProductByIdAsync(itemDto.ProductId);
            if (product == null)
                throw new ArgumentException($"Product not found (ID: {itemDto.ProductId})");

            order.AddProductItem(product, itemDto.Quantity);
        }

        await _orderRepository.AddOrderAsync(order);

        return _mapper.Map<OrderDto>(order);
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetOrderByIdAsync(id);
        return order == null ? null : _mapper.Map<OrderDto>(order);
    }

    public async Task UpdateOrderAsync(OrderUpdateDto dto)
    {
        var order = await _orderRepository.GetOrderByIdAsync(dto.Id);
        if (order == null)
            throw new ArgumentException("Order not found.", nameof(dto.Id));

       

        // Update shipping address
        if (dto.ShippingAddress is not null)
        {
            order.ShippingAddress = _mapper.Map<Address>(dto.ShippingAddress);
        }

        // Optional: Replace all order items (only if dto.OrderItems is provided)
        if (dto.OrderItems != null && dto.OrderItems.Count > 0)
        {
            order.OrderItems.Clear();
            foreach (var itemDto in dto.OrderItems)
            {
                var product = await _productRepository.GetProductByIdAsync(itemDto.ProductId);
                if (product == null)
                    throw new ArgumentException($"Product not found (ID: {itemDto.ProductId})");

                order.AddProductItem(product, itemDto.Quantity);
            }
        }

        await _orderRepository.AddOrderAsync(order); // or update if you refactor
    }

    public async Task DeleteOrderAsync(int id)
    {
        await _orderRepository.DeleteOrderAsync(id);
    }
}
