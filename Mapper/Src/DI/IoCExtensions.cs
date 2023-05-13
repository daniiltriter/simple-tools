using Microsoft.Extensions.DependencyInjection;
using SimpleTools.Mapper.Abstractions;

namespace SimpleTools.Mapper.DI;

public static class IoCExtensions
{
    // TODO: make other method that not will be require configuration
    public static IServiceCollection AddMapper(this IServiceCollection services, Action<MapperOptions> mapperConfiguration)
    {
        var mapperOptions = new MapperOptions();
        mapperConfiguration(mapperOptions);
        services.AddSingleton(mapperOptions);
        services.AddSingleton<IMapper, DefaultMapper>();
        return services;
    }
}