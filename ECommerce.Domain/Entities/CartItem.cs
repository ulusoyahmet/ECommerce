namespace ECommerce.Domain.Entities
{
    public class CartItem
    {
        public Guid CartItemID { get; set; }
        public Guid CartID { get; set; }
        public Cart? Cart { get; set; } 
        public Guid ProductID { get; set; }
        public Product? Product { get; set; } 
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
