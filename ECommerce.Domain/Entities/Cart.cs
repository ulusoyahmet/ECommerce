using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Domain.Entities
{
    public class Cart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("items")]
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        // ignore

        [BsonIgnore]
        public decimal TotalAmount
        {
            get
            {
                return Items.Sum(item => item.Quantity * item.Price);
            }
        }

        [BsonIgnore]
        public int TotalItems
        {
            get
            {
                return Items.Sum(item => item.Quantity);
            }
        }
    }
}
