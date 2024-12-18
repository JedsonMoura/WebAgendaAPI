
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Dapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbcontext;

        public UserRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var Users = await _dbcontext.Users.ToListAsync();
            return Users ?? Enumerable.Empty<User>();
        }

        public async Task<User> GetUserById(int id)
        {
            var User = await _dbcontext.Users.FindAsync(id);

            if (User == null)
                throw new ArgumentNullException(nameof(User));

            return User;
        }

        public async Task<User> AddUser(User User)
        {
            if (User == null)
                throw new ArgumentNullException(nameof(User));

            await _dbcontext.AddAsync(User);
            return User;
        }

        public void UpdateUser(User User)
        {
            if (User == null)
                throw new ArgumentNullException(nameof(User));

            _dbcontext.Update(User);
        }

        public async Task<User> DeleteUser(int id)
        {
            var User = await GetUserById(id);

            if (User is null)
                throw new InvalidOperationException("User not found !");

            _dbcontext.Users.Remove(User);

            return User;
        }

    }

}
