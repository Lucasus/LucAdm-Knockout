using AutoMapper;

namespace KnockAdm
{
	public static class MappingExtensions
	{
        public static T ToDto<T>(this object entity)
        {
            return (T)Mapper.Map(entity, entity.GetType(), typeof(T));
        }
    }
}
