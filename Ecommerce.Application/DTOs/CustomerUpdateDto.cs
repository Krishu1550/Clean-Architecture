using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.DTOs
{
    public class CustomerUpdateDto: CustomerCreateDto
    {
        [Required]
        public int Id { get; set; }
    }
}
