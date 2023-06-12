using Microsoft.AspNetCore.OData.Query;
using System.Linq.Expressions;
using WordsAPI.Core.Models;

namespace WordsAPI.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id, ODataQueryOptions<User> queryOptions = null);
        IQueryable<T> GetAll(ODataQueryOptions<User> queryOptions = null);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(List<T> entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
