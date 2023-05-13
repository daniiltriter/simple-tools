using SimpleTools.Mapper.Abstractions;
using SimpleTools.Mapper.Helpers;

namespace SimpleTools.Mapper;

public class DefaultMapper : IMapper
{
    public TResult Map<TSource, TResult>(TSource source) where TResult : new()
    {
        var sourceCuts = FieldSlicer.Cuts(source);
        return FieldFiller.ByCuts<TResult>(sourceCuts);
    }
}