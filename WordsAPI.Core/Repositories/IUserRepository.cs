using Microsoft.AspNetCore.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;

namespace WordsAPI.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> CreateUserAsync(UserRegisterDTO user);
        Task<User> GetUserByEmailAsync(string email, ODataQueryOptions<User> queryOptions = null);
        IQueryable<User> GetAllUsers(ODataQueryOptions<User> queryOptions = null);
        Task<User> GetUserByUserNameAsync(string username, ODataQueryOptions<User> queryOptions = null);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<bool> AddWordToUserVocabulary(User user, string word);
        Task<List<WordDTO>> GetUserVocabulary(User user);
        Task<User> GetUserById(string id);
    }
}
