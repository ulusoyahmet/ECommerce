using MongoDB.Driver;

namespace ECommerce.Application.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoDatabase Database => _database;
    }
} 