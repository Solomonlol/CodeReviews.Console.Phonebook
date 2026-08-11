using AutoMapper;
using Backend.Models;
using Backend.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>()
                .ForMember(u => u.Id, d => d.Ignore())
                .ForMember(u => u.LoginPasswordHash, d => d.Ignore())
                .ForMember(u => u.EmailPasswordHash, d => d.Ignore())
                .ForMember(u => u.Contacts, d => d.Ignore())
                .ForAllMembers(u => u.Condition((dto, user, member) =>
                                member != null && 
                                !(member is string s && string.IsNullOrEmpty(s))));

            CreateMap<UserDto, User>()
                .ForMember(u => u.Id, d => d.Ignore())
                .ForMember(u => u.LoginPasswordHash, d => d.Ignore())
                .ForMember(u => u.EmailPasswordHash, d => d.Ignore())
                .ForMember(u => u.Contacts, d => d.Ignore())
                .ForAllMembers(u => u.Condition((dto, user, member) =>
                                member != null &&
                                !(member is string s && string.IsNullOrEmpty(s))));

            CreateMap<Contact, ContactDto>();
            CreateMap<ContactDto, Contact>()
                .ForMember(c => c.Id, d => d.Ignore())
                .ForMember(c => c.UserId, d => d.Ignore())
                .ForMember(c => c.User, d => d.Ignore())
                .ForAllMembers(u => u.Condition((dto, user, member) =>
                                member != null &&
                                !(member is string s && string.IsNullOrEmpty(s))));

        }
    }
}
