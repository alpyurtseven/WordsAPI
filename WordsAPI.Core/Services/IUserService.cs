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
        Task<CustomResponseDto<UserDTO>> GetUserByUserNameAsync(string username);
        Task<CustomResponseDto<UserDTO>> GetUserByEmailAsync(UserLoginDTO user);
    }
}
