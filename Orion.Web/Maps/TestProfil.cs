using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Orion.Domain;
using Orion.Dto;

namespace Orion.Web.Maps
{
    public class TestProfil: Profile
    {
        public TestProfil()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Contact, ContactDto>().ReverseMap();
        }
    }
}
