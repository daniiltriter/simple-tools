using System.Reflection;
using Mapper;
using Microsoft.Extensions.DependencyInjection;
using SimpleTools.Mapper.Abstractions;
using SimpleTools.Mapper.Configurations;
using SimpleTools.Mapper.DI;
using SimpleTools.Mapper.Extensions;
using SimpleTools.Mapper.Helpers;
using SimpleTools.Mapper.Primitivies;

namespace SimpleTools.Mapper;

public class DefaultMapper : IMapper
{
    private readonly MapperOptions _options;
    private readonly TypeCutCache _cuts;
    private readonly IServiceProvider _services;

    // public DefaultMapper(MapperOptions options, TypeCutCache cuts)
    // {
    //     _options = options;
    //     _cuts = cuts;
    // }
    public DefaultMapper(IServiceProvider services)
    {
        _services = services;
        _cuts = services.GetService<TypeCutCache>();
        _options = services.GetService<MapperOptions>();
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
                //config.Apply<TSource, TResult>(cut);
                var rawMethod = config.GetType().BaseType.GetMembers();
                //var preparedMethod = rawMethod.MakeGenericMethod(typeof(TSource), typeof(TResult), cut.Type);
                //preparedMethod.Invoke(config, new object[]{ source, cut });
            }
            
        }
    }
    
    // private void ApplyOptions<TSource, TResult>(TSource source, ref ICollection<FieldCut> cuts)
    // {
    //     var config = _options.GetPairConfiguration<TSource, TResult>();
    //     if (config != null)
    //     {
    //         config.Apply(ref result);
    //     }
    // }
}