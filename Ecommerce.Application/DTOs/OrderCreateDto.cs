using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.DTOs
{
    public class OrderCreateDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public AddressDto ShippingAddress { get; set; }

        [Required]
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
