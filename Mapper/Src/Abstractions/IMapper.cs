namespace SimpleTools.Mapper.Abstractions;

public interface IMapper
{
    public TResult Map<TSource, TResult>(TSource source) where TResult : new();
}