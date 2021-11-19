using AutoMapper;
using EHI.Core.Entities;
using EHI.Core.Models;
using EHI.Core.Repository;
using EHI.Core.Services;
using EHI.Data;
using EHI.Services.Services;
using EHI.WebApi.Mapping;
using EHI.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace EHI.TestApi
{
    [TestClass]
    public class ContactUnitTest
    {

        private IContactService _contactService;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public static DbContextOptions<EHIDBContext> dbContextOptions { get; }
        public static string connectionString = "server=DESKTOP-0D0NMIP;user id=sa;password=wurknow2019;persistsecurityinfo=True;database=EHI";

        static ContactUnitTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<EHIDBContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public ContactUnitTest()
        {
            var context = new EHIDBContext(dbContextOptions);
            _unitOfWork = new UnitOfWork(context);
            _contactService = new ContactService(_unitOfWork);
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mockMapper.CreateMapper();
        }

        [TestMethod]
        public void CreateContactTest()
        {
            SaveContactViewModel contact = new SaveContactViewModel()
            {
                FirstName = "Ankit",
                LastName = "Kumar",
                Email = "ankit110989@gmail.com",
                Phone = "9888114646"
            };
          
            ContactController contactController = new ContactController(_contactService, _mapper);
            var data = contactController.CreateContact(contact).Result;
            Assert.IsInstanceOfType(data.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void CreateContactFailedTest()
        {
            SaveContactViewModel contact = new SaveContactViewModel()
            {
                FirstName = null,
                LastName = "Kumar",
                Email = "ankit110989@gmail.com",
                Phone = "9888114646"
            };

            ContactController contactController = new ContactController(_contactService, _mapper);
            var data = contactController.CreateContact(contact).Result;
            Assert.IsInstanceOfType(data.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void UpdateContactTest()
        {
            IEnumerable<Contact> contacts = _contactService.GetAllContacts().Result;
            var lastRecord = contacts.Last();

            SaveContactViewModel contact = new SaveContactViewModel()
            {
                FirstName = lastRecord.FirstName +"_updated",
                LastName = lastRecord.LastName,
                Email = lastRecord.Email,
                Phone = lastRecord.Phone
            };

            ContactController contactController = new ContactController(_contactService, _mapper);
            var data = contactController.UpdateContact(lastRecord.Id, contact).Result;
            Assert.IsInstanceOfType(data.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void UpdateContactFailedTest()
        {
            IEnumerable<Contact> contacts = _contactService.GetAllContacts().Result;
            var lastRecord = contacts.Last();

            SaveContactViewModel contact = new SaveContactViewModel()
            {
                FirstName = null,
                LastName = lastRecord.LastName,
                Email = lastRecord.Email,
                Phone = lastRecord.Phone
            };

            ContactController contactController = new ContactController(_contactService, _mapper);
            var data = contactController.UpdateContact(lastRecord.Id, contact).Result;
            Assert.IsInstanceOfType(data.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void DeleteContactTest()
        {

            ContactController contactController = new ContactController(_contactService, _mapper);

            SaveContactViewModel contact = new SaveContactViewModel()
            {
                FirstName = "Ankit",
                LastName = "Kumar",
                Email = "ankit110989@gmail.com",
                Phone = "9888114646"
            };

            var result = contactController.CreateContact(contact).Result;
            var contactViewModel = (result.Result as OkObjectResult).Value as ContactViewModel;
            
            var data = contactController.DeleteContact(contactViewModel.Id).Result;
            Assert.IsInstanceOfType(data, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteContactWithZeroIdTest()
        {
            ContactController contactController = new ContactController(_contactService, _mapper);
            var data = contactController.DeleteContact(0).Result;
            Assert.IsInstanceOfType(data, typeof(BadRequestResult));
        }

        [TestMethod]
        public void DeleteContactWithNonExistIdTest()
        {
            ContactController contactController = new ContactController(_contactService, _mapper);
            var data = contactController.DeleteContact(-1).Result;
            Assert.IsInstanceOfType(data, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ChangeContactStatusTest()
        {
            ContactController contactController = new ContactController(_contactService, _mapper);

            SaveContactViewModel contact = new SaveContactViewModel()
            {
                FirstName = "Ankit",
                LastName = "Kumar",
                Email = "ankit110989@gmail.com",
                Phone = "9888114646"
            };

            var result = contactController.CreateContact(contact).Result;
            var contactViewModel = (result.Result as OkObjectResult).Value as ContactViewModel;

            var data = contactController.ChangeContactStatus(contactViewModel.Id).Result;
            Assert.IsInstanceOfType(data, typeof(OkResult));
        }

        [TestMethod]
        public void ChangeContactStatusWithZeroIdTest()
        {
            ContactController contactController = new ContactController(_contactService, _mapper);
            var data = contactController.ChangeContactStatus(0).Result;
            Assert.IsInstanceOfType(data, typeof(BadRequestResult));
        }

        [TestMethod]
        public void ChangeContactStatusWithNonExistIdTest()
        {
            ContactController contactController = new ContactController(_contactService, _mapper);
            var data = contactController.ChangeContactStatus(-1).Result;
            Assert.IsInstanceOfType(data, typeof(NotFoundResult));
        }
    }
}
