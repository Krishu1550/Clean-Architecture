using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public Status Status { get; set; }

        public CustomerDto Customer { get; set; } // Optional: Light customer info

        public AddressDto ShippingAddress { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}
