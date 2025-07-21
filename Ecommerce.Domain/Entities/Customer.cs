using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string PhoneNumber { get; set; }

        public Address Address { get; set; } // Customer's address  



        public Customer(string name, string email,string phoneNumber ,Address  address)
        {
            // Parameterless constructor for ORM compatibility

            Name = name;
            Email = email ;
            PhoneNumber = phoneNumber;
            Address = address;

        }

        public Customer()
        {
            // Parameterless constructor for ORM compatibility
        }   
    }
}
