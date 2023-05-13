using SimpleTools.Mapper.Abstractions;

namespace SimpleTools.Mapper.Extensions;

public class MapperOptions
{
    private readonly ICollection<MapConfiguration> _configurations = new List<MapConfiguration>();
    
    public MapperOptions With<TMapConfiguration>() where TMapConfiguration : MapConfiguration, new()
    {
        _configurations.Add(new TMapConfiguration());
        return this;
    }
    
    // TODO: need to refactor 
    internal MapConfiguration GetConfigFor<TSource, TResult>()
    {
        foreach (var config in _configurations)
        {
            var typeTuple = config.GetSourceResultPair();
            if (typeTuple.Item1 == typeof(TSource) && typeTuple.Item2 == typeof(TResult))
            {
                return config;
            }
        }

        return null;
    }
}