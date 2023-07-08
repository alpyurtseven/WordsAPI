using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Configuration;
using SharedLibrary.Dtos;
using SharedLibrary.Utililty;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;
using WordsAPI.Core.Repositories;
using WordsAPI.Core.Services;
using WordsAPI.Core.UnitOfWorks;
using WordsAPI.SharedLibrary.Exceptions;

namespace WordsAPI.Service.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository repository, IUnitOfWork unitOfWork, IConfiguration configuration) : base(repository, unitOfWork)
        {
            _userRepository = repository;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto<UserDTO>> CreateUserAsync(UserRegisterDTO user)
        {
            var hasUser = _userRepository.Where(z => z.UserName == user.Username || z.Email == user.Email).SingleOrDefault();
           

            if (hasUser != null)
            {
                if (hasUser.UserName == user.Username && hasUser.Email == user.Email)
                {
                    throw new ClientSideException(_configuration.GetSection("ErrorMessages").GetSection("Register").GetValue<string>("UsernameAndEmail"));
                } else if(hasUser.UserName == user.Username)
                {
                    throw new ClientSideException(_configuration.GetSection("ErrorMessages").GetSection("Register").GetValue<string>("Username"));
                }
                else{
                    throw new ClientSideException(_configuration.GetSection("ErrorMessages").GetSection("Register").GetValue<string>("Email"));
                }
            }

            var userEntity = await _userRepository.CreateUserAsync(user);

            return CustomResponseDto<UserDTO>.Success(200, new UserDTO() {
                Email = userEntity.Email,
                Id = userEntity.Id,
                Name = userEntity.FirstName,
                Surname = userEntity.LastName,
                Username = userEntity.UserName,
                Level = userEntity.Level,
                ExperiencePoint = userEntity.ExperiencePoints,
                RequiredExperiencePoint = userEntity.RequiredExperiencePoints,
                ProfilePicture = userEntity.ProfilePicture
            });
        }

        public async Task<CustomResponseDto<UserDTO>> GetUserByEmailAsync(UserLoginDTO user, ODataQueryOptions<User> queryOptions)
        {
            var userEntity = await _userRepository.GetUserByEmailAsync(user.Email ?? "", queryOptions);

            return CustomResponseDto<UserDTO>.Success(200, new UserDTO() {
                Email = userEntity.Email,
                Id = userEntity.Id,
                Name = userEntity.FirstName,
                Surname = userEntity.LastName,
                Username = userEntity.UserName,
                Level = userEntity.Level,
                ExperiencePoint = userEntity.ExperiencePoints,
                RequiredExperiencePoint = userEntity.RequiredExperiencePoints,
                ProfilePicture = userEntity.ProfilePicture
            });
        }

        public async Task<CustomResponseDto<UserDTO>> GetUserByUserNameAsync(string username, ODataQueryOptions<User> queryOptions)
        {
            var userEntity = await _userRepository.GetUserByUserNameAsync(username, queryOptions);

            return CustomResponseDto<UserDTO>.Success(200, new UserDTO() {
                Email = userEntity.Email,
                Id = userEntity.Id,
                Name = userEntity.FirstName,
                Surname = userEntity.LastName,
                Username = userEntity.UserName,
                Level = userEntity.Level,
                ExperiencePoint = userEntity.ExperiencePoints,
                RequiredExperiencePoint = userEntity.RequiredExperiencePoints,
                ProfilePicture = userEntity.ProfilePicture
            });;
        }

        public async Task<CustomResponseDto<bool>> AddWordToUserVocabulary(string username, string word)
        {
            if (username == null)
            {
                throw new ClientSideException("Bu işlemi yapmak için yetkiniz bulunmuyor.");
            }

            var userEntity = await _userRepository.GetUserByUserNameAsync(username);

            return CustomResponseDto<bool>.Success(200, await _userRepository.AddWordToUserVocabulary(userEntity, word));
        }

        public async Task<CustomResponseDto<User>> GetUserById(string id)
        {
            var userEntity = await _userRepository.GetUserById(id);

            return CustomResponseDto<User>.Success(200, userEntity);
        }

        public async Task<CustomResponseDto<List<WordDTO>>> GetUserVocabulary(string username)
        {
            if (username == null)
            {
                throw new ClientSideException("Bu işlemi yapmak için yetkiniz bulunmuyor.");
            }

            var userEntity = await _userRepository.GetUserByUserNameAsync(username);
            var userVocabulary = await _userRepository.GetUserVocabulary(userEntity);

            return CustomResponseDto<List<WordDTO>>.Success(200, userVocabulary);
        }

        public async Task<CustomResponseDto<UserDTO>> UpdateUser(string username, UserUpdateDTO user)
        {
            var hasUser = Utility.isNull<User>(await _userRepository.GetUserByUserNameAsync(username));
            var passwordHasher = new PasswordHasher<IdentityUser>();
        
            hasUser.FirstName = user.Name;
            hasUser.LastName = user.Surname;
            hasUser.Email = user.Email;
            hasUser.ProfilePicture = user.ProfilePicture;

            if (user.Password != null)
            {
                hasUser.PasswordHash = passwordHasher.HashPassword(hasUser, user.Password);
            }
       
            _userRepository.Update(hasUser);

            _unitOfWork.Commit();

            return CustomResponseDto<UserDTO>.Success(200, new UserDTO()
            {
                Email = user.Email,
                ProfilePicture = user.ProfilePicture,
                ExperiencePoint = hasUser.ExperiencePoints,
                RequiredExperiencePoint=hasUser.RequiredExperiencePoints,
                Name = user.Name,
                Surname = user.Surname,
                Id = hasUser.Id,
                Level =hasUser.Level,
                Username = hasUser.UserName
            });
        }
    }
}
