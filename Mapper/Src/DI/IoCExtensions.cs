using Microsoft.Extensions.DependencyInjection;
using SimpleTools.Mapper.Abstractions;

namespace SimpleTools.Mapper.Extensions;

public static class IoCExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddSingleton<IMapper, DefaultMapper>();
        return services;
    }
}