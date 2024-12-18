using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetContacts();
        Task<Contact> GetContactById(int id);
        Task<Contact> AddContact(Contact contact);
        void UpdateContact(Contact contact);
        Task<Contact> DeleteContact(int id);
    }
}
