using AutoMapper;
using Backend.Models;
using Backend.Models.Dto;

namespace Backend.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>()
                .ForMember(u => u.Id, d => d.Ignore())
                .ForMember(u => u.LoginPasswordHash, d => d.Ignore())
                .ForMember(u => u.EmailPasswordProtected, d => d.Ignore())
                .ForMember(u => u.Contacts, d => d.Ignore())
                .ForAllMembers(u => u.Condition((dto, user, member) =>
                                member != null && 
                                !(member is string s && string.IsNullOrEmpty(s))));

            CreateMap<UserDto, User>()
                .ForMember(u => u.Id, d => d.Ignore())
                .ForMember(u => u.LoginPasswordHash, d => d.Ignore())
                .ForMember(u => u.EmailPasswordProtected, d => d.Ignore())
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
