namespace ECommerce.Domain.Entities
{
    public class Category
    {
        public Guid CategoryID { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Product>? Products { get; set; }
    }
}
