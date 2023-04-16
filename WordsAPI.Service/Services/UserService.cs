using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;
using WordsAPI.Core.Repositories;
using WordsAPI.Core.Services;
using WordsAPI.Core.UnitOfWorks;

namespace WordsAPI.Service.Services
{
    public class UserService : Service<User>, IUserService 
    {
        private readonly IUserRepository _userRepository;



        public UserService(IUserRepository repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _userRepository = repository;
        }

        public async Task<CustomResponseDto<UserDTO>> CreateUserAsync(UserRegisterDTO user)
        {
            var userEntity = await _userRepository.CreateUserAsync(user);

            return CustomResponseDto<UserDTO>.Success(200, new UserDTO() { Email = userEntity.Email,Id = userEntity.Id,Name = userEntity.Name,Surname=userEntity.Surname,Username=userEntity.Username});
        }

        public async Task<CustomResponseDto<UserDTO>> GetUserByEmailAsync(UserLoginDTO user)
        {
            var userEntity = await _userRepository.GetUserByEmailAsync(user.Email);

            return CustomResponseDto<UserDTO>.Success(200, new UserDTO() { Email = userEntity.Email, Id = userEntity.Id, Name = userEntity.Name, Surname = userEntity.Surname, Username = userEntity.Username });
        }

        public async Task<CustomResponseDto<UserDTO>> GetUserByUserNameAsync(string username)
        {
            var userEntity = await _userRepository.GetUserByUserNameAsync(username);

            return CustomResponseDto<UserDTO>.Success(200, new UserDTO() { Email = userEntity.Email, Id = userEntity.Id, Name = userEntity.Name, Surname = userEntity.Surname, Username = userEntity.Username });
        }
    }
}
