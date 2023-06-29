using Microsoft.AspNetCore.OData.Query;
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

            return CustomResponseDto<UserDTO>.Success(200, new UserDTO() { Email = userEntity.Email,Id = userEntity.Id,Name = userEntity.FirstName,Surname=userEntity.LastName,Username=userEntity.UserName});
        }

        public async Task<CustomResponseDto<UserDTO>> GetUserByEmailAsync(UserLoginDTO user, ODataQueryOptions<User> queryOptions)
        {
            var userEntity = await _userRepository.GetUserByEmailAsync(user.Email,queryOptions);

            return CustomResponseDto<UserDTO>.Success(200, new UserDTO() { Email = userEntity.Email, Id = userEntity.Id, Name = userEntity.FirstName, Surname = userEntity.LastName, Username = userEntity.UserName });
        }

        public async Task<CustomResponseDto<UserDTO>> GetUserByUserNameAsync(string username, ODataQueryOptions<User> queryOptions)
        {
            var userEntity = await _userRepository.GetUserByUserNameAsync(username, queryOptions);
           
            return CustomResponseDto<UserDTO>.Success(200, new UserDTO() { Email = userEntity.Email, Id = userEntity.Id, Name = userEntity.FirstName, Surname = userEntity.LastName, Username = userEntity.UserName });
        }

        public async Task<CustomResponseDto<bool>> AddWordToUserVocabulary(string username,string word)
        {
            var userEntity = await _userRepository.GetUserByUserNameAsync(username);
            

            return CustomResponseDto<bool>.Success(200, await _userRepository.AddWordToUserVocabulary(userEntity, word));
        }

        public async Task<CustomResponseDto<UserDTO>> GetAllUsers(ODataQueryOptions<User> queryOptions)
        {
            var userEntity =_userRepository.GetAllUsers(queryOptions).ToList().FirstOrDefault();

            return CustomResponseDto<UserDTO>.Success(200, new UserDTO() { Email = userEntity.Email, Id = userEntity.Id, Name = userEntity.FirstName, Surname = userEntity.LastName, Username = userEntity.UserName });
        }

        public async Task<CustomResponseDto<User>> GetUserById(string id)
        {
            var userEntity =await _userRepository.GetUserById(id);

            return CustomResponseDto<User>.Success(200, userEntity);
        }

        public async Task<CustomResponseDto<List<WordDTO>>> GetUserVocabulary(string username)
        {
            var userEntity = await _userRepository.GetUserByUserNameAsync(username);
            var userVocabulary = await _userRepository.GetUserVocabulary(userEntity);

            return CustomResponseDto<List<WordDTO>>.Success(200, userVocabulary);
        }
    }
}
