using System.Linq.Expressions;
using System.Reflection;

namespace Mapper;

public class Creator<TSource, TResult>
{
    private readonly Func<Dictionary<string, object>, TResult> _resultCreator;

    public Creator()
    {
        var resultType = typeof(TResult);
        var resultExpression = Expression.New(resultType);
        var parameter = Expression.Parameter(typeof(Dictionary<string, object>), "d");
        var resultMembers = new List<MemberBinding>();
        var resultProperties = resultType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        foreach (var property in resultProperties)
        {
            var call = Expression.Call(
                typeof(DictionaryExtension), 
                nameof(DictionaryExtension.GetValue),
                new[]{property.PropertyType},
                new Expression[]
                {
                    parameter,
                    Expression.Constant(property.Name)
                });

            var memberBinding = Expression.Bind(property.GetSetMethod(), call);
            resultMembers.Add(memberBinding);
        }

        var finalParameters = new[] { parameter };
        var initialization = Expression.MemberInit(resultExpression, resultMembers);
        var finalExpression = Expression.Lambda<Func<Dictionary<string, object>, TResult>>(initialization, finalParameters);

        _resultCreator = finalExpression.Compile();
    }

    public TResult Create(Dictionary<string, object> infos)
    {
        return _resultCreator(infos);
    }
}

static class DictionaryExtension
{
    public static TType GetValue<TType>(this Dictionary<string, object> d, string name)
    {
        return d.TryGetValue(name, out var value) ? (TType)value : default(TType);
    }
}