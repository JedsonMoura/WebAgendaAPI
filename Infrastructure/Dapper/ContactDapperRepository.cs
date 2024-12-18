using Domain.Interfaces;
using Domain.Entities;
using System.Data;
using Dapper;


namespace Infrastructure.Dapper
{
    public class ContactDapperRepository : IContactDapperRepository
    {
        private readonly IDbConnection _dbConnection;

        public ContactDapperRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            var query = "SELECT * FROM Contacts"; 
            return await _dbConnection.QueryAsync<Contact>(query);
        }

        public async Task<Contact> GetContactById(int id)
        {
            var query = "SELECT * FROM Contacts WHERE Id = @Id"; 

            return await _dbConnection.QueryFirstOrDefaultAsync<Contact>(query, new { Id = id });
        }

      
    }
}
