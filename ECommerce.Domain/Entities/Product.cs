namespace ECommerce.Domain.Entities
{
    public class Product
    {
        public Guid ProductID { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public Guid CategoryID { get; set; }
        public Category? Category { get; set; }
    }
}
