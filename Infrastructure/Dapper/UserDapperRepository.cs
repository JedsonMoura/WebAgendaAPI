using Domain.Interfaces;
using Domain.Entities;
using System.Data;
using Dapper;


namespace Infrastructure.Dapper
{
    public class UserDapperRepository : IUserDapperRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserDapperRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var query = "SELECT * FROM Users"; 
            return await _dbConnection.QueryAsync<User>(query);
        }

        public async Task<User> GetUserById(int id)
        {
            var query = "SELECT * FROM Users WHERE Id = @Id"; 

            return await _dbConnection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });
        }


        public async Task<User> GetCredentialsUser(string username, string password)
        {
            var query = "SELECT * FROM Users WHERE Username = @Username AND Password = Password";

            return await _dbConnection.QueryFirstOrDefaultAsync<User>(query, new { Username = username, Password = password });
        }


    }
}
