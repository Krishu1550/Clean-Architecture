using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Domain.Entities
{
    [Owned]
    public class Address
    {

        [Range(1000, 10000)]
        [Required(ErrorMessage = "Pincode is required")]
        public int PostalCode { get; set; }

        public string Street { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } 
        public string? State { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } 
    }
}
