using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> AddUser(User User);
        void UpdateUser(User User);
        Task<User> DeleteUser(int id);
    }
}
