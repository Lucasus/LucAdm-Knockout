using AutoMapper;

namespace KnockAdm
{
    public static class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile(new DtoMappingProfile());
            });
        }
    }
}