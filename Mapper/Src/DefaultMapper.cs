using Mapper;
using SimpleTools.Mapper.Abstractions;
using SimpleTools.Mapper.DI;
using SimpleTools.Mapper.Extensions;
using SimpleTools.Mapper.Helpers;

namespace SimpleTools.Mapper;

public class DefaultMapper : IMapper
{
    private readonly MapperOptions _options;
    private readonly TypeCutCache _cuts;

    public DefaultMapper(MapperOptions options, TypeCutCache cuts)
    {
        _options = options;
        _cuts = cuts;
    }
    
    public TResult Map<TSource, TResult>(TSource source) where TResult : new()
    {
        var sourceCuts = _cuts.GetOrAdd<TSource>();

        foreach (var cut in sourceCuts)
        {
            cut.FillValue(source);
        }
        
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