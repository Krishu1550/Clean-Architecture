using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.DTOs
{
    public class AddressDto
    {

        [Range(1000, 10000)]
        public int PostalCode { get; set; }

        public string? Street { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        public string? State { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
    }
}
