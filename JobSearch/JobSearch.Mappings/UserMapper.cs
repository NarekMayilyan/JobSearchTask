using AutoMapper;
using JobSearch.DAL.Entities.Users;
using JobSearch.Models.Account;
using System;

namespace JobSearch.Mappings
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<RegisterModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(m => m.SecurityStamp, x => x.MapFrom(v => Guid.NewGuid()))
                .ForMember(m => m.CreatedDate, x => x.MapFrom(r => DateTime.Now));
        }
    }
}
