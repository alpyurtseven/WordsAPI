using Microsoft.AspNetCore.OData.Query;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;

namespace WordsAPI.Core.Services
{
    public interface IUserService : IService<User>
    {
        Task<CustomResponseDto<UserDTO>> CreateUserAsync(UserRegisterDTO user);
        Task<CustomResponseDto<UserDTO>> GetUserByUserNameAsync(string username, ODataQueryOptions<User> queryOptions = null);
        Task<CustomResponseDto<UserDTO>> GetAllUsers(ODataQueryOptions<User> queryOptions);
        Task<CustomResponseDto<UserDTO>> GetUserByEmailAsync(UserLoginDTO user, ODataQueryOptions<User> queryOptions = null);
        Task<CustomResponseDto<bool>> AddWordToUserVocabulary(string username, string word);
        Task<CustomResponseDto<List<WordDTO>>> GetUserVocabulary(string username);
        Task<CustomResponseDto<User>> GetUserById(string id);
    }
}
