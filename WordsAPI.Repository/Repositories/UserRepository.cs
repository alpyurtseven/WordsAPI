using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Utililty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;
using WordsAPI.Core.Repositories;
using WordsAPI.Core.UnitOfWorks;
using WordsAPI.Repository.UnitOfWorks;

namespace WordsAPI.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        protected readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<User> _dbSet;
        private readonly IGenericRepository<English> _englishRepository;

        public UserRepository(AppDbContext context, IUnitOfWork unitOfWork, IGenericRepository<English> englishRepository) : base(context)
        {
            _context = context;
            _unitOfWork= unitOfWork;
            _englishRepository= englishRepository;
            _dbSet = _context.Set<User>();
        }

        public async Task<User> CreateUserAsync(UserRegisterDTO user)
        {
            var passwordHasher = new PasswordHasher<IdentityUser>();
            var userEntity = new User() { Email = user.Email, FirstName = user.Name, NormalizedEmail = user.Email.ToUpper(), PasswordHash = user.Password.ToUpper(), NormalizedUserName = user.Username.ToUpper(), LastName = user.Surname,   UserName = user.Username };

            userEntity.PasswordHash = passwordHasher.HashPassword(userEntity, user.Password);

            await _dbSet.AddAsync(userEntity);
            await _unitOfWork.CommitAsync();

            return userEntity;
        }

        public async Task<User> GetUserByEmailAsync(string email, ODataQueryOptions<User> queryOptions)
        {
            return await _dbSet.Where(z => z.NormalizedEmail == email.ToUpper()).SingleOrDefaultAsync();
        }

        public async Task<User> GetUserByUserNameAsync(string username, ODataQueryOptions<User> queryOptions)
        {
            return await _dbSet.Where(z => z.NormalizedUserName == username.ToUpper()).SingleOrDefaultAsync();
        }


        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            var passwordHasher = new PasswordHasher<IdentityUser>();
       
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            return result == PasswordVerificationResult.Success;
        }

        public async Task<bool> AddWordToUserVocabulary(User user, string word)
        {
            var userEntity = await _dbSet.Where(z=>z.Id==user.Id).SingleOrDefaultAsync();
            var wordEntity = await _englishRepository.Where(z => z.NormalizedWord == Utility.NormalizeWord(word)).SingleOrDefaultAsync();

            userEntity.UserWords.Add(new UserWord() { CorrectAnswersCount=1,LastCorrectAnswerDate=DateTime.Now,UserId=user.Id,WordId=wordEntity.Id,WrongAnswersCount=0});

            _dbSet.Update(userEntity);

            await _unitOfWork.CommitAsync();

            return true;
        }

        public IQueryable<User> GetAllUsers(ODataQueryOptions<User> queryOptions)
        {
            return queryOptions.ApplyTo(_dbSet.AsQueryable()) as IQueryable<User>;
        }

    }
}
