using Microsoft.EntityFrameworkCore;
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


        public UserRepository(AppDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _context = context;
            _unitOfWork= unitOfWork;
            _dbSet = _context.Set<User>();
        }

        public async Task<User> CreateUserAsync(UserRegisterDTO user)
        {
            var userEntity = new User() { CreatedDate = DateTime.Now, Email = user.Email, ExperiencePoints = 0, Level = 0, Name = user.Name, NormalizedEmail = user.Email.ToUpper(), NormalizedPassword = user.Password.ToUpper(), Password = user.Password, NormalizedUsername = user.Username.ToUpper(), ProfilePicture = "", RequiredExcperincePoints = 100, Status = 1, Surname = user.Surname, Type = 1, UpdatedDate = DateTime.Now, Username = user.Username };

            await _dbSet.AddAsync(userEntity);
            await _unitOfWork.CommitAsync();

            return userEntity;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbSet.Where(z => z.NormalizedEmail == email.ToUpper()).SingleOrDefaultAsync();
        }

        public async Task<User> GetUserByUserNameAsync(string username)
        {
            return await _dbSet.Where(z => z.NormalizedUsername == username.ToUpper()).SingleOrDefaultAsync();
        }


        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            var userEntity = await _dbSet.Where(z => z.NormalizedEmail == user.NormalizedEmail && z.NormalizedPassword == password.ToUpper()).SingleOrDefaultAsync();

            return userEntity != null;
        }
    }
}
