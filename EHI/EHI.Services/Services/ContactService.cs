using EHI.Core.Entities;
using EHI.Core.Repository;
using EHI.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EHI.Services.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        public async Task<Contact> CreateContact(Contact contact)
        {
            contact.IsActive = true;
            contact.CreatedOn = DateTime.Now;

            await _unitOfWork.ContactRepository.AddAsync(contact);
            await _unitOfWork.CommitAsync();
            return contact;
        }

        public async Task DeleteContact(Contact contact)
        {
            _unitOfWork.ContactRepository.Remove(contact);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await _unitOfWork.ContactRepository.GetAllAsync();
        }

        public async Task<Contact> GetContactById(int id)
        {
            return await _unitOfWork.ContactRepository.GetByIdAsync(id);
        }

        public async Task UpdateContact(Contact contactToBeUpdated, Contact contact)
        {
            contactToBeUpdated.FirstName = contact.FirstName;
            contactToBeUpdated.LastName = contact.LastName;
            contactToBeUpdated.Email = contact.Email;
            contactToBeUpdated.Phone = contact.Phone;
            await _unitOfWork.CommitAsync();
        }

        public async Task ChangeContactStatus(Contact contact)
        {
            contact.IsActive = !contact.IsActive;
            await _unitOfWork.CommitAsync();
        }
    }
}
