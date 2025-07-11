using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Domain.Entities
{
    public class CartItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CartItemID { get; set; }

        [BsonElement("cartId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CartID { get; set; }

        [BsonIgnore]
        public Cart? Cart { get; set; }

        [BsonElement("productId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ProductID { get; set; }

        [BsonIgnore]
        public Product? Product { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
