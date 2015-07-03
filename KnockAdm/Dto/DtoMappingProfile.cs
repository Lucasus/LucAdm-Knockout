using AutoMapper;

namespace KnockAdm
{
    public class DtoMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<User, UserItemDto>();
        }
    }
}