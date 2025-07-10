namespace ECommerce.Domain.Entities
{
    public class Cart
    {
        public Guid CartID { get; set; }
        public Guid UserID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
