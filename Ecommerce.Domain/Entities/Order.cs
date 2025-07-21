using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Entities
{
    public class Order
    {


        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalAmount { get; set; }
        public Status Statuses { get; set; } // e.g., Pending, Shipped, Delivered, Cancelled

        [ForeignKey("Customer")]    
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public Address ShippingAddress { get; set; } // Address where the order will be shipped
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public Order() { }

        public Order(int customerId, Address shippingAddress)
        {
            this.CustomerId = customerId;
            this.ShippingAddress = shippingAddress;
            this.OrderDate = DateTime.Now;
            this.Statuses = Status.Pending; // Default status
        }



        public void AddProductItem(Product product, int quantity)
        {
            if (quantity <=0)
            {
               throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            }
            var existingProduct= this.OrderItems.FirstOrDefault(i => i.ProductId == product.Id);
            if (existingProduct == null)
            {
                this.OrderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = quantity,
                    UnitPrice = product.Price,
                    Order = this

                });
            }
            else
            {   existingProduct.Quantity += quantity; // Update quantity if product already exists in the order
            }
            CalculateTotalAmount();
        }

        public void RemoveProductItem(int productId)
        {
            var item = this.OrderItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                this.OrderItems.Remove(item);
            }
            else
            {
                throw new ArgumentException("Product not found in the order.", nameof(productId));
            }
            CalculateTotalAmount();
        }   

        public void CalculateTotalAmount()
        {
            this.TotalAmount = this.OrderItems.Sum(item => item.UnitPrice * item.Quantity);
        }

    }

    
    public enum Status
    {
        Pending,
        Shipped,
        Delivered,
        Cancelled
    }


}
