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
         Task<User> GetUserByEmailAsync(string email);
         Task<User> GetUserByUserNameAsync(string username);
         Task<bool> CheckPasswordAsync(User user, string password);
    }
}
