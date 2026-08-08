using AutoMapper;
using Backend.Models;
using Backend.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d=>d.)
        }
    }
}
