using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IContactDapperRepository
    {
        Task<IEnumerable<Contact>> GetContacts();
        Task<Contact> GetContactById(int id);
    }
}
