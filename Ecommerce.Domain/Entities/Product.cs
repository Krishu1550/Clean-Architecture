using System.ComponentModel.DataAnnotations;


namespace Ecommerce.Domain.Entities
{
    public class Product
    {
        [Key] // Assuming you are using Entity Framework or similar ORM 
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value")] 
        public string? Description { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public decimal Price { get; set; } // Price of the product

        [Range(0.01, 100.0, ErrorMessage = "Rating must be between 0.1 and 100.0")]  
        public int StockQuantity { get; set; } // Available stock for the product


        public Product(int id, string name, decimal price, int stock, string? description = null)
        {
            Id = id;
            Name = name;
            Price = price;
            StockQuantity = stock;
            Description = description;
        }

        public Product()
        {
            // Parameterless constructor for ORM compatibility
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            }
            StockQuantity += quantity;
        }

        public void ChangePrice(decimal newPrice)
        {
            if (newPrice <= 0)
            {
                throw new ArgumentException("Price must be a positive value.", nameof(newPrice));
            }
            Price = newPrice;
        }

        public void ReduceStock(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            }
            if (quantity > StockQuantity)
            {
                throw new InvalidOperationException("Insufficient stock available.");
            }
            StockQuantity -= quantity;
        }
    }
}
