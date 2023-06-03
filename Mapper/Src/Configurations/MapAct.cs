using System.Linq.Expressions;
using SimpleTools.Mapper.Abstractions;
using SimpleTools.Mapper.Primitivies;

namespace SimpleTools.Mapper.Configurations;

internal class MapAct<TSource, TResult> : IMapAct<TSource, TResult>
{
    private readonly ICollection<MapCriterion> _criteria = new List<MapCriterion>();

    // public IMapAct<TSource, TResult> Ignore<TMember>(Expression<Func<TSource, TMember>> expression)
    // {
    //     var property = expression.Body as MemberExpression;
    //     var name = property.Member.Name;
    //
    //     var newCriterion = new MapCriterion()
    //     {
    //         FieldName = name,
    //         Action = result =>
    //         {
    //             var memberInfos = typeof(TResult).GetMembers();
    //             foreach (var member in memberInfos)
    //             {
    //                 var type = typeof(TResult);
    //                 type.GetMember(name);
    //                 switch (member)
    //                 {
    //                     case PropertyInfo prop:
    //                         prop.SetValue(result, null);
    //                         break;
    //                     case FieldInfo field:
    //                         field.SetValue(result, null);
    //                         break;
    //                 }
    //             }
    //
    //             return default;
    //         }
    //     };
    //     _сriteria.Add(newCriterion);
    //     return this;
    // }
    
    public IMapAct<TSource, TResult> Ignore<TMember>(Expression<Func<TSource, TMember>> expression)
    {
        var property = expression.Body as MemberExpression;
        var name = property.Member.Name;
        
        var newCriterion = new MapCriterion()
        {
            FieldName = name,
            Action = _ => default,
            MemberType = typeof(TMember)
        };
        _criteria.Add(newCriterion);
        return this;
    }

    public IMapAct<TSource, TResult> Alternate<TMember>(Expression<Func<TResult, TMember>> expression, Func<TSource, TMember> alternate)
    {
        var property = expression.Body as MemberExpression;
        var name = property.Member.Name;

        var newCriterion = new MapCriterion()
        {
            FieldName = name,
            Action = (Func<object, object>)(object)alternate,
            MemberType = typeof(TMember)
        };
        _criteria.Add(newCriterion);
        return this;
    }

    // public IMapAct<TSource, TResult> Alternate<TMember>(Expression<Func<TResult, TMember>> expression, Func<TSource, TMember> alternate)
    // {
    //     var type = typeof(Func<object, object>);
    //     var newCriterion = new MapCriterion()
    //     {
    //         Action = (type)(object)alternate,
    //     }
    // }

    internal IEnumerable<MapCriterion> TransitCriteria()
    {
        return _criteria;
    }
}