using System.Linq.Expressions;
using System.Reflection;
using SimpleTools.Mapper.Abstractions;
using SimpleTools.Mapper.Primitivies;

namespace SimpleTools.Mapper;

internal class MapAct<TSource, TResult> : IMapAct<TSource, TResult>
{
    private readonly ICollection<MapCriterion> _сriteria = new List<MapCriterion>();
    
    public IMapAct<TSource, TResult> Ignore<TMember>(Expression<Func<TSource, TMember>> expression)
    {
        var property = expression.Body as MemberExpression;
        var name = property.Member.Name;

        var newCriterion = new MapCriterion()
        {
            FieldName = name,
            Action = result =>
            {
                var memberInfos = typeof(TResult).GetMembers();
                foreach (var member in memberInfos)
                {
                    switch (member)
                    {
                        case PropertyInfo prop when prop.Name == name:
                            prop.SetValue(result, null);
                            break;
                        case FieldInfo field when field.Name == name:
                            field.SetValue(result, null);
                            break;
                    }
                }

                return default;
            }
        };
        _сriteria.Add(newCriterion);
        return this;
    }
    public MapAct<TSource, TResult> ChangeValueFor<TAlternate>(
        Expression<Func<TResult, TAlternate>> expression, 
        Func<TSource, TAlternate> alternate)
    {
        return this;
    }

    internal ICollection<MapCriterion> TransitCriteria()
    {
        return _сriteria;
    }
}