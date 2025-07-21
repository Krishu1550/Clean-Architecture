using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.MappigProfile
{
    public class MapperProfile : Profile
    {


        public MapperProfile()
        {
            // Add your mapping configurations here
            // For example:
            // CreateMap<SourceModel, DestinationModel>();
            // Customer
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<CustomerUpdateDto, Customer>();
            CreateMap<Customer, CustomerDto>();

            // Address
            CreateMap<AddressDto, Address>().ReverseMap();

            // Product
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductDto>();

            // Order
            CreateMap<OrderCreateDto, Order>();
            CreateMap<OrderUpdateDto, Order>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();
           // CreateMap<Customer, CustomerDto>(); ;


        }

        // You can add more mapping configurations as needed
    }
}
