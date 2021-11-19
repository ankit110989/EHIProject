using EHI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EHI.Core.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact> GetContactById(int id);
        Task<Contact> CreateContact(Contact contact);
        Task UpdateContact(Contact contactToBeUpdated, Contact contact);
        Task DeleteContact(Contact contact);
        Task ChangeContactStatus(Contact contact);
    }
}
