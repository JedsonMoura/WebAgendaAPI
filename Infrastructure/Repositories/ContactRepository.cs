
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
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _dbcontext;

        public ContactRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            var contacts = await _dbcontext.Contacts.ToListAsync();
            return contacts ?? Enumerable.Empty<Contact>();
        }

        public async Task<Contact> GetContactById(int id)
        {
            var contact = await _dbcontext.Contacts.FindAsync(id);

            if (contact == null)
                throw new ArgumentNullException(nameof(contact));

            return contact;
        }

        public async Task<Contact> AddContact(Contact contact)
        {
            if (contact == null)
                throw new ArgumentNullException(nameof(contact));

            await _dbcontext.AddAsync(contact);
            return contact;
        }

        public void UpdateContact(Contact contact)
        {
            if (contact == null)
                throw new ArgumentNullException(nameof(contact));

            _dbcontext.Update(contact);
        }

        public async Task<Contact> DeleteContact(int id)
        {
            var contact = await GetContactById(id);

            if (contact is null)
                throw new InvalidOperationException("Contact not found !");

            _dbcontext.Contacts.Remove(contact);

            return contact;
        }

    }

}
