using AutoMapper;

namespace SmartPlate.API.Extensions
{
    public static class AutoMapperExtensions
    {
        public static TDestination Map<TSource, TDestination>(
            this TDestination destination,IMapper mapper,  TSource source)
        {
            return mapper.Map(source, destination);
        }
    }
}
