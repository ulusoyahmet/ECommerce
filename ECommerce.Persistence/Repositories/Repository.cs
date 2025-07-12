using System.Linq.Expressions;
using ECommerce.Application.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ECommerce.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public Repository(IMongoDatabase database)
        {
            var collectionName = typeof(T).Name.ToLower() + "s";
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return null;

            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null)
            {
                var id = idProperty.GetValue(entity)?.ToString();
                if (!string.IsNullOrEmpty(id) && ObjectId.TryParse(id, out _))
                {
                    var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
                    await _collection.ReplaceOneAsync(filter, entity);
                }
            }
        }

        public async Task DeleteAsync(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return;

            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            await _collection.DeleteOneAsync(filter);
        }

        public async Task<long> GetCountAsync()
        {
            return await _collection.CountDocumentsAsync(_ => true);
        }

        public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetByFilterListAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return false;

            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _collection.Find(filter).AnyAsync();
        }

        public async Task<List<T>> GetPagedAsync(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            return await _collection.Find(_ => true)
                .Skip(skip)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<List<T>> GetPagedAsync(int page, int pageSize, Expression<Func<T, bool>>? filter = null)
        {
            var skip = (page - 1) * pageSize;
            var mongoFilter = filter ?? (_ => true);
            
            return await _collection.Find(mongoFilter)
                .Skip(skip)
                .Limit(pageSize)
                .ToListAsync();
        }
    }
}
