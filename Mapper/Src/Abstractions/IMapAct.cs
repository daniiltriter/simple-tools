using System.Linq.Expressions;

namespace SimpleTools.Mapper.Abstractions;

public interface IMapAct<TSource, TResult>
{
    IMapAct<TSource, TResult> Ignore<TMember>(Expression<Func<TSource, TMember>> expression);
}