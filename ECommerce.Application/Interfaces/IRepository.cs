using System.Linq.Expressions;

namespace ECommerce.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
        Task<long> GetCountAsync();
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetByFilterListAsync(Expression<Func<T, bool>> filter);
        Task<bool> ExistsAsync(string id);
        Task<List<T>> GetPagedAsync(int page, int pageSize);
        Task<List<T>> GetPagedAsync(int page, int pageSize, Expression<Func<T, bool>>? filter = null);
    }
}
