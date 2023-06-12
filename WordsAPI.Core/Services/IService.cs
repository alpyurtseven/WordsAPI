using Microsoft.AspNetCore.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WordsAPI.Core.Models;

namespace WordsAPI.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id, ODataQueryOptions<User> queryOptions = null);
        Task<IQueryable<T>> GetAllAsync(ODataQueryOptions<User> queryOptions = null);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(List<T> entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
