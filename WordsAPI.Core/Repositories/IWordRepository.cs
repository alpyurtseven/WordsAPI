using Microsoft.AspNetCore.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAPI.Core.Models;

namespace WordsAPI.Core.Repositories
{
    public interface IWordRepository<T> : IGenericRepository<T> where T : AWord
    {
        Task<List<T>> GetWordsWithRelations(ODataQueryOptions<T> queryOptions = null);
        Task<T> GetWordWithRelations(int id, ODataQueryOptions<T> queryOptions = null);
    }
}
