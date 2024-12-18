using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserDapperRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> GetCredentialsUser(string usename, string password);
    }
}
