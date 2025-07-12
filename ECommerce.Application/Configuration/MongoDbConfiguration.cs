namespace ECommerce.Application.Configuration
{
    public class MongoDbConfiguration
    {
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";
        public string DatabaseName { get; set; } = "ECommerceDb";
    }
} 