using Mapper;
using SimpleTools.Mapper.Abstractions;
using SimpleTools.Mapper.Configurations;
using SimpleTools.Mapper.DI;
using SimpleTools.Mapper.Helpers;
using SimpleTools.Mapper.Primitivies;
using static  System.Reflection.BindingFlags;

namespace SimpleTools.Mapper;

public class DefaultMapper : IMapper
{
    private readonly MapperOptions _options;
    private readonly TypeCutCache _cuts;

    public DefaultMapper(TypeCutCache cuts, MapperOptions options)
    {
        _cuts = cuts;
        _options = options;
    }
    
    public TResult Map<TSource, TResult>(TSource source) where TResult : new()
    {
        //var creator = _services.GetService<Creator<TSource, TResult>>();
        //var sourceCuts = _cuts.GetOrAddDict(source);
        var cuts = _cuts.GetOrAdd(source);

        ApplyOptions<TSource, TResult>(source, cuts);
        
        //var applicator = _services.GetService<MapApplicator<TSource, TResult>>();

        var mapped = FieldFiller.FromCuts<TResult>(cuts);
        //var mapped = creator.Create(sourceCuts);
        
        return mapped;
    }

    private void ApplyOptions<TSource, TResult>(TSource source, ICollection<FieldCut> cuts)
    {
        var config = _options.GetPairConfiguration<TSource, TResult>();
        if (config != null)
        {
            foreach (var cut in cuts)
            {
                var rawMethod = typeof(MapConfiguration).GetMethod("Apply", NonPublic | Instance);
                var preparedMethod = rawMethod.MakeGenericMethod(typeof(TSource), typeof(TResult), cut.Type);
                preparedMethod.Invoke(config, new object[]{ source, cut });
            }
            
        }
    }
}