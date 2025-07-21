using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class OrderItem
    {
        [Key] // Assuming you are using Entity Framework or similar ORM 
        public int Id { get; set; } 
        public int ProductId { get; set; }

        public string ProductName { get; set; } // Optional, can be used for display purposes

        public int OrderId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal UnitPrice { get; set; } // Price at the time of order
        public Order Order { get; set; }
        public Product Product { get; set; }


        public OrderItem(int productId,int quantity, decimal unitPrice, string productName, Order order)
        {
            ProductId = productId;
            Order= order;
            OrderId = order.Id;
            Quantity = quantity;
            UnitPrice = unitPrice;
            ProductName = productName;  
        }  
        public OrderItem()
        {
            // Parameterless constructor for ORM compatibility
        }
    }

}
