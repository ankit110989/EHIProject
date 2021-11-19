using AutoMapper;
using EHI.Core.Entities;
using EHI.Core.Models;

namespace EHI.WebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, ContactViewModel>();
            CreateMap<ContactViewModel, Contact>();
            CreateMap<SaveContactViewModel, Contact>();
        }
    }
}
