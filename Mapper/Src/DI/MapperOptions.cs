using SimpleTools.Mapper.Abstractions;
using SimpleTools.Mapper.Configurations;

namespace SimpleTools.Mapper.DI;

public class MapperOptions
{
    private readonly ICollection<MapConfiguration> _configurations = new List<MapConfiguration>();
    
    public MapperOptions With<TMapConfiguration>() where TMapConfiguration : MapConfiguration, new()
    {
        _configurations.Add(new TMapConfiguration());
        return this;
    }

    internal MapConfiguration GetPairConfiguration<TSource, TResult>()
    {
        foreach (var config in _configurations)
        {
            var typeTuple = config.GetTypePair();
            if (typeTuple.Item1 == typeof(TSource) && typeTuple.Item2 == typeof(TResult))
            {
                return config;
            }
        }
        
        return null;
    }
    
    // TODO: need to refactor and divide    
    // private MapConfiguration GetPairConfiguration<TSource, TResult>()
    // {
    //     foreach (var config in _configurations)
    //     {
    //         var typeTuple = config.GetSourceResultPair();
    //         if (typeTuple.Item1 == typeof(TSource) && typeTuple.Item2 == typeof(TResult))
    //         {
    //             return config;
    //         }
    //     }
    //
    //     return null;
    // }
}