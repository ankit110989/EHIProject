using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EHI.Core.Entities;
using EHI.Core.Models;
using EHI.Core.Services;
using EHI.WebApi.Validations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EHI.WebAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        public ContactController(IContactService contactService, IMapper mapper)
        {
            this._mapper = mapper;
            this._contactService = contactService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAllContacts()
        {
            var contacts = await _contactService.GetAllContacts();
            var contactResources = _mapper.Map<IEnumerable<Contact>, IEnumerable<ContactViewModel>>(contacts);
            return Ok(contactResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactViewModel>> GetContactById(int id)
        {
            var Contact = await _contactService.GetContactById(id);
            var ContactResource = _mapper.Map<Contact, ContactViewModel>(Contact);

            return Ok(ContactResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<ContactViewModel>> CreateContact([FromBody] SaveContactViewModel saveContactResource)
        {
            var validator = new SaveContactValidator();
            var validationResult = await validator.ValidateAsync(saveContactResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var ContactToCreate = _mapper.Map<SaveContactViewModel, Contact>(saveContactResource);

            var newContact = await _contactService.CreateContact(ContactToCreate);

            var Contact = await _contactService.GetContactById(newContact.Id);

            var ContactResource = _mapper.Map<Contact, ContactViewModel>(Contact);

            return Ok(ContactResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ContactViewModel>> UpdateContact(int id, [FromBody] SaveContactViewModel saveContactResource)
        {
            var validator = new SaveContactValidator();
            var validationResult = await validator.ValidateAsync(saveContactResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); 

            var contactToBeUpdate = await _contactService.GetContactById(id);

            if (contactToBeUpdate == null)
                return NotFound();

            var contact = _mapper.Map<SaveContactViewModel, Contact>(saveContactResource);

            await _contactService.UpdateContact(contactToBeUpdate, contact);

            var updatedContact = await _contactService.GetContactById(id);
            var updatedContactResource = _mapper.Map<Contact, ContactViewModel>(updatedContact);

            return Ok(updatedContactResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            if (id == 0)
                return BadRequest();

            var contact = await _contactService.GetContactById(id);

            if (contact == null)
                return NotFound();

            await _contactService.DeleteContact(contact);

            return Ok();
        }

        [HttpPost("ChangeContactStatus/{id}")]
        public async Task<IActionResult> ChangeContactStatus(int id)
        {
            if (id == 0)
                return BadRequest();

            var contact = await _contactService.GetContactById(id);

            if (contact == null)
                return NotFound();

            await _contactService.ChangeContactStatus(contact);

            return Ok();
        }

    }

}
