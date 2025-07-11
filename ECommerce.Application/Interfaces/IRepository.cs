using System.Linq.Expressions;

namespace ECommerce.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<int> GetCountAsync();
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetAllWithExplicitIncludeAsync(Func<IQueryable<T>, IQueryable<T>> includeFunc);

    }
}
