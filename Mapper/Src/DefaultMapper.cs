using SimpleTools.Mapper.Abstractions;
using SimpleTools.Mapper.DI;
using SimpleTools.Mapper.Helpers;

namespace SimpleTools.Mapper;

internal class DefaultMapper : IMapper
{
    private readonly MapperOptions _options;

    public DefaultMapper(MapperOptions options)
    {
        _options = options;
    }
    
    public TResult Map<TSource, TResult>(TSource source) where TResult : new()
    {
        var sourceCuts = FieldSlicer.Cuts(source);
        
        var mapped = FieldFiller.ByCuts<TResult>(sourceCuts);
        
        ApplyOptions(source, ref mapped);

        return mapped;
    }
    
    private void ApplyOptions<TSource, TResult>(TSource source, ref TResult result)
    {
        var config = _options.GetPairConfiguration<TSource, TResult>();
        if (config != null)
        {
            config.Apply(ref result);
        }
    }
}