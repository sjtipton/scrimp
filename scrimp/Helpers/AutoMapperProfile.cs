using AutoMapper;
using scrimp.Dtos;
using scrimp.Entities;

namespace scrimp.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
