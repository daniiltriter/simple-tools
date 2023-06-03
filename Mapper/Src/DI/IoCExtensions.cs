using Mapper;
using Microsoft.Extensions.DependencyInjection;
using SimpleTools.Mapper.Abstractions;
using SimpleTools.Mapper.Configurations;

namespace SimpleTools.Mapper.DI;

public static class IoCExtensions
{
    // TODO: make other method that not will be require configuration
    public static IServiceCollection AddMapper(this IServiceCollection services, Action<MapperOptions> modifier = null)
    {
        var mapperOptions = new MapperOptions();

        if (modifier != null)
        {
            modifier(mapperOptions);
        }
        
        services.AddSingleton(mapperOptions);
        services.AddSingleton<IMapper, DefaultMapper>();
        services.AddSingleton<TypeCutCache>();

        var configs = mapperOptions.GetConfigurations();

        foreach (var configuration in configs)
        {
            var typeTuple = configuration.GetTypePair();
            var creator = typeof(Creator<,>).MakeGenericType(typeTuple.Item1, typeTuple.Item2);
            services.AddSingleton(creator);
        }
        
        return services;
    }
}